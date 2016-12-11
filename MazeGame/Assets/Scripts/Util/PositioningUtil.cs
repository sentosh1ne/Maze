using UnityEngine;
using System.Collections.Generic;

public static class PositioningUtil{

    /// <summary>
    /// Creates grid of positions
    /// </summary>
    /// <param name="positions">Grid to fill</param>
    /// <param name="columns">Number of columns in the grid</param>
    /// <param name="rows">Number of rows in the grid<</param>
    public static void InitializePositions(List<Vector3> positions, int columns, int rows)
    {
        positions.Clear();

        for (int x = 1; x < columns; x++)
        {
            for (int y = 1; y < rows; y++)
            {
                positions.Add(new Vector3(x, y, 0f));
            }
        }
    }
   /// <summary>
   /// Generates random position in the grid
   /// </summary>
   /// <param name="positions">Possible positions</param>
   /// <returns>(X,Y,Z) of a position</returns>
    public static Vector3 GeneratePosition(List<Vector3> positions)
    {
        int index = Random.Range(0, positions.Count);
        Vector3 position = positions[index];
        positions.RemoveAt(index);
        return position;
    }
}
