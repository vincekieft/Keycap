using Keycap.InputEngine;

namespace Keycap
{
    public class Input
    {
        private static KeyHook keyHook;

        /// <summary>
        /// Initalize the input functions
        /// </summary>
        public static void initInput()
        {
            // init keyhook
            keyHook = keyHook == null ? new KeyHook() : keyHook;
        }

        /// <summary>
        /// This methode can be used in a update function to detect if a certain key is pressed down. For example:
        /// If(Input.onKeyDown(KeyCode.Space)){
        ///     // this is called when the space bar is pressed down
        /// }
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Returns true or false</returns>
        public static bool onKeyDown(keyCode code)
        {
            keyObj key = keycodes.keys[code];
            if(key != null)
            {
                bool b = key.isDown();
                if (b) { key.waitForChange = true; }
                
                return b;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// This methode can be used in a update function to detect if a certain key is released. For example:
        /// If(Input.onKeyUp(KeyCode.Space)){
        ///     // this is called when the space bar is released
        /// }
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Returns true or false</returns>
        public static bool onKeyUp(keyCode code)
        {
            keyObj key = keycodes.keys[code];
            if (key != null)
            {
                bool b = key.isUp();
                if (b) { key.waitForChange = true; }
                return b;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This methode can be used in a update function to detect if a certain key is being held down. For example:
        /// If(Input.onKeyPressed(KeyCode.Space)){
        ///     // this is called when the space bar is pressed
        /// }
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Returns true or false</returns>
        public static bool onKeyPressed(keyCode code)
        {
            keyObj key = keycodes.keys[code];
            if (key != null)
            {
                return key.isPressed();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This methode can be used to register a key down event. You can use it like this:
        /// Input.registerOnKeyDown(KeyCode.Space,callbackVoid,(optional true or false for repeat));
        /// 
        /// </summary>
        /// <param name="code">The keycode you want to use, for example: KeyCode.Space</param>
        /// <param name="callback">The void that you want the system to callback once the given key is pressed</param>
        /// <param name="repeat">Do you want the event to stay alive after the first event.</param>
        public static void registerOnKeyDown(keyCode code, keycodes.registerCallback callback, bool repeat = false)
        {
            if (repeat)
            {
                keycodes.registerKey(code, callback, inputTypes.repeatedDown);
            } else
            {
                keycodes.registerKey(code, callback, inputTypes.down);
            }
        }
        
        /// <summary>
        /// This methode can be used to register a key pressed event. You can use it like this:
        /// Input.registerOnKeyPressed(KeyCode.Space,callbackVoid);
        /// 
        /// </summary>
        /// <param name="code">The keycode you want to use, for example: KeyCode.Space</param>
        /// <param name="callback">The void that you want the system to callback once the given key is pressed</param>
        public static void registerOnKeyPressed(keyCode code, keycodes.registerCallback callback)
        {
            keycodes.registerKey(code, callback, inputTypes.pressed);
        }
        
        /// <summary>
        /// This methode can be used to register a key up event. You can use it like this:
        /// Input.registerOnKeyUp(KeyCode.Space,callbackVoid,(optional true or false for repeat));
        /// 
        /// </summary>
        /// <param name="code">The keycode you want to use, for example: KeyCode.Space</param>
        /// <param name="callback">The void that you want the system to callback once the given key is pressed</param>
        /// <param name="repeat">Do you want the event to stay alive after the first event.</param>
        public static void registerOnKeyUp(keyCode code,keycodes.registerCallback callback, bool repeat = false)
        {
            if (repeat)
            {
                keycodes.registerKey(code,callback,inputTypes.repeatedUp);
            } else
            {
                keycodes.registerKey(code, callback, inputTypes.up);
            }
        }

        /// <summary>
        /// Remove all the registerd events from a key
        /// </summary>
        /// <param name="code">The key you want to target</param>
        public static void removeKeyRegister(keyCode code)
        {
            keycodes.cleanListOf(code,inputTypes.up,true);
        }
        
    }
}
