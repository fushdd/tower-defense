using UnityEngine;

public class TowerSelectButton : MonoBehaviour
{
    public TowerPlacementManager placementManager;
    public GameObject tower;

    public void OnButtonClicked()
    {
        placementManager.InitiateTowerPlacement(tower);
    }
}
