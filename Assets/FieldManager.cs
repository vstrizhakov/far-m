using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    public Vector2Int areaSize;
    public GameObject prefab;
    public Tilemap tilemap;

    private List<GameObject> _fields;
    private bool[,] _area;

    public void Awake()
    {
        _fields = new List<GameObject>();
        _area = new bool[areaSize.x, areaSize.y];

        for (int i = 0; i < _area.GetLength(0); i++)
        {
            for (int j = 0; j < _area.GetLength(1); j++)
            {
                var position = new Vector3Int(i, j, 0);
                tilemap.SetTileVisibility(position, false);
            }
        }
    }

    public bool AddField(Field field, Vector2Int position)
    {
        var added = false;
        var size = field.size;
        if (IsAvailable(position, size))
        {
            for (int i = position.x; i < position.x + size.x; i++)
            {
                for (int j = position.y; j < position.y + size.y; j++)
                {
                    _area[i, j] = true;
                }
            }

            var gameObject = CreateGameObject(field, position);

            tilemap.SetTileAreaVisibility(position, size, true);

            _fields.Add(gameObject);
            added = true;
        }
        return added;
    }

    public bool IsOccupied(Vector2Int position)
    {
        return _area[position.x, position.y];
    }

    public bool IsAvailable(Vector2Int position, Vector2Int size)
    {
        var isAvailable = true;
        for (int i = position.x; i < position.x + size.x; i++)
        {
            for (int j = position.y; j < position.y + size.y; j++)
            {
                var currentPosition = new Vector2Int(i, j);
                if (!IsInBounds(currentPosition) || IsOccupied(currentPosition))
                {
                    isAvailable = false;
                    break;
                }
            }
        }
        return isAvailable;
    }

    public bool IsInBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < _area.GetLength(0)
            && position.y >= 0 && position.y < _area.GetLength(1);
    }

    public bool[,] GetArea()
    {
        var width = _area.GetLength(0);
        var height = _area.GetLength(1);
        var area = new bool[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                area[i, j] = _area[i, j];
            }
        }
        return area;
    }

    private GameObject CreateGameObject(Field field, Vector2Int position)
    {
        var gameObject = Instantiate(prefab);
        var block = gameObject.GetComponent<Block>();
        block.tilemap = tilemap;
        block.position = new Vector3Int(position.x, position.y, 0);

        var fieldBehaviour = gameObject.GetComponent<FieldBehaviour>();
        fieldBehaviour.field = field;

        return gameObject;
    }
}
