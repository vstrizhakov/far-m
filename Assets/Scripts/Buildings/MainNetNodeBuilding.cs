using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainNetNodeBuilding : Building, IEnergyConsumer
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.MainNetNode,
        size = new Vector2Int(3, 4),
        sprite = Resources.Load<Sprite>("mainnetnode"),
    };

    public override BuildingType Type => BuildingType.MainNetNode;

    private const int ConsumedEnergyInternal = 100;
    private const int ConsumedSemiconductorsInternal = 150;

    public int ConsumedEnergy => ConsumedEnergyInternal;

    private MainNetNodeBuilding(Vector2Int position) : base(position)
    {

    }

    public override void Tick(float deltaSeconds, Context context)
    {

    }

    public static MainNetNodeBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        context.semiconductors.Substract(ConsumedSemiconductorsInternal, context);
        return JustCreate(position, context);
    }

    public static MainNetNodeBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new MainNetNodeBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return context.energy.CanUse(ConsumedEnergyInternal, context) && context.semiconductors.CanSubstract(ConsumedSemiconductorsInternal, context);
    }
}
