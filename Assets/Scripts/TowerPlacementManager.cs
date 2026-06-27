using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    private GameObject curTower = null;
    private bool isPlacing = false;
    private bool atValidPlacementSpot = false;
    private PlacementSpotTrigger curPlacementSpot = null;

    public void InitiateTowerPlacement(GameObject tower)
    {
        if (curTower != null) return;

        // set only tower's sprite (visuals)
        curTower = Instantiate(tower);
        curTower.GetComponent<CircleCollider2D>().enabled = false;
        curTower.GetComponent<AttackEnemies>().enabled = false;

        isPlacing = true;
    }
    public void EnterPlacementSpot(PlacementSpotTrigger spot)
    {
        curPlacementSpot = spot;
        atValidPlacementSpot = curPlacementSpot.isValid;
                
    }

    public void LeavePlacementSpot()
    {
        curPlacementSpot = null;
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
        int placemenSpotLayer = LayerMask.GetMask("Tower Placement Spot");
        Collider2D objectUnderCursor = Physics2D.OverlapPoint(worldPos, placemenSpotLayer); // check only on specific layer
        if (objectUnderCursor != null && objectUnderCursor.TryGetComponent(out PlacementSpotTrigger spot) && isPlacing)
        {
            EnterPlacementSpot(spot);
        }
        else
        {
            LeavePlacementSpot();
        }

        // move tower sprite along with the cursor (if not on valid placement spot)
        if (!atValidPlacementSpot)
        {
            curTower.transform.position = worldPos;
        }
        else
        {
            curTower.transform.position = curPlacementSpot.transform.position;
        }

        // place the tower on a placement spot
        if (Mouse.current.leftButton.wasPressedThisFrame && atValidPlacementSpot)
        {
            curTower.GetComponent<CircleCollider2D>().enabled = true;
            curTower.GetComponent<AttackEnemies>().enabled = true;
            isPlacing = false;
            curTower = null;
            curPlacementSpot.isValid = false;
        }

        // stop placing with right click
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Destroy(curTower);
            //curTower = null;
            isPlacing = false;
        }
        
    }
}
