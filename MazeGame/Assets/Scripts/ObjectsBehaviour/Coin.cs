using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public GameObject zombie;
    public GameObject mummy;

    private ScoreManager scoreManager;
    private AudioSource audioSource;
    private Collider2D collider;
    private Renderer renderer;
    private GameManager gameManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        //Ignore collisions with coins
        Physics2D.IgnoreLayerCollision(0, 9);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            LayoutManager.coinsCount--;
            scoreManager.AddScore(1);

            audioSource.PlayOneShot(audioSource.clip);
            renderer.enabled = false;
            collider.enabled = false;
            gameManager.IncrementMsOnPickUp(0.05f);
            Destroy(gameObject, audioSource.clip.length);

            if (scoreManager.toSpawnZombie())
                Spawning.SpawnInUnoccupied(zombie, LayoutManager.wallsPositions, LayoutManager.positions);

            if (scoreManager.toSpawnMummy())
                Spawning.SpawnInUnoccupied(mummy, LayoutManager.wallsPositions, LayoutManager.positions);
        }
    }
}
