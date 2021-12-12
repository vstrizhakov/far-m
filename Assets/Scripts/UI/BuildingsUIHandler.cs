using UnityEngine;
using UnityEngine.UI;

public class BuildingsUIHandler : MonoBehaviour
{
    public GameObject incubatorButton;
    public GameObject capsuleButton;
    public GameObject scrapyardButton;
    public GameObject mainnetnodeButton;

    private BuildingsFactory buildingsFactory;
    private BlockPlacementManager blockPlacementManager;

    private void Awake()
    {
        buildingsFactory = GetComponent<BuildingsFactory>();
        blockPlacementManager = GetComponent<BlockPlacementManager>();
    }

    private void Update()
    {
        incubatorButton.GetComponent<Button>().interactable = buildingsFactory.CanCreate(BuildingType.Incubator);
        capsuleButton.GetComponent<Button>().interactable = buildingsFactory.CanCreate(BuildingType.Capsule);
        scrapyardButton.GetComponent<Button>().interactable = buildingsFactory.CanCreate(BuildingType.Scrapyard);
        mainnetnodeButton.GetComponent<Button>().interactable = buildingsFactory.CanCreate(BuildingType.MainNetNode);
    }

    public void OnIncubatorClick()
    {
        Create(BuildingType.Incubator);
    }

    public void OnCapsuleClick()
    {
        Create(BuildingType.Capsule);
    }

    public void OnScrapyardClick()
    {
        Create(BuildingType.Scrapyard);
    }

    public void OnMainNetNodeClick()
    {
        Create(BuildingType.MainNetNode);
    }

    private void Create(BuildingType buildingType)
    {
        if (buildingsFactory.CanCreate(buildingType))
        {
            blockPlacementManager.buildingSpecifications = buildingsFactory.GetSpecifications(buildingType);
            blockPlacementManager.isEnabled = !blockPlacementManager.isEnabled;
        }
    }
}
