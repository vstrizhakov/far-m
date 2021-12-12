using UnityEngine;
using UnityEngine.UI;

public class WastelandUIHandler : MonoBehaviour
{
    public WastelandBuilding building;

    public GameObject addWorkerButton;

    private ContextManager contextManager;
    private Canvas canvas;

    private void Awake()
    {
        contextManager = GetComponent<ContextManager>();
        canvas = addWorkerButton.GetComponentInParent<Canvas>();
    }

    public void OnAddWorkerClicked()
    {
        if (building != null)
        {
            if (building.CanCreateWorker(contextManager.context))
            {
                building.AddWorker(contextManager.context);
            }
        }
        canvas.enabled = false;
    }

    private void Update()
    {
        if (building != null)
        {
            addWorkerButton.GetComponent<Button>().interactable = building.CanCreateWorker(contextManager.context);
        }
    }
}
