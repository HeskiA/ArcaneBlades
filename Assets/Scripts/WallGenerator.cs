using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirections);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirections);
        CreateBasicWalls(tileMapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tileMapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions) 
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.eightDirections)
            {
                var neighbourPos = position + direction;
                if (floorPositions.Contains(neighbourPos))
                {
                    neighboursBinary += '1';
                }
                else
                {
                    neighboursBinary += '0';
                }
            }
            tileMapVisualizer.paintSingleCornerWall(position, neighboursBinary);

        }
    }

    private static void CreateBasicWalls(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.cardinalDirections)
            {
                var neighbourPos = position + direction;
                if(floorPositions.Contains(neighbourPos))
                {
                    neighboursBinary += '1';
                }else
                {
                    neighboursBinary += '0';
                }
            }
            tileMapVisualizer.paintSingleBasicWall(position, neighboursBinary);
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

