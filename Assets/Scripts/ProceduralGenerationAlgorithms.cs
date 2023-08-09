using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var prevPosition = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            var newPosition = prevPosition + Direction2D.getRandomDirection();
            path.Add(newPosition);
            prevPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLenght)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.getRandomDirection();
        var currentPos = startPosition;
        corridor.Add(currentPos);
        for (int i = 0;i < corridorLenght;i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1), // UP
        new Vector2Int(1,0), // RIGHT
        new Vector2Int(0,-1), // DOWN
        new Vector2Int(-1,0), // LEFT
    };

    public static Vector2Int getRandomDirection()
    {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}
