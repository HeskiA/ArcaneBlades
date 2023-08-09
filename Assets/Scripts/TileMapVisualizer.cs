using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap wallTileMap;

    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTop;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap TileMap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(TileMap,Tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    internal void paintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap,wallTop, position);
    }
}
