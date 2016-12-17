using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
/// <summary>
/// Responsible for generating layout objects
/// </summary>
public class LayoutManager : MonoBehaviour
{

    #region Variables
    public int columns = 13;
    public int rows = 13;
    public int minWalls = 20;
    public int maxWalls = 25;
    public int maxCoins = 10;
    public static int coinsCount = 0;
    public GameObject player;
    public GameObject ground;
    public GameObject zombie;
    public GameObject mummy;
    public GameObject coin;
    public GameObject wall;

    //Corrdinates in the grid
    [HideInInspector]
    public static List<Vector3> positions = new List<Vector3>();
    public float coinSpawnRate = 4f;
    [HideInInspector]
    public static List<Vector3> wallsPositions = new List<Vector3>();

    //Parent for objects, so we don't overflow hierarchy
    private Transform layoutHolder;
    #endregion

    //Creates initial setup of the maze
    public void SetupScene()
    {
        SetupGrid();
       
        PositioningUtil.InitializePositions(positions, columns, rows);

        Instantiate(ground, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        wallsPositions = Spawning.SpawnTilesRandomly(wall, minWalls, maxWalls,positions);

        Spawning.SpawnInUnoccupied(player, wallsPositions,positions);
        Spawning.SpawnInUnoccupied(zombie, wallsPositions,positions);
    }

    //Instantiates grid with layout prefabs
    void SetupGrid()
    {
        layoutHolder = new GameObject("Layout").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = ground;
                if (isOuterTile(x, y))
                {
                    toInstantiate = wall;
                }
                GameObject tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                tileInstance.transform.SetParent(layoutHolder);
            }
        }
    }

    private bool isOuterTile(int x, int y)
    {
        return (x == -1 || x == columns || y == -1 || y == rows);
    }

   void SpawnCoin()
    {
        if (coinsCount == maxCoins)
        {
            return;
        }

        Vector3 coinSpawn = PositioningUtil.GeneratePosition(positions);

        while (wallsPositions.Contains(coinSpawn))
        {
            coinSpawn = PositioningUtil.GeneratePosition(positions);
        }
        GameObject spawned = Instantiate(coin, coinSpawn, Quaternion.identity) as GameObject;
        spawned.transform.SetParent(layoutHolder);
        coinsCount++;
    }

    void Start()
    {
        InvokeRepeating("SpawnCoin", 0.5f, coinSpawnRate);
    }

}
