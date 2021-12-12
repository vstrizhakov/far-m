using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UIHandler : MonoBehaviour
{
    private BlockPlacementManager blockPlacementManager;

    private void Start()
    {
        blockPlacementManager = GetComponent<BlockPlacementManager>();
    }

    public void OnSpawnClick()
    {
        var field = new Field()
        {
            size = new Vector2Int(2, 5),
        };
        blockPlacementManager.field = field;
        blockPlacementManager.isEnabled = !blockPlacementManager.isEnabled;
    }
}
