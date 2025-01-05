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
	public struct UJoystick
	{
		public readonly string name;
		public static float tolerance = 0.1f;
		public Vector2 value;

		#if ENABLE_INPUT_SYSTEM
		public readonly StickControl control;

		public UJoystick(StickControl control, string name)
		{
			this.control = control;
			this.name = name;
			value = Vector2.zero;
		}

		internal void Update()
		{
			if (control != null) Update(control.value);
		}
		#else
		public readonly string mapName_AxisX, mapName_AxisY;
		public readonly bool invertX, invertY;

		public UJoystick(string mapName_AxisX, string mapName_AxisY, string name)
		: this(mapName_AxisX, mapName_AxisY, name, false, true)
		{}

		public UJoystick(string mapName_AxisX, string mapName_AxisY, string name, bool invertX, bool invertY)
		{
			this.mapName_AxisX = mapName_AxisX;
			this.mapName_AxisY = mapName_AxisY;
			this.name = name;
			this.invertX = invertX;
			this.invertY = invertY;
			value = Vector2.zero;
		}

		internal void Update()
		{
			if (mapName_AxisX != null && mapName_AxisY != null)
			{
				float valueX = Input.GetAxisRaw(mapName_AxisX);
				float valueY = Input.GetAxisRaw(mapName_AxisY);
				if (invertX) valueX = -valueX;
				if (invertY) valueY = -valueY;
				Update(new Vector2(valueX, valueY));
			}
		}
		#endif

		private void Update(Vector2 newValue)
		{
			if (newValue.magnitude <= tolerance) newValue = Vector2.zero;
			value = newValue;
		}
	}
}