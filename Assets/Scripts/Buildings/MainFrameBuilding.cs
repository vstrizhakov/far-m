using System;
using UnityEngine;

public class MainFrameBuilding : Building, ISemiconductorsStorage
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.MainFrame,
        size = new Vector2Int(3, 4),
        sprite = Resources.Load<Sprite>("main"),
    };

    public override BuildingType Type => BuildingType.MainFrame;

    public int SemiconductorsCapacity => 300;

    private MainFrameBuilding(Vector2Int position) : base(position)
    {
    }

    public override void Tick(float deltaSeconds, Context context)
    {
    }

    public static MainFrameBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        return JustCreate(position, context);
    }

    public static MainFrameBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new MainFrameBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return true;
    }
}
