using UnityEngine;

public class BuildingBehaviour : MonoBehaviour
{
    public Building building;
    public ContextManager contextManager;

    private void FixedUpdate()
    {
        building.Tick(Time.fixedDeltaTime, contextManager.context);
    }
}
