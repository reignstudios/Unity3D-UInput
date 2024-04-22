using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reign;

public class TestUInput : MonoBehaviour
{
	private KeyCode[] keyCodes;
	private string lastValue;

	private void Start()
	{
		keyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		lastValue = string.Empty;
	}

	private void Update()
	{
		// mouse
		if (UInput.GetMouseButtonDown(0)) lastValue = "Mouse Left Click";
		if (UInput.GetMouseButtonDown(1)) lastValue = "Mouse Right Click";
		if (UInput.GetMouseButtonDown(2)) lastValue = "Mouse Middle Click";

		// keyboard
		foreach (var keyCode in keyCodes)
		{
			if (UInput.GetKeyDown(keyCode)) lastValue = "Key: " + keyCode.ToString();
		}
	}

	private void OnGUI()
	{
		GUI.TextArea(new Rect(0, 0, 128, 32), lastValue);
	}
}
