using System;
using System.Collections.Generic;
using System.Linq;

namespace Keycap.InputEngine
{
    public class keycodes
    {
        public delegate void registerCallback();
        public static keycodes single;
        private static List<keyRegister> registerdKeys = new List<keyRegister>();

        public static void initKeyCodes()
        {
            // special keys
            keys.Add(KeyCode.Space, new keyObj(32));
            keys.Add(KeyCode.Backspace, new keyObj(8));
            keys.Add(KeyCode.Tab, new keyObj(9));
            keys.Add(KeyCode.Enter, new keyObj(13));
            keys.Add(KeyCode.Shift, new keyObj(16));
            keys.Add(KeyCode.Ctrl, new keyObj(17));
            keys.Add(KeyCode.Pause, new keyObj(18));
            keys.Add(KeyCode.CapsLock, new keyObj(20));
            keys.Add(KeyCode.Esc, new keyObj(27));
            keys.Add(KeyCode.PageUp, new keyObj(33));
            keys.Add(KeyCode.PageDown, new keyObj(34));
            keys.Add(KeyCode.End, new keyObj(35));
            keys.Add(KeyCode.Home, new keyObj(36));
            keys.Add(KeyCode.Left, new keyObj(37));
            keys.Add(KeyCode.Up, new keyObj(38));
            keys.Add(KeyCode.Right, new keyObj(39));
            keys.Add(KeyCode.Down, new keyObj(40));
            keys.Add(KeyCode.Select, new keyObj(41));
            keys.Add(KeyCode.PrintScreen, new keyObj(42));
            keys.Add(KeyCode.Ins, new keyObj(45));
            keys.Add(KeyCode.Del, new keyObj(46));
            keys.Add(KeyCode.NumLock, new keyObj(144));

            // letters
            keys.Add(KeyCode.A, new keyObj(65));
            keys.Add(KeyCode.B, new keyObj(66));
            keys.Add(KeyCode.C, new keyObj(67));
            keys.Add(KeyCode.D, new keyObj(68));
            keys.Add(KeyCode.E, new keyObj(69));
            keys.Add(KeyCode.F, new keyObj(70));
            keys.Add(KeyCode.G, new keyObj(71));
            keys.Add(KeyCode.H, new keyObj(72));
            keys.Add(KeyCode.I, new keyObj(73));
            keys.Add(KeyCode.J, new keyObj(74));
            keys.Add(KeyCode.K, new keyObj(75));
            keys.Add(KeyCode.L, new keyObj(76));
            keys.Add(KeyCode.M, new keyObj(77));
            keys.Add(KeyCode.N, new keyObj(78));
            keys.Add(KeyCode.O, new keyObj(79));
            keys.Add(KeyCode.P, new keyObj(80));
            keys.Add(KeyCode.Q, new keyObj(81));
            keys.Add(KeyCode.R, new keyObj(82));
            keys.Add(KeyCode.S, new keyObj(83));
            keys.Add(KeyCode.T, new keyObj(84));
            keys.Add(KeyCode.U, new keyObj(85));
            keys.Add(KeyCode.V, new keyObj(86));
            keys.Add(KeyCode.W, new keyObj(87));
            keys.Add(KeyCode.X, new keyObj(88));
            keys.Add(KeyCode.Y, new keyObj(89));
            keys.Add(KeyCode.Z, new keyObj(90));


            // numbers
            keys.Add(KeyCode.Zero, new keyObj(48));
            keys.Add(KeyCode.One, new keyObj(49));
            keys.Add(KeyCode.Two, new keyObj(50));
            keys.Add(KeyCode.Three, new keyObj(51));
            keys.Add(KeyCode.Four, new keyObj(52));
            keys.Add(KeyCode.Five, new keyObj(53));
            keys.Add(KeyCode.Six, new keyObj(54));
            keys.Add(KeyCode.Seven, new keyObj(55));
            keys.Add(KeyCode.Eight, new keyObj(56));
            keys.Add(KeyCode.Nine, new keyObj(57));

            // function keys
            keys.Add(KeyCode.F1, new keyObj(112));
            keys.Add(KeyCode.F2, new keyObj(113));
            keys.Add(KeyCode.F3, new keyObj(114));
            keys.Add(KeyCode.F4, new keyObj(115));
            keys.Add(KeyCode.F5, new keyObj(116));
            keys.Add(KeyCode.F6, new keyObj(117));
            keys.Add(KeyCode.F7, new keyObj(118));
            keys.Add(KeyCode.F8, new keyObj(119));
            keys.Add(KeyCode.F9, new keyObj(120));
            keys.Add(KeyCode.F10, new keyObj(121));
            keys.Add(KeyCode.F11, new keyObj(122));
            keys.Add(KeyCode.F12, new keyObj(123));
        }

        public static keycodes getInstance()
        {
            return single = single == null ? new keycodes() : single;
        }

        public static Dictionary<KeyCode, keyObj> keys = new Dictionary<KeyCode, keyObj>();

        public static void setPressed(int id,bool press)
        {
            keyObj matchedKeys = getKeyByID(id);
            if (matchedKeys != null)
            {
                if (press)
                {
                    // down
                    if (matchedKeys.isPres)
                    {
                        matchedKeys.waitForChange = true;
                    } else
                    {
                        matchedKeys.waitForChange = false;
                    }
                    matchedKeys.isPres = true;
                } else
                {
                    // up
                    if (matchedKeys.isPres)
                    {
                        matchedKeys.waitForChange = false;
                    } else
                    {
                        // if is already up
                        matchedKeys.waitForChange = true;
                    }
                    matchedKeys.isPres = false;
                }
            }
            else
            {
                Console.WriteLine("unknown key");
            }
        }

        public static void refreshCallbacks(int keyID,inputTypes inputType)
        {
            KeyCode key = getKeyNameByID(keyID);
            List<keyRegister> keys = getAllRegisterdByKey(key);
            if(keys.Count > 0)
            {
                foreach (keyRegister rKey in keys)
                {
                    if(rKey.getKeyInputType() != inputTypes.pressed)
                    {
                        // single event
                        keyObj keyobj = keycodes.keys[key];
                        if(rKey.getKeyInputType() == inputTypes.up || rKey.getKeyInputType() == inputTypes.repeatedUp)
                        {
                            if (keyobj.isUp())
                            {
                                rKey.Invoke();
                            }
                        } else
                        {
                            if (keyobj.isDown())
                            {
                                rKey.Invoke();
                            }
                        }
                    } else
                    {
                        // is pressed
                        if(inputType == inputTypes.down)
                        {
                            rKey.Invoke();
                        }
                    }
                    
                }
                cleanListOf(key,inputType);
            }
        }

        public static void registerKey(KeyCode key, registerCallback callback,inputTypes inputType)
        {
            registerdKeys.Add(new keyRegister(callback,key, inputType));
        }

        public static void cleanListOf(KeyCode key,inputTypes input, bool force = false)
        {
            bool found = false;
            foreach(keyRegister rKey in registerdKeys)
            {
                if (force)
                {
                    // is key
                    found = true;
                    registerdKeys.Remove(rKey);
                    break;
                } else
                if(rKey.getKeyCode() == key && rKey.getKeyInputType() != inputTypes.pressed && rKey.getKeyInputType() != inputTypes.repeatedUp && rKey.getKeyInputType() != inputTypes.repeatedDown)
                {
                    if(input == inputTypes.up)
                    {
                        if(rKey.getKeyInputType() == inputTypes.up || rKey.getKeyInputType() == inputTypes.repeatedUp)
                        {
                            // is key
                            found = true;
                            registerdKeys.Remove(rKey);
                            break;
                        }
                    } else
                    {
                        if (rKey.getKeyInputType() == inputTypes.down || rKey.getKeyInputType() == inputTypes.repeatedDown)
                        {
                            // is key
                            found = true;
                            registerdKeys.Remove(rKey);
                            break;
                        }
                    }
                   
                }
            }
            if (found)
            {
                cleanListOf(key, input, force);
            }
        }

        public static keyObj getKeyByID(int id)
        {
            return keys.FirstOrDefault(x => x.Value.VirtualCode == id).Value;
        }
        public static KeyCode getKeyNameByID(int id)
        {
            return keys.FirstOrDefault(x => x.Value.VirtualCode == id).Key;
        }

        

        public static List<keyRegister> getAllRegisterdByKey(KeyCode key)
        {
            List<keyRegister> returnList = new List<keyRegister>();
            foreach (keyRegister rKey in registerdKeys)
            {
                if (rKey.getKeyCode() == key)
                {
                    returnList.Add(rKey);
                }
            }
            return returnList;
        }
    }
    public enum inputTypes
    {
        down,
        up,
        pressed,
        repeatedUp,
        repeatedDown
    }

    public class keyObj
    {
        public int VirtualCode { get; set; }
        public bool isPres = false;
        public bool waitForChange = true;

        public keyObj(int VirtualCode)
        {
            this.VirtualCode = VirtualCode;
        }

        public bool isDown()
        {
            if (isPres && !waitForChange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPressed()
        {
            return isPres;
        }

        public bool isUp()
        {
            if (!isPres && !waitForChange)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void onChange()
        {
            if (waitForChange)
            {
                waitForChange = false;
            }
        }
    }

    public class keyRegister
    {
        private keycodes.registerCallback callback;
        private KeyCode key;
        private inputTypes inputType;
        public keyRegister(keycodes.registerCallback callback, KeyCode key,inputTypes inputType)
        {
            this.key = key;
            this.callback = callback;
            this.inputType = inputType;
        }

        public KeyCode getKeyCode()
        {
            return this.key;
        }

        public inputTypes getKeyInputType()
        {
            return this.inputType;
        }

        public void Invoke()
        {
            callback.Invoke();
        }
    }
}

namespace Keycap
{
    public enum KeyCode
    {
        // special buttons
        Space,
        Tab,
        Enter,
        Shift,
        Backspace,
        Ctrl,
        Alt,
        Pause,
        CapsLock,
        Esc,
        PageUp,
        PageDown,
        End,
        Home,
        Left,
        Right,
        Up,
        Down,
        Ins,
        Del,
        Help,
        PrintScreen,
        Select,
        NumLock,
        
        // letters
        A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,

        //numbers
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,

        //Function Keys
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12
    }
}
