using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BlockPlacementManager : MonoBehaviour
{
    public Tilemap freeTilemap;
    public Tilemap occupiedTilemap;
    public Tilemap placementTilemap;
    public FieldManager fieldManager;
    public bool isEnabled;
    public Field field;

    private Vector3Int? _prevCell;
    private bool? _prevIsEnabled;
    private Field _prevField;

    private void Update()
    {
        var currentIsEnabled = isEnabled;
        if (_prevIsEnabled != currentIsEnabled)
        {
            var area = fieldManager.GetArea();
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    var isOccupied = area[i, j];
                    var position = new Vector3Int(i, j, 0);
                    freeTilemap.SetTileVisibility(position, !isOccupied && currentIsEnabled);
                    occupiedTilemap.SetTileVisibility(position, isOccupied && currentIsEnabled);
                    placementTilemap.SetTileVisibility(position, false);
                }
            }
            _prevIsEnabled = currentIsEnabled;
        }

        if (currentIsEnabled)
        {
            var mousePosition = Input.mousePosition;
            var gridLayout = placementTilemap.GetComponentInParent<GridLayout>();
            var position = Camera.main.ScreenToWorldPoint(mousePosition);
            position.z = 0;

            var currentCell = gridLayout.WorldToCell(position);
            if (currentCell != _prevCell)
            {
                if (_prevCell.HasValue && _prevField != null)
                {
                    var size = _prevField.size;
                    var prevCell = _prevCell.Value;
                    var position2D = new Vector2Int(prevCell.x, prevCell.y);
                    placementTilemap.SetTileAreaVisibility(position2D, size, false);
                }

                if (field != null)
                {
                    var size = field.size;
                    var position2D = new Vector2Int(currentCell.x, currentCell.y);
                    if (fieldManager.IsAvailable(position2D, size))
                    {
                        placementTilemap.SetTileAreaVisibility(position2D, size, true);
                    }
                    _prevField = field;
                }

                _prevCell = currentCell;
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && field != null)
            {
                var position2D = new Vector2Int(currentCell.x, currentCell.y);
                fieldManager.AddField(field, position2D);

                isEnabled = false;
            }
        }
    }
}
