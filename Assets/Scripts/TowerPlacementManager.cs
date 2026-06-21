using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    private GameObject curTower = null;
    private bool isPlacing = false;
    private bool atValidPlacementSpot = false;
    private bool isDraggingTower = false;
    private PlacementSpotTrigger curPlacementSpot = null;

    public void InitiateTowerPlacement(GameObject tower)
    {
        // set only tower's sprite (visuals)
        if (curTower != null) return;

        curTower = Instantiate(tower);
        curTower.GetComponent<CircleCollider2D>().enabled = false;
        curTower.GetComponent<TowerAttackEnemies>().enabled = false;

        isPlacing = true;
    }
    public void EnterValidPlacementSpot(PlacementSpotTrigger spot)
    {
        curPlacementSpot = spot;
        atValidPlacementSpot = curPlacementSpot.isValid;
        isDraggingTower = false;
        curTower.transform.position = curPlacementSpot.transform.position;
    }

    public void LeaveValidPlacementSpot()
    {
        curPlacementSpot = null;
        isDraggingTower = true;
        atValidPlacementSpot = false;
    }

    public bool GetIsPlacing()
    {
        return isPlacing;
    }

    private void Update()
    {
        if (!isPlacing) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;

        // check if cursor hovers over a placement spot
        Collider2D objectUnderCursor = Physics2D.OverlapPoint(worldPos);
        if (objectUnderCursor != null && objectUnderCursor.TryGetComponent(out PlacementSpotTrigger spot) && isPlacing)
        {
            EnterValidPlacementSpot(spot);
        }
        else
        {
            LeaveValidPlacementSpot();
        }

        // move tower sprite along with the cursor
        if (isDraggingTower)
        {
            curTower.transform.position = worldPos;
        }

        // place the tower on a placement spot
        if (Mouse.current.leftButton.wasPressedThisFrame && atValidPlacementSpot)
        {
            curTower.GetComponent<CircleCollider2D>().enabled = true;
            curTower.GetComponent<TowerAttackEnemies>().enabled = true;
            isPlacing = false;
            curTower = null;
            curPlacementSpot.isValid = false;
        }
        
    }
}
