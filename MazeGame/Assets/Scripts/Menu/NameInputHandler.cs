using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class NameInputHandler : MonoBehaviour {

    InputField nameInput;

	void Start ()
    {
        nameInput = GetComponent<InputField>();
        nameInput.onValueChanged.AddListener(ChangeName);
	}

    private void ChangeName(string arg0)
    {
        PlayerPreferences.playerName = nameInput.text;
    }
}

