using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField] private int iterations = 10;
    [SerializeField] public int walkLenght = 10;
    [SerializeField] public bool startRandomlyEachIteration = true;

    [SerializeField] private TileMapVisualizer tileMapVisualizer;


    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var curentPositions = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.simpleRandomWalk(curentPositions, walkLenght);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration)
            {
                curentPositions = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
