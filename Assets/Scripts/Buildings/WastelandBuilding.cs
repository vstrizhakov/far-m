using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WastelandBuilding : Building, ISemiconductorsStorage, IEnergyConsumer, IWorkplace
{
    public static readonly BuildingSpecifications Specifications = new BuildingSpecifications
    {
        buildingType = BuildingType.Wasteland,
        size = new Vector2Int(6, 6),
        sprite = Resources.Load<Sprite>("wasteland"),
    };

    private const int ConsumedEnergyInternal = 10;
    private const int ConsumedSemiconductorsInternal = 20;

    private float _elapsedTime;
    private List<Worker> _workers;

    public float Period => 30;
    public int SemiconductorsPerPeriod => 15;
    public int SemiconductorsCapacity => 200;

    public int WorkersCapacity => 2;

    public override BuildingType Type => BuildingType.Wasteland;

    public int ConsumedEnergy => _workers.Sum(x => x.ConsumedEnergy);

    private WastelandBuilding(Vector2Int position) : base(position)
    {
        _workers = new List<Worker>();
    }

    public override void Tick(float deltaTime, Context context)
    {
        _elapsedTime += deltaTime;
        if (_elapsedTime > Period)
        {
            _elapsedTime -= Period;

            try
            {
                for (int i = 0; i < _workers.Count; i++)
                {
                    context.semiconductors.Add(SemiconductorsPerPeriod, context);
                }
            }
            catch
            {
            }
        }
    }

    public bool CanCreateWorker(Context context)
    {
        return _workers.Count + 1 <= WorkersCapacity && Worker.CanCreate(ConsumedEnergyInternal, ConsumedSemiconductorsInternal, context);
    }

    public void AddWorker(Context context)
    {
        if (!CanCreateWorker(context))
        {
            throw new InvalidOperationException();
        }
        var worker = Worker.Create(ConsumedEnergyInternal, ConsumedSemiconductorsInternal, context);
        _workers.Add(worker);
    }

    public static WastelandBuilding Create(Vector2Int position, Context context)
    {
        if (!CanCreate(context))
        {
            throw new InvalidOperationException();
        }
        return JustCreate(position, context);
    }

    public static WastelandBuilding JustCreate(Vector2Int position, Context context)
    {
        var building = new WastelandBuilding(position);
        context.buildings.Add(building);
        return building;
    }

    public static bool CanCreate(Context context)
    {
        return true;
    }
}
