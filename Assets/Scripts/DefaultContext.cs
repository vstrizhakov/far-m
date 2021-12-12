using UnityEngine;

public class DefaultContext : Context
{
    public DefaultContext()
    {
        MainFrameBuilding.JustCreate(new Vector2Int(6, 11), this);

        IncubatorBuilding.JustCreate(new Vector2Int(7, 6), this);

        CapsuleBuilding.JustCreate(new Vector2Int(8, 2), this);
        CapsuleBuilding.JustCreate(new Vector2Int(9, 2), this);

        WastelandBuilding.JustCreate(new Vector2Int(0, 0), this);

        semiconductors.Add(150, this);
    }
}
