using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Direction2D
    {
        public static List<Vector2Int> generalDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(1, 0), // Right
            new Vector2Int(-1, 0), //Left
        };
    }
}
