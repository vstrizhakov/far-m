using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject buildingsCanvas;
    public Text energyText;
    public Text semiconductorsText;

    private ContextManager contextManager;

    private void Awake()
    {
        contextManager = GetComponent<ContextManager>();
    }

    public void OnBuildingsClick()
    {
        var canvas = buildingsCanvas.GetComponent<Canvas>();
        canvas.enabled = !canvas.enabled;
    }

    public void Update()
    {
        var context = contextManager.context;
        energyText.text = $"{context.energy.GetAmount(context)}/{context.energy.GetCapacity(context)}";
        semiconductorsText.text = $"{context.semiconductors.GetAmount(context)}/{context.semiconductors.GetCapacity(context)}";
    }
}
