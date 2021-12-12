using UnityEngine;

public class BuildingsFactory : MonoBehaviour
{
    public ContextManager contextManager;

    public bool CanCreate(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.MainFrame:
                return MainFrameBuilding.CanCreate(contextManager.context);
            case BuildingType.Incubator:
                return IncubatorBuilding.CanCreate(contextManager.context);
            case BuildingType.Capsule:
                return CapsuleBuilding.CanCreate(contextManager.context);
            case BuildingType.Wasteland:
                return WastelandBuilding.CanCreate(contextManager.context);
            case BuildingType.Scrapyard:
                return ScrapyardBuilding.CanCreate(contextManager.context);
            case BuildingType.MainNetNode:
                return MainNetNodeBuilding.CanCreate(contextManager.context);
            default:
                return false;
        }
    }

    public BuildingSpecifications? GetSpecifications(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.MainFrame:
                return MainFrameBuilding.Specifications;
            case BuildingType.Incubator:
                return IncubatorBuilding.Specifications;
            case BuildingType.Capsule:
                return CapsuleBuilding.Specifications;
            case BuildingType.Wasteland:
                return WastelandBuilding.Specifications;
            case BuildingType.Scrapyard:
                return ScrapyardBuilding.Specifications;
            case BuildingType.MainNetNode:
                return MainNetNodeBuilding.Specifications;
            default:
                return null;
        }
    }

    public Building Create(Vector2Int position, BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.MainFrame:
                return MainFrameBuilding.Create(position, contextManager.context);
            case BuildingType.Incubator:
                return IncubatorBuilding.Create(position, contextManager.context);
            case BuildingType.Capsule:
                return CapsuleBuilding.Create(position, contextManager.context);
            case BuildingType.Wasteland:
                return WastelandBuilding.Create(position, contextManager.context);
            case BuildingType.Scrapyard:
                return ScrapyardBuilding.Create(position, contextManager.context);
            case BuildingType.MainNetNode:
                return MainNetNodeBuilding.Create(position, contextManager.context);
            default:
                return null;
        }
    }
}
