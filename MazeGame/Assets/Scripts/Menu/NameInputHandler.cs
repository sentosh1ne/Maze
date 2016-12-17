using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class NameInputHandler : MonoBehaviour {
	
    //Reference to input field	
    InputField nameInput;

	void Start ()
       {
        nameInput = GetComponent<InputField>();
        nameInput.onValueChanged.AddListener(ChangeName);
	}
    
    // changing player preferences	
    private void ChangeName(string arg0)
    {
        PlayerPreferences.playerName = nameInput.text;
    }
}

