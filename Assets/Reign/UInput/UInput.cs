//#undef ENABLE_INPUT_SYSTEM

using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

namespace Reign
{
    [DefaultExecutionOrder(-32000)]
    public class UInput : MonoBehaviour
    {
        #if ENABLE_INPUT_SYSTEM
        private static Mouse mouse;
        private static Keyboard keyboard;

        private static bool devicesChanged;
        public static List<UGamepad> gamepads { get; private set; }

        public delegate void DevicesChangedCallbackMethod();
        public static event DevicesChangedCallbackMethod DevicesChangedCallback;

        private void RefreshDevices()
        {
            //Debug.Log("UInput: RefreshDevices");
            var devices = InputSystem.devices;
            gamepads = new List<UGamepad>();
            for (int i = 0; i != devices.Count; ++i)
            {
                var device = devices[i];
                if (!(device is Gamepad)) continue;

                //Debug.Log($"UInput: Gampad '{device.name}' Type:'{device.GetType()}'");
                gamepads.Add(new UGamepad(device));
            }

            DevicesChangedCallback?.Invoke();
        }

		private void Start()
		{
            RefreshDevices();
			InputSystem.onDeviceChange += InputSystem_onDeviceChange;
		}

        private void OnDestroy()
        {
            InputSystem.onDeviceChange -= InputSystem_onDeviceChange;
        }

        private void InputSystem_onDeviceChange(InputDevice device, InputDeviceChange change)
        {
            devicesChanged = true;
        }

        private void Update()
        {
            // keep grabbing them in case device changes occur
            mouse = Mouse.current;
            keyboard = Keyboard.current;

            // refresh devices
            if (devicesChanged)
            {
                devicesChanged = false;
                RefreshDevices();
            }

            // collect input from all devices
            foreach (var gamepad in gamepads)
            {
                gamepad.Update();
            }
        }
        #else
        private static Vector3 lastMousePos;

        static UInput()
        {
            lastMousePos = Input.mousePosition;
        }
        #endif

        #region Mouse
        public static Vector2 mousePosition
        {
            get
            {
                #if ENABLE_INPUT_SYSTEM
                if (mouse == null) return default;
                return mouse.position.ReadValue();
                #else
                 return Input.mousePosition;
                #endif
            }
        }

        public static Vector2 mouseVelocity
        {
            get
            {
                #if ENABLE_INPUT_SYSTEM
                if (mouse == null) return default;
                return mouse.delta.ReadValue();
                #else
                var currentMousePos = Input.mousePosition;
                var result = currentMousePos - lastMousePos;
                lastMousePos = currentMousePos;
                return result;
                #endif
            }
        }

        public static bool GetMouseButton(int button)
        {
            #if ENABLE_INPUT_SYSTEM
            if (mouse == null) return false;
            if (button == 0) return mouse.leftButton.isPressed;
            if (button == 1) return mouse.rightButton.isPressed;
            if (button == 2) return mouse.middleButton.isPressed;
            return false;
            #else
            return Input.GetMouseButton(button);
            #endif
        }

        public static bool GetMouseButtonDown(int button)
        {
            #if ENABLE_INPUT_SYSTEM
            if (mouse == null) return false;
            if (button == 0) return mouse.leftButton.wasPressedThisFrame;
            if (button == 1) return mouse.rightButton.wasPressedThisFrame;
            if (button == 2) return mouse.middleButton.wasPressedThisFrame;
            return false;
            #else
            return Input.GetMouseButtonDown(button);
            #endif
        }

        public static bool GetMouseButtonUp(int button)
        {
            #if ENABLE_INPUT_SYSTEM
            if (mouse == null) return false;
            if (button == 0) return mouse.leftButton.wasReleasedThisFrame;
            if (button == 1) return mouse.rightButton.wasReleasedThisFrame;
            if (button == 2) return mouse.middleButton.wasReleasedThisFrame;
            return false;
            #else
            return Input.GetMouseButtonUp(button);
            #endif
        }
		#endregion

        #region Keyboard
		public static bool GetKey(KeyCode key)
        {
            #if ENABLE_INPUT_SYSTEM
            if (keyboard == null) return false;
            var keyCode = ConvertKeyCode(key);
            if (keyCode == Key.None) return false;
            var keyControl = keyboard[keyCode];
            if (keyControl != null) return keyControl.isPressed;
            return false;
            #else
            return Input.GetKey(key);
            #endif
        }

        public static bool GetKeyDown(KeyCode key)
        {
            #if ENABLE_INPUT_SYSTEM
            if (keyboard == null) return false;
            var keyCode = ConvertKeyCode(key);
            if (keyCode == Key.None) return false;
            var keyControl = keyboard[keyCode];
            if (keyControl != null) return keyControl.wasPressedThisFrame;
            return false;
            #else
            return Input.GetKeyDown(key);
            #endif
        }

        public static bool GetKeyUp(KeyCode key)
        {
            #if ENABLE_INPUT_SYSTEM
            if (keyboard == null) return false;
            var keyCode = ConvertKeyCode(key);
            if (keyCode == Key.None) return false;
            var keyControl = keyboard[keyCode];
            if (keyControl != null) return keyControl.wasReleasedThisFrame;
            return false;
            #else
            return Input.GetKeyUp(key);
            #endif
        }

        #if ENABLE_INPUT_SYSTEM
        private static Key ConvertKeyCode(KeyCode key)
		{
			switch (key)
			{
				case KeyCode.A: return Key.A;
				case KeyCode.B: return Key.B;
				case KeyCode.C: return Key.C;
				case KeyCode.D: return Key.D;
				case KeyCode.E: return Key.E;
				case KeyCode.F: return Key.F;
				case KeyCode.G: return Key.G;
				case KeyCode.H: return Key.H;
				case KeyCode.I: return Key.I;
				case KeyCode.J: return Key.J;
				case KeyCode.K: return Key.K;
				case KeyCode.L: return Key.L;
				case KeyCode.M: return Key.M;
				case KeyCode.N: return Key.N;
				case KeyCode.O: return Key.O;
				case KeyCode.P: return Key.P;
				case KeyCode.Q: return Key.Q;
				case KeyCode.R: return Key.R;
				case KeyCode.S: return Key.S;
				case KeyCode.T: return Key.T;
				case KeyCode.U: return Key.U;
				case KeyCode.V: return Key.V;
				case KeyCode.W: return Key.W;
				case KeyCode.X: return Key.X;
				case KeyCode.Y: return Key.Y;
				case KeyCode.Z: return Key.Z;

				case KeyCode.Alpha0: return Key.Digit0;
				case KeyCode.Alpha1: return Key.Digit1;
				case KeyCode.Alpha2: return Key.Digit2;
				case KeyCode.Alpha3: return Key.Digit3;
				case KeyCode.Alpha4: return Key.Digit4;
				case KeyCode.Alpha5: return Key.Digit5;
				case KeyCode.Alpha6: return Key.Digit6;
				case KeyCode.Alpha7: return Key.Digit7;
				case KeyCode.Alpha8: return Key.Digit8;
				case KeyCode.Alpha9: return Key.Digit9;

				case KeyCode.Keypad0: return Key.Numpad0;
				case KeyCode.Keypad1: return Key.Numpad1;
				case KeyCode.Keypad2: return Key.Numpad2;
				case KeyCode.Keypad3: return Key.Numpad3;
				case KeyCode.Keypad4: return Key.Numpad4;
				case KeyCode.Keypad5: return Key.Numpad5;
				case KeyCode.Keypad6: return Key.Numpad6;
				case KeyCode.Keypad7: return Key.Numpad7;
				case KeyCode.Keypad8: return Key.Numpad8;
				case KeyCode.Keypad9: return Key.Numpad9;

				case KeyCode.KeypadDivide: return Key.NumpadDivide;
				case KeyCode.KeypadMultiply: return Key.NumpadMultiply;
				case KeyCode.KeypadMinus: return Key.NumpadMinus;
				case KeyCode.KeypadPlus: return Key.NumpadPlus;
				case KeyCode.KeypadPeriod: return Key.NumpadPeriod;
				case KeyCode.KeypadEnter: return Key.NumpadEnter;

				case KeyCode.Minus: return Key.Minus;
				case KeyCode.Equals: return Key.Equals;

				case KeyCode.LeftBracket: return Key.LeftBracket;
				case KeyCode.RightBracket: return Key.RightBracket;

				case KeyCode.Comma: return Key.Comma;
				case KeyCode.Period: return Key.Period;

				case KeyCode.Tilde: return Key.Backquote;
				case KeyCode.Return: return Key.Enter;
				case KeyCode.Backspace: return Key.Backspace;
				case KeyCode.Insert: return Key.Insert;
				case KeyCode.Delete: return Key.Delete;
				case KeyCode.Tab: return Key.Tab;
				case KeyCode.CapsLock: return Key.CapsLock;
				case KeyCode.Semicolon: return Key.Semicolon;
				case KeyCode.Quote: return Key.Quote;
				case KeyCode.Slash: return Key.Slash;
				case KeyCode.Backslash: return Key.Backslash;
				case KeyCode.Escape: return Key.Escape;
				case KeyCode.Space: return Key.Space;

				case KeyCode.LeftShift: return Key.LeftShift;
				case KeyCode.RightShift: return Key.RightShift;

				case KeyCode.LeftAlt: return Key.LeftAlt;
				case KeyCode.RightAlt: return Key.RightAlt;

				case KeyCode.LeftControl: return Key.LeftCtrl;
				case KeyCode.RightControl: return Key.RightCtrl;

				case KeyCode.LeftArrow: return Key.LeftArrow;
				case KeyCode.RightArrow: return Key.RightArrow;
				case KeyCode.DownArrow: return Key.DownArrow;
				case KeyCode.UpArrow: return Key.UpArrow;

				case KeyCode.Home: return Key.Home;
				case KeyCode.End: return Key.End;
				case KeyCode.PageUp: return Key.PageUp;
				case KeyCode.PageDown: return Key.PageDown;

				case KeyCode.F1: return Key.F1;
				case KeyCode.F2: return Key.F2;
				case KeyCode.F3: return Key.F3;
				case KeyCode.F4: return Key.F4;
				case KeyCode.F5: return Key.F5;
				case KeyCode.F6: return Key.F6;
				case KeyCode.F7: return Key.F7;
				case KeyCode.F8: return Key.F8;
				case KeyCode.F9: return Key.F9;
				case KeyCode.F10: return Key.F10;
				case KeyCode.F11: return Key.F11;
				case KeyCode.F12: return Key.F12;
			}

			return Key.None;
		}
        #endif
		#endregion
	}
}