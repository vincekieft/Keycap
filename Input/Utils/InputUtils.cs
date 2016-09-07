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
            keys.Add(keyCode.Space, new keyObj(32));
            keys.Add(keyCode.Backspace, new keyObj(8));
            keys.Add(keyCode.Tab, new keyObj(9));
            keys.Add(keyCode.Enter, new keyObj(13));
            keys.Add(keyCode.Shift, new keyObj(16));
            keys.Add(keyCode.Ctrl, new keyObj(17));
            keys.Add(keyCode.Pause, new keyObj(18));
            keys.Add(keyCode.CapsLock, new keyObj(20));
            keys.Add(keyCode.Esc, new keyObj(27));
            keys.Add(keyCode.PageUp, new keyObj(33));
            keys.Add(keyCode.PageDown, new keyObj(34));
            keys.Add(keyCode.End, new keyObj(35));
            keys.Add(keyCode.Home, new keyObj(36));
            keys.Add(keyCode.Left, new keyObj(37));
            keys.Add(keyCode.Up, new keyObj(38));
            keys.Add(keyCode.Right, new keyObj(39));
            keys.Add(keyCode.Down, new keyObj(40));
            keys.Add(keyCode.Select, new keyObj(41));
            keys.Add(keyCode.PrintScreen, new keyObj(42));
            keys.Add(keyCode.Ins, new keyObj(45));
            keys.Add(keyCode.Del, new keyObj(46));
            keys.Add(keyCode.NumLock, new keyObj(144));

            // letters
            keys.Add(keyCode.A, new keyObj(65));
            keys.Add(keyCode.B, new keyObj(66));
            keys.Add(keyCode.C, new keyObj(67));
            keys.Add(keyCode.D, new keyObj(68));
            keys.Add(keyCode.E, new keyObj(69));
            keys.Add(keyCode.F, new keyObj(70));
            keys.Add(keyCode.G, new keyObj(71));
            keys.Add(keyCode.H, new keyObj(72));
            keys.Add(keyCode.I, new keyObj(73));
            keys.Add(keyCode.J, new keyObj(74));
            keys.Add(keyCode.K, new keyObj(75));
            keys.Add(keyCode.L, new keyObj(76));
            keys.Add(keyCode.M, new keyObj(77));
            keys.Add(keyCode.N, new keyObj(78));
            keys.Add(keyCode.O, new keyObj(79));
            keys.Add(keyCode.P, new keyObj(80));
            keys.Add(keyCode.Q, new keyObj(81));
            keys.Add(keyCode.R, new keyObj(82));
            keys.Add(keyCode.S, new keyObj(83));
            keys.Add(keyCode.T, new keyObj(84));
            keys.Add(keyCode.U, new keyObj(85));
            keys.Add(keyCode.V, new keyObj(86));
            keys.Add(keyCode.W, new keyObj(87));
            keys.Add(keyCode.X, new keyObj(88));
            keys.Add(keyCode.Y, new keyObj(89));
            keys.Add(keyCode.Z, new keyObj(90));


            // numbers
            keys.Add(keyCode.Zero, new keyObj(48));
            keys.Add(keyCode.One, new keyObj(49));
            keys.Add(keyCode.Two, new keyObj(50));
            keys.Add(keyCode.Three, new keyObj(51));
            keys.Add(keyCode.Four, new keyObj(52));
            keys.Add(keyCode.Five, new keyObj(53));
            keys.Add(keyCode.Six, new keyObj(54));
            keys.Add(keyCode.Seven, new keyObj(55));
            keys.Add(keyCode.Eight, new keyObj(56));
            keys.Add(keyCode.Nine, new keyObj(57));

            // function keys
            keys.Add(keyCode.F1, new keyObj(112));
            keys.Add(keyCode.F2, new keyObj(113));
            keys.Add(keyCode.F3, new keyObj(114));
            keys.Add(keyCode.F4, new keyObj(115));
            keys.Add(keyCode.F5, new keyObj(116));
            keys.Add(keyCode.F6, new keyObj(117));
            keys.Add(keyCode.F7, new keyObj(118));
            keys.Add(keyCode.F8, new keyObj(119));
            keys.Add(keyCode.F9, new keyObj(120));
            keys.Add(keyCode.F10, new keyObj(121));
            keys.Add(keyCode.F11, new keyObj(122));
            keys.Add(keyCode.F12, new keyObj(123));
        }

        public static keycodes getInstance()
        {
            return single = single == null ? new keycodes() : single;
        }

        public static Dictionary<keyCode, keyObj> keys = new Dictionary<keyCode, keyObj>();

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
            keyCode key = getKeyNameByID(keyID);
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

        public static void registerKey(keyCode key, registerCallback callback,inputTypes inputType)
        {
            registerdKeys.Add(new keyRegister(callback,key, inputType));
        }

        public static void cleanListOf(keyCode key,inputTypes input, bool force = false)
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
        public static keyCode getKeyNameByID(int id)
        {
            return keys.FirstOrDefault(x => x.Value.VirtualCode == id).Key;
        }

        

        public static List<keyRegister> getAllRegisterdByKey(keyCode key)
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
        private keyCode key;
        private inputTypes inputType;
        public keyRegister(keycodes.registerCallback callback,keyCode key,inputTypes inputType)
        {
            this.key = key;
            this.callback = callback;
            this.inputType = inputType;
        }

        public keyCode getKeyCode()
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
    public enum keyCode
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
