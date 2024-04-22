using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Reign
{
	public struct UJoystick
	{
		public static float tolerance = 0.1f;

		public StickControl control;
		public Vector2 value;

		internal void Update()
		{
			if (control == null) return;

			var newValue = control.value;
			if (newValue.magnitude <= tolerance) newValue = Vector2.zero;
			value = newValue;
		}
	}
}
#endif