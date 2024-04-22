using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reign;

public class TestUInput : MonoBehaviour
{
	private KeyCode[] keyCodes;
	private string lable;

	private void Start()
	{
		keyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		lable = string.Empty;

		#if ENABLE_INPUT_SYSTEM
		UInput.DevicesChangedCallback += UInput_DevicesChangedCallback;
		#endif
	}

	#if ENABLE_INPUT_SYSTEM
	private void OnDestroy()
	{
		UInput.DevicesChangedCallback -= UInput_DevicesChangedCallback;
	}
	#endif

	private void UInput_DevicesChangedCallback()
	{
		lable = "Devices Changed";
	}

	private void Update()
	{
		// mouse
		if (UInput.GetMouseButtonDown(0)) lable = "Mouse Left Click";
		if (UInput.GetMouseButtonDown(1)) lable = "Mouse Right Click";
		if (UInput.GetMouseButtonDown(2)) lable = "Mouse Middle Click";

		// keyboard
		foreach (var keyCode in keyCodes)
		{
			if (UInput.GetKeyDown(keyCode)) lable = "Key: " + keyCode.ToString();
		}

		// controller
		#if ENABLE_INPUT_SYSTEM
		foreach (var gamepad in UInput.gamepads)
		{
			if (gamepad.button0.down) lable = "Gamepad: Button0";
			if (gamepad.button1.down) lable = "Gamepad: Button1";
			if (gamepad.button2.down) lable = "Gamepad: Button2";
			if (gamepad.button3.down) lable = "Gamepad: Button3";

			if (gamepad.dpadUp.down) lable = "Gamepad: DPadUp";
			if (gamepad.dpadDown.down) lable = "Gamepad: DPadDown";
			if (gamepad.dpadLeft.down) lable = "Gamepad: DPadLeft";
			if (gamepad.dpadRight.down) lable = "Gamepad: DPadRight";

			if (gamepad.menu.down) lable = "Gamepad: Menu";
			if (gamepad.back.down) lable = "Gamepad: Back";

			if (gamepad.bumperLeft.down) lable = "Gamepad: BumperLeft";
			if (gamepad.bumperRight.down) lable = "Gamepad: BumperRight";

			if (gamepad.triggerLeft.value != 0) lable = "Gamepad: TriggerLeft: " + gamepad.triggerLeft.value.ToString();
			if (gamepad.triggerRight.value != 0) lable = "Gamepad: TriggerRight: " + gamepad.triggerRight.value.ToString();
			gamepad.SetRumble(gamepad.triggerLeft.value, gamepad.triggerRight.value);

			if (gamepad.joystickButtonLeft.down) lable = "Gamepad: JoystickButtonLeft";
			if (gamepad.joystickButtonRight.down) lable = "Gamepad: JoystickButtonRight";

			if (gamepad.joystickLeft.value.magnitude != 0) lable = "Gamepad: TriggerLeft: " + gamepad.joystickLeft.value.ToString();
			if (gamepad.joystickRight.value.magnitude != 0) lable = "Gamepad: TriggerRight: " + gamepad.joystickRight.value.ToString();
		}
		#endif
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(20, 20, 256, 32), lable);
	}
}
