//#undef ENABLE_INPUT_SYSTEM

#if UINPUT_DISABLE_NEW
#undef ENABLE_INPUT_SYSTEM
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.Controls;
#endif

namespace Reign
{
	public struct UButton
	{
		public readonly string name;
		public bool on, down, up;

		#if ENABLE_INPUT_SYSTEM
		public readonly ButtonControl control;

		public UButton(ButtonControl control, string name)
		{
			this.control = control;
			this.name = name;
			on = false;
			down = false;
			up = false;
		}

		internal void Update()
		{
			if (control != null) UpdateInternal(control.isPressed);
		}
		#else
		public readonly string mapName;
		public readonly bool useAxis, negAxis;

		public UButton(string mapName, string name)
		: this(mapName, name, false, false)
		{}

		public UButton(string mapName, string name, bool useAxis, bool negAxis)
		{
			this.mapName = mapName;
			this.name = name;
			this.useAxis = useAxis;
			this.negAxis = negAxis;
			on = false;
			down = false;
			up = false;
		}

		internal void Update()
		{
			if (mapName == null) return;
			if (useAxis)
			{
				if (negAxis) UpdateInternal(Input.GetAxisRaw(mapName) < -.5f);
				else UpdateInternal(Input.GetAxisRaw(mapName) > .5f);
			}
			else
			{
				UpdateInternal(Input.GetButton(mapName));
			}
		}
		#endif

		private void UpdateInternal(bool pressed)
		{
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
