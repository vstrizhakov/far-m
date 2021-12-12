using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    public static void SetTileAreaVisibility(this Tilemap tilemap, Vector2Int position, Vector2Int size, bool isVisible)
    {
        for (int i = position.x; i < position.x + size.x; i++)
        {
            for (int j = position.y; j < position.y + size.y; j++)
            {
                var currentCell = new Vector3Int(i, j, 0);
                SetTileVisibility(tilemap, currentCell, isVisible);
            }
        }
    }

    public static void SetTileVisibility(this Tilemap tilemap, Vector3Int position, bool isVisible)
    {
        var tile = tilemap.GetTile<Tile>(position);
        if (tile != null)
        {
            tile.color = new Color(1, 1, 1, isVisible ? 1 : 0);
        }
        tilemap.RefreshTile(position);
    }
}
