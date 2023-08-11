using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDugneonGen : SimpleRandomWalkMapGenerator
{
    [SerializeField] private int corridorLenght = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f,1)] private float roomPercent = 0.8f;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors =  CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPositions);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        

        CreateRoomsAtDeadEnd(deadEnds, roomPos);

        floorPositions.UnionWith(roomPos);

        for (int i = 0; i < corridors.Count;i++)
        {
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for (int i = 1;i < corridor.Count;i++)
        {
            for (int x = -1;x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                }
            }
        }
        return newCorridor;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParams, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var pos in floorPositions) 
        {
            int neighboursCount = 0;

            foreach (var direction in Direction2D.cardinalDirections) {
                if(floorPositions.Contains(direction + pos)) {
                    neighboursCount++;
                }
            }
            if(neighboursCount == 1) {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy( x=> Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach(var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParams, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPosition;
        potentialRoomPositions.Add(currentPos);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++) 
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPos, corridorLenght);
            corridors.Add(corridor);
            currentPos = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPos);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
