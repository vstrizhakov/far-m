using UnityEngine;

public abstract class Building
{
    public Vector2Int Position { get; }
    public abstract BuildingType Type { get; }
    public abstract void Tick(float deltaSeconds, Context context);

    protected Building(Vector2Int position)
    {
        Position = position;
    }
}
