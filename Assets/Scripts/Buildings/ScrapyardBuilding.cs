using System;
using UnityEngine;

public class ScrapyardBuilding : Building, ISemiconductorsStorage, IEnergyConsumer
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.Scrapyard,
        size = new Vector2Int(3, 3),
        sprite = Resources.Load<Sprite>("scrapyard"),
    };

    public override BuildingType Type => BuildingType.Scrapyard;

    private const int ConsumedSemiconductorsInternal = 50;
    private const int ConsumedEnergyInternal = 80;

    public int SemiconductorsCapacity => 500;

    public int ConsumedEnergy => ConsumedEnergyInternal;

    private ScrapyardBuilding(Vector2Int position) : base(position)
    {
    }

    public override void Tick(float deltaSeconds, Context context)
    {

    }

    public static ScrapyardBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        context.semiconductors.Substract(ConsumedSemiconductorsInternal, context);
        return JustCreate(position, context);
    }

    public static ScrapyardBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new ScrapyardBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return context.semiconductors.CanSubstract(ConsumedSemiconductorsInternal, context) && context.energy.CanUse(ConsumedEnergyInternal, context);
    }
}
