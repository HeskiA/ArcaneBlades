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

    public static List<BoundsInt> BinaryPartitioning(BoundsInt spaceToSplit, int minimumWidth, int minimumHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minimumHeight && room.size.x >= minimumWidth) 
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minimumHeight * 2)
                    {
                        SplitHorizontaly(minimumHeight,roomsQueue, room);
                    }
                    else if (room.size.x >= minimumWidth * 2)
                    {
                        SplitVerticaly(minimumWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minimumWidth * 2)
                    {
                        SplitVerticaly(minimumWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minimumHeight * 2)
                    {
                        SplitHorizontaly(minimumHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVerticaly(int minimumWidth,  Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontaly( int minimumHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

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
