using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(List<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.CardinalDirections);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.DiagonalDirections);
        CreateBasicWalls(tileMapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tileMapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileMapVisualizer tileMapVisualizer, List<Vector2Int> cornerWallPositions, List<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.EightDirections)
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

    private static void CreateBasicWalls(TileMapVisualizer tileMapVisualizer, List<Vector2Int> basicWallPositions, List<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.CardinalDirections)
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
            tileMapVisualizer.paintSingleBasicWall(position, neighboursBinary);
        }
    }

    private static List<Vector2Int> FindWallsInDirections(List<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        List<Vector2Int> wallPositions = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPos = position + direction;
                if (!floorPositions.Contains(neighbourPos))
                {
                    wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;
    }
}
