using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BuildingsManager : MonoBehaviour
{
    public Vector2Int areaSize;
    public GameObject prefab;
    public Tilemap tilemap;
    public GameObject wastelandCanvas;

    private BuildingsFactory buildingsFactory;
    private WastelandUIHandler wastelandUIHandler;
    private List<GameObject> _fields;
    private int[,] _area;

    public void Awake()
    {
        buildingsFactory = GetComponent<BuildingsFactory>();
        wastelandUIHandler = GetComponent<WastelandUIHandler>();

        _fields = new List<GameObject>();
        _area = new int[areaSize.x, areaSize.y];

        for (int i = 0; i < _area.GetLength(0); i++)
        {
            for (int j = 0; j < _area.GetLength(1); j++)
            {
                var position = new Vector3Int(i, j, 0);
                tilemap.SetTileVisibility(position, false);
            }
        }
    }

    public void Start()
    {
        var context = buildingsFactory.contextManager.context;
        var buildings = context.buildings;
        foreach (var building in buildings)
        {
            var specs = buildingsFactory.GetSpecifications(building.Type);
            if (specs.HasValue)
            {
                AddField(specs.Value, building.Position, building);
            }
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cell = tilemap.WorldToCell(mousePosition);
            Building selectedBuilding = null;
            if (IsInBounds(new Vector2Int(cell.x, cell.y)))
            {
                var hashCode = _area[cell.x, cell.y];
                if (hashCode != 0)
                {
                    foreach (var gameObject in _fields)
                    {
                        var building = gameObject.GetComponent<BuildingBehaviour>().building;
                        if (building.GetHashCode() == hashCode)
                        {
                            selectedBuilding = building;
                            break;
                        }
                    }
                }
            }
            wastelandCanvas.GetComponent<Canvas>().enabled = selectedBuilding is WastelandBuilding;
            if (selectedBuilding is WastelandBuilding wastelandBuilding)
            {
                wastelandUIHandler.building = wastelandBuilding;
            }
        }
    }

    public bool AddField(BuildingSpecifications buildingSpecifications, Vector2Int position)
    {
        var added = false;
        var size = buildingSpecifications.size;
        if (IsAvailable(position, size))
        {
            var building = buildingsFactory.Create(position, buildingSpecifications.buildingType);

            for (int i = position.x; i < position.x + size.x; i++)
            {
                for (int j = position.y; j < position.y + size.y; j++)
                {
                    _area[i, j] = building.GetHashCode();
                }
            }

            var gameObject = CreateGameObject(buildingSpecifications, position, building);
            tilemap.SetTileAreaVisibility(position, size, true);

            _fields.Add(gameObject);
            added = true;
        }
        return added;
    }

    public bool AddField(BuildingSpecifications buildingSpecifications, Vector2Int position, Building building)
    {
        var added = false;
        var size = buildingSpecifications.size;
        if (IsAvailable(position, size))
        {
            for (int i = position.x; i < position.x + size.x; i++)
            {
                for (int j = position.y; j < position.y + size.y; j++)
                {
                    _area[i, j] = building.GetHashCode();
                }
            }

            var gameObject = CreateGameObject(buildingSpecifications, position, building);
            tilemap.SetTileAreaVisibility(position, size, true);

            _fields.Add(gameObject);
            added = true;
        }
        return added;
    }

    public bool IsOccupied(Vector2Int position)
    {
        return _area[position.x, position.y] != 0;
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

    public int[,] GetArea()
    {
        var width = _area.GetLength(0);
        var height = _area.GetLength(1);
        var area = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                area[i, j] = _area[i, j];
            }
        }
        return area;
    }

    private GameObject CreateGameObject(BuildingSpecifications buildingSpecifications, Vector2Int position, Building building)
    {
        var gameObject = Instantiate(prefab);
        var block = gameObject.GetComponent<Block>();
        block.position = tilemap.CellToWorld(new Vector3Int(position.x, position.y, 0));
        block.sprite = buildingSpecifications.sprite;
        block.size = buildingSpecifications.size;

        var fieldBehaviour = gameObject.GetComponent<BuildingBehaviour>();
        fieldBehaviour.contextManager = buildingsFactory.contextManager;
        fieldBehaviour.building = building;

        return gameObject;
    }
}
