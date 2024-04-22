using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;

namespace Reign
{
	public sealed class UGamepad
	{
		public readonly InputDevice device;
		private IDualMotorRumble dualMotorRumble;
		public readonly List<InputControl> controls;

		public UButton button0, button1, button2, button3;
		public UButton dpadUp, dpadDown, dpadLeft, dpadRight;
		public UButton menu, back;

		public UButton bumperLeft, bumperRight;
		public UTrigger triggerLeft, triggerRight;

		public UButton joystickButtonLeft, joystickButtonRight;
		public UJoystick joystickLeft, joystickRight;

		public UGamepad(InputDevice device)
		{
			this.device = device;
			dualMotorRumble = device as IDualMotorRumble;

			// grab only the controls we need
			var inputControls = device.allControls;
			controls = new List<InputControl>();
			foreach (var control in inputControls)
			{
				string path = control.path;
				if (control is ButtonControl button)
				{
					// ignore joysticks acting like dpad
					if (path.Contains("Stick/") || path.Contains("Trigger/")) continue;

					// map specific buttons to known primitives
					switch (control.name)
					{
						// actual buttons
						case "buttonSouth": button0.control = button; break;
						case "buttonWest": button1.control = button; break;
						case "buttonNorth": button2.control = button; break;
						case "buttonEast": button3.control = button; break;

						case "down": dpadDown.control = button; break;
						case "left": dpadLeft.control = button; break;
						case "up": dpadUp.control = button; break;
						case "right": dpadRight.control = button; break;

						case "start": menu.control = button; break;
						case "select": back.control = button; break;

						case "leftShoulder": bumperLeft.control = button; break;
						case "rightShoulder": bumperRight.control = button; break;

						case "leftStickPress": joystickButtonLeft.control = button; break;
						case "rightStickPress": joystickButtonRight.control = button; break;

						// axis using button type
						case "leftTrigger": triggerLeft.control = button; break;
						case "rightTrigger": triggerRight.control = button; break;
					}
				}
				else if (control is StickControl stick)
				{
					switch (control.name)
					{
						case "leftStick": joystickLeft.control = stick; break;
						case "rightStick": joystickRight.control = stick; break;
					}
				}
			}
		}

		internal void Update()
		{
			//TestMappings();

			button0.Update();
			button1.Update();
			button2.Update();
			button3.Update();

			dpadDown.Update();
			dpadLeft.Update();
			dpadUp.Update();
			dpadRight.Update();

			menu.Update();
			back.Update();

			bumperLeft.Update();
			bumperRight.Update();
			triggerLeft.Update();
			triggerRight.Update();

			joystickButtonLeft.Update();
			joystickButtonRight.Update();
			joystickLeft.Update();
			joystickRight.Update();
		}

		public void SetRumble(float lowFrequency, float highFrequency)
		{
			if (dualMotorRumble != null)
			{
				dualMotorRumble.SetMotorSpeeds(lowFrequency, highFrequency);
			}
		}

		/*private void TestMappings()
		{
			// NOTE: test mapping code
			var inputControls = device.allControls;
			foreach (var control in inputControls)
			{
				string path = control.path;
				//Debug.Log($"Control: '{control.name}' Path:'{path}' Type:'{control.GetType()}'");

				if (control is ButtonControl button)
				{
					if (button.wasPressedThisFrame) Debug.Log($"Button: '{control.name}' Path:'{path}'");
					if (button.value != 0) Debug.Log($"Axis: '{control.name}' Path:'{path}' Value:'{button.value}'");// IDK why this is an button but ok Unity
				}
				else if (control is StickControl stick)
				{
					var value = stick.value;
					if (value.magnitude != 0) Debug.Log($"Stick: '{control.name}' Path:'{path}'");
				}
			}
		}*/
	}
}
#endif