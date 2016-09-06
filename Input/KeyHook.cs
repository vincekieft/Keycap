using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Keycap.InputEngine
{
    public class KeyHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        #region
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion

        public KeyHook()
        {
            keycodes.initKeyCodes();
            _hookID = SetHook(_proc);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            onAnyKeyPressed(nCode, wParam, lParam);

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static void onAnyKeyPressed(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == (IntPtr)WM_KEYDOWN)
            {
                // key down
                keycodes.setPressed(getKeyNumber(lParam), true);
                keycodes.refreshCallbacks(getKeyNumber(lParam),inputTypes.down);
            }
            else
            {
                // key up
                keycodes.setPressed(getKeyNumber(lParam), false);
                keycodes.refreshCallbacks(getKeyNumber(lParam), inputTypes.up);
            }
        }

        private static int getKeyNumber(IntPtr lParam)
        {
            return Marshal.ReadInt32(lParam);
        }

    }
}
