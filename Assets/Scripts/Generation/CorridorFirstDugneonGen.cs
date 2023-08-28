using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorFirstDugneonGen : MonoBehaviour
{
    [SerializeField] protected TileMapVisualizer tileMapVisualizer = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] protected RandomWalkParams randomWalkParams;
    [SerializeField] private int corridorLenght = 14, corridorCount = 5;
    [SerializeField][Range(0.1f, 1)] private float roomPercent = 0.8f;
    public Dictionary<Vector2Int, int> distancesDict = new Dictionary<Vector2Int, int>();
    public Dictionary<Vector2Int, List<Vector2Int>> roomDict = new Dictionary<Vector2Int, List<Vector2Int>>();
    public GameObject enemyPrefab;
    public GameObject nextLevelPrefab;

    protected void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    public void clearMap()
    {
        tileMapVisualizer.Clear();
    }

    public void CorridorFirstGeneration()
    {
        distancesDict.Clear();
        roomDict.Clear();
        List<Vector2Int> floorPositions = new List<Vector2Int>();
        List<Vector2Int> potentialRoomPositions = new List<Vector2Int>();
        List<Vector2Int> roomList = new List<Vector2Int>();
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        List<Vector2Int> roomPos = CreateRooms(potentialRoomPositions, roomList, roomDict);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions, roomList);

        CreateRoomsAtDeadEnd(deadEnds, roomPos, roomDict);

        floorPositions.AddRange(roomPos);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.AddRange(corridors[i]);
        }
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);

        CalculateDistancesFromOrigin(roomList, distancesDict);

        EnemySpawning.nextLevelPrefab = nextLevelPrefab;
        EnemySpawning.myPrefab = enemyPrefab;
        EnemySpawning.SpawnEnemies(distancesDict, roomDict);
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, List<Vector2Int> roomFloors, Dictionary<Vector2Int, List<Vector2Int>> roomDict)
    {
        foreach (var position in deadEnds)
        {
            if (!roomFloors.Contains(position))
            {
                var room = RunRandomWalk(randomWalkParams, position);
                roomFloors.AddRange(room);
                if (!roomDict.ContainsKey(position)) roomDict.Add(position, room);

            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(List<Vector2Int> floorPositions, List<Vector2Int> roomList)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var pos in floorPositions)
        {
            int neighboursCount = 0;

            foreach (var direction in Direction2D.CardinalDirections)
            {
                if (floorPositions.Contains(direction + pos))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(pos);
                roomList.Add(pos);
            }
        }
        return deadEnds;
    }

    private List<Vector2Int> CreateRooms(List<Vector2Int> potentialRoomPositions, List<Vector2Int> roomList, Dictionary<Vector2Int, List<Vector2Int>> roomDict)
    {
        List<Vector2Int> roomPositions = new List<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParams, roomPosition);
            roomPositions.AddRange(roomFloor);
            roomList.Add(roomPosition);
            if (!roomDict.ContainsKey(roomPosition)) roomDict.Add(roomPosition, roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(List<Vector2Int> floorPositions, List<Vector2Int> potentialRoomPositions)
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
            floorPositions.AddRange(corridor);
        }
        return corridors;
    }

    private void CalculateDistancesFromOrigin(List<Vector2Int> roomList, Dictionary<Vector2Int, int> distancesDict)
    {
        Vector2Int origin = new Vector2Int(0, 0);

        foreach (Vector2Int roomCenter in roomList)
        {
            int distance = Mathf.Abs(roomCenter.x - origin.x) + Mathf.Abs(roomCenter.y - origin.y);
            if (!distancesDict.ContainsKey(roomCenter))
            {
                distancesDict.Add(roomCenter, distance);
            }
        }
    }

    protected List<Vector2Int> RunRandomWalk(RandomWalkParams parameters, Vector2Int position)
    {
        var currentPositions = position;
        List<Vector2Int> floorPositions = new List<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.RandomWalk(currentPositions, parameters.walkLenght);
            floorPositions.AddRange(path);
            if (parameters.startRandomlyEachIteration)
            {
                currentPositions = floorPositions[Random.Range(0, floorPositions.Count)];
            }
        }
        return floorPositions;
    }

}
