using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace Reign
{
    public class UController : MonoBehaviour
    {
		private static ReadOnlyArray<InputDevice> devices;

		static UController()
		{
			//devices = InputSystem.devices;
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
						if (button.wasPressedThisFrame)
						{
							if (button.path.Contains("Stick") || button.path.Contains("Trigger")) continue;
							Debug.Log($"'{button.name}'");
						}
					}
				}
			}
		}
	}
}