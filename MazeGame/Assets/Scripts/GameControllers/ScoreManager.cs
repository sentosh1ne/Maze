using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public float score = 0;
    public Text scoreText;

    public void AddScore(float addition)
    {
        score += addition;
        UpdateScore();
    }

    public bool toSpawnZombie()
    {
        if (score == 5)
        {
            return true;
        }

        return false;
    }

    public bool toSpawnMummy()
    {
        if (score == 10)
        {
            return true;
        }
        return false;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score : " + (int)score;
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

}
