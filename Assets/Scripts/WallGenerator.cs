using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirections);
        foreach (var wall in basicWallPositions)
        {
            tileMapVisualizer.paintSingleBasicWall(wall);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) 
        {
            foreach (var direction in directionList)
            {
                var neighbourPos = position + direction;
                if(floorPositions.Contains(neighbourPos) == false)
                {
                    wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;
    }
}

