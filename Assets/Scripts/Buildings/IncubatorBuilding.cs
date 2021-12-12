using System;
using UnityEngine;

public class IncubatorBuilding : Building, IPeopleProducer, IEnergyConsumer
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.Incubator,
        size = new Vector2Int(3, 3),
        sprite = Resources.Load<Sprite>("incubator"),
    };

    private const int ConsumedEnergyInternal = 50;
    private const int ConsumedSemiconductorsInternal = 100;

    public override BuildingType Type => BuildingType.Incubator;

    public int ProducedPeople => 10;

    public int ConsumedEnergy => ConsumedEnergyInternal;

    private IncubatorBuilding(Vector2Int position) : base(position)
    {
    }

    public override void Tick(float deltaSeconds, Context context)
    {
    }

    public static IncubatorBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        context.semiconductors.Substract(ConsumedSemiconductorsInternal, context);
        return JustCreate(position, context);
    }

    public static IncubatorBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new IncubatorBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return context.energy.CanUse(ConsumedEnergyInternal, context) && context.semiconductors.CanSubstract(ConsumedSemiconductorsInternal, context);
    }
}
