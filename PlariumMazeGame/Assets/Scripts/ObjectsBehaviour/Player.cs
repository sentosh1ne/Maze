using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

/// <summary>
/// Interaction of player rwith other game objects / mechanics
/// </summary>
public class Player : MonoBehaviour
{
    //Time spent in the maze
    private float elapsedTime = 0f;

    //Delay before loading back to menu
    private float loadDelay = 2f;

    // Delay before player destruction
    private float destroyDelay = 1.5f;
    private ScoreManager scoreManager;
    private Renderer renderer;
    private GameManager gameManager;

    void OnCollisionEnter2D(Collision2D other)
    {
        TouchedEnemy(other.gameObject.tag, other.gameObject);
    }


    //Interaction on collidign with enemy
    private void TouchedEnemy(string reason,GameObject other)
    {
        bool isEnemy = (reason == "Mummy" || reason == "Zombie");

        if (!isEnemy)
            return;

        if (reason == "Mummy")
            scoreManager.score = 0;

        //Trigger attack animation of the enemy
        other.GetComponent<Animator>().SetTrigger("Attack");

        float attackDelay = CurrentAnimDuration(other.gameObject);
        OnPlayerDestruction(reason);
        Destroy(gameObject, attackDelay * destroyDelay);
        gameManager.Invoke("GoBackToMenu",loadDelay);
    }

    // Serialize info on player losing the game
    private void OnPlayerDestruction(string reason)
    {
        Record r = new Record(PlayerPreferences.playerName, (int)scoreManager.score, elapsedTime, DateTime.Now, reason);
        RecordsSaver recordSaver = new RecordsSaver();
        if (!File.Exists(PlayerPreferences.recordsSaveLocation))
        {
            recordSaver.CreateNewRecordSource(r);
        }
        else
        {
            recordSaver.AppendRecord(r);
        }
    }

   
    float CurrentAnimDuration(GameObject tile)
    {
      return tile.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Escape))
        {
            OnPlayerDestruction("ESC");
            Application.LoadLevel("GameMenu");
        }
    }

    void Start()
    {
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

}
