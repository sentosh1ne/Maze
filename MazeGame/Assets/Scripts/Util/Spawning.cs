using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Provides tools to spawn objects in the game
/// </summary>
public static class Spawning   {

    /// <summary>
    /// Spawns tile in random unoccupied positions
    /// </summary>
    /// <param name="tile">Tile to spawn</param>
    /// <param name="occupied">Occupied positions</param>
    /// <param name="positions">Possible positions</param>
    public static void SpawnInUnoccupied(GameObject tile, List<Vector3> occupied, List<Vector3> positions)
    {
        Vector3 toSpawnPosition = PositioningUtil.GeneratePosition(positions);

        while (occupied.Contains(toSpawnPosition))
        {
            toSpawnPosition = PositioningUtil.GeneratePosition(positions);
        }

        Object.Instantiate(tile, toSpawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Spawns range of tiles (game objects) in random positions
    /// </summary>
    /// <param name="tile">Tile to spawn</param>
    /// <param name="min">Floor of range</param>
    /// <param name="max">Celling of range</param>
    /// <param name="positions">Possible positions</param>
    /// <returns></returns>
    public static List<Vector3> SpawnTilesRandomly(GameObject tile, int min, int max, List<Vector3> positions)
    {
        int tilesCount = Random.Range(min, max + 1);
        List<Vector3> created = new List<Vector3>();
        for (int i = 0; i < tilesCount; i++)
        {
            Vector3 randomPosition = PositioningUtil.GeneratePosition(positions);
            created.Add(randomPosition);
            Object.Instantiate(tile, randomPosition, Quaternion.identity);
        }

        return created;
    }
}
