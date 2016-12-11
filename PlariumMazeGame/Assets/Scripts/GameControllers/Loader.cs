using UnityEngine;
using System.Collections;

// Instantiates our GameManager prefab
public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
