using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

//Encapsulates layout manager (GameManager is actual gameobject)
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public LayoutManager layoutController;

    void InitGame()
    {
        layoutController.SetupScene();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        layoutController = GetComponent<LayoutManager>();
        InitGame();
    }

    // Increases movement speed of enemies by percentage
    public void IncrementMsOnPickUp(float percentage)
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI e in enemies)
        {
            e.moveSpeed *= (1 + percentage);
        }
    }

    void GoBackToMenu()
    {
        Application.LoadLevel("GameMenu");
    }
}
