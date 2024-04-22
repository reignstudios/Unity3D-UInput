using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace Reign
{
    public class UController : MonoBehaviour
    {
		private static ReadOnlyArray<InputDevice> devices;
		private string lastValue;

		static UController()
		{
			//devices = InputSystem.devices;
		}

		private void Start()
		{
			lastValue = string.Empty;
		}

		private void Update()
		{
			devices = InputSystem.devices;

			foreach (var device in devices)
			{
				//Debug.Log($"Device: '{device.name}' {device.GetType()}");
				var controls = device.allControls;
				//controls[0].GetChildControl<Button>()
				foreach (var control in controls)
				{
					//Debug.Log($"Control: '{control.name}' '{control.GetType()}'");
					if (control is ButtonControl button)
					{
						if (button.name == "anyKey") continue;
						if (button.wasPressedThisFrame)
						{
							string path = button.path;
							if (path.Contains("Stick/") || path.Contains("Trigger/")) continue;
							lastValue = $"'{button.name}' '{path}'";

							if (device is IDualMotorRumble rumble) rumble.SetMotorSpeeds(1, 1);
						}
						else if (button.wasReleasedThisFrame)
						{
							if (device is IDualMotorRumble rumble) rumble.SetMotorSpeeds(0, 0);
						}
					}
					else if (control is StickControl stick)
					{
						var value = stick.value;
						if (value.magnitude != 0) lastValue = value.ToString();
					}
					/*else if (control is AxisControl axis)
					{
						lastValue = axis.value.ToString();
					}*/
				}
			}
		}

		private void OnGUI()
		{
			GUI.TextArea(new Rect(0, 64, 512, 32), lastValue);
		}
	}
}