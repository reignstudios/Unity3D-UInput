using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Reign
{
	public struct UButton
	{
		public ButtonControl control;
		public bool on, down, up;

		internal void Update()
		{
			if (control == null) return;

			bool pressed = control.isPressed;
			down = false;
			up = false;
			if (on != pressed)
			{
				on = pressed;
				if (pressed) down = true;
				else up = true;
			}
		}
	}
}
#endif
