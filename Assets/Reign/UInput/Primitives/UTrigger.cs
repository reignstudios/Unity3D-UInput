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
	public struct UTrigger
	{
		public readonly string name;
		public static float tolerance = 0.1f;
		public float value;

		#if ENABLE_INPUT_SYSTEM
		public ButtonControl control;

		public UTrigger(ButtonControl control, string name)
		{
			this.control = control;
			this.name = name;
			value = 0;
		}

		internal void Update()
		{
			if (control != null) Update(control.value);
		}
		#else
		public readonly string mapName;
		public readonly float axisOffset, axisDiv, delayUntilNotEqual;
		private bool hasStarted;

		public UTrigger(string mapName, string name)
		: this(mapName, name, 0, 1, 0)
		{}

		public UTrigger(string mapName, string name, float axisOffset, float axisDiv, float delayUntilNotEqual)
		{
			this.mapName = mapName;
			this.name = name;
			this.axisOffset = axisOffset;
			this.axisDiv = axisDiv;
			this.delayUntilNotEqual = delayUntilNotEqual;
			hasStarted = false;
			value = 0;
		}

		internal void Update()
		{
			if (mapName != null)
			{
				float value = Input.GetAxisRaw(mapName);
				if (value != delayUntilNotEqual) hasStarted = true;
				if (hasStarted) Update((value + axisOffset) / axisDiv);
			}
		}
		#endif

		private void Update(float newValue)
		{
			if (newValue <= tolerance) newValue = 0;
			value = newValue;
		}
	}
}
