using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class PlayButton : MonoBehaviour
{

    public Button playButton;
    public InputField nameInput;

    void Start()
    {
        playButton = GetComponent<Button>();
        nameInput = GetComponent<InputField>();
        playButton.onClick.AddListener(StartMazeScene);
    }

    void StartMazeScene()
    {
        Application.LoadLevel("MazeScene");
    }

}

