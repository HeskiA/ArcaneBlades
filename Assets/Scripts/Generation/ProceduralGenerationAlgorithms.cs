using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
        var currentPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            currentPosition += Direction2D.GetRandomDirection();
            path.Add(currentPosition);
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int> { startPosition };
        var direction = Direction2D.GetRandomDirection();
        var currentPosition = startPosition;

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> CardinalDirections { get; } = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public static List<Vector2Int> DiagonalDirections { get; } = new List<Vector2Int>
    {
        Vector2Int.up + Vector2Int.right,
        Vector2Int.down + Vector2Int.right,
        Vector2Int.down + Vector2Int.left,
        Vector2Int.up + Vector2Int.left,
    };

    public static List<Vector2Int> EightDirections { get; } = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.up + Vector2Int.right,
        Vector2Int.right,
        Vector2Int.down + Vector2Int.right,
        Vector2Int.down,
        Vector2Int.down + Vector2Int.left,
        Vector2Int.left,
        Vector2Int.up + Vector2Int.left,
    };

    private static System.Random random = new System.Random();

    public static Vector2Int GetRandomDirection()
    {
        return CardinalDirections[random.Next(0, CardinalDirections.Count)];
    }
}
