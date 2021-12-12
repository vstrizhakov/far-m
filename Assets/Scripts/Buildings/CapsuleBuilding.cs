using System;
using UnityEngine;

public class CapsuleBuilding : Building, IEnergyProducer, IPeopleConsumer, IEnergyConsumer
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.Capsule,
        size = new Vector2Int(1, 2),
        sprite = Resources.Load<Sprite>("capsule"),
    };

    private const int ConsumedPeopleInternal = 1;
    private const int ConsumedEnergyInternal = 10;
    private const int ConsumedSemiconductorsInternal = 30;

    public override BuildingType Type => BuildingType.Capsule;

    public int ProducedEnergy => 50;

    public int ConsumedPeople => ConsumedPeopleInternal;

    public int ConsumedEnergy => ConsumedEnergyInternal;

    private CapsuleBuilding(Vector2Int position) : base(position)
    {
    }

    public override void Tick(float deltaSeconds, Context context)
    {
    }

    public static CapsuleBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        context.semiconductors.Substract(ConsumedSemiconductorsInternal, context);
        return JustCreate(position, context);
    }

    public static CapsuleBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new CapsuleBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return context.energy.CanUse(ConsumedEnergyInternal, context)
            && context.people.CanUse(ConsumedPeopleInternal, context)
            && context.semiconductors.CanSubstract(ConsumedSemiconductorsInternal, context);
    }
}
