using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reign;

public class TestUInput : MonoBehaviour
{
	private KeyCode[] keyCodes;

	private void Start()
	{
		keyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
	}

	private void Update()
	{
		// mouse
		if (UInput.GetMouseButtonDown(0)) Debug.Log("Mouse Left Click");
		if (UInput.GetMouseButtonDown(1)) Debug.Log("Mouse Right Click");
		if (UInput.GetMouseButtonDown(2)) Debug.Log("Mouse Middle Click");

		// keyboard
		foreach (var keyCode in keyCodes)
		{
			if (UInput.GetKeyDown(keyCode)) Debug.Log("Key: " + keyCode.ToString());
		}
	}
}
