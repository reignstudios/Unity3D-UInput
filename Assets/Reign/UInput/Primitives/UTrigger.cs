using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Reign
{
	public struct UTrigger
	{
		public static float tolerance = 0.1f;

		public ButtonControl control;
		public float value;

		internal void Update()
		{
			if (control == null) return;

			float newValue = control.value;
			if (newValue <= tolerance) newValue = 0;
			value = newValue;
		}
	}
}
#endif
