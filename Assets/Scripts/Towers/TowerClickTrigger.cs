using UnityEngine;
using UnityEngine.InputSystem;

public class TowerClickTrigger : MonoBehaviour
{
    private int towerClickLayer;
    private Transform attackRangeUI;

    private void Start()
    {
        attackRangeUI = transform.parent.Find("AttackRangeUI");
        towerClickLayer = LayerMask.GetMask("Tower Click Trigger");
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D objectUnderCursor = Physics2D.OverlapPoint(mousePos, towerClickLayer); // check only on specific layer
            if (objectUnderCursor != null && objectUnderCursor.GetComponent<TowerClickTrigger>() == this)
            {
                attackRangeUI.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                attackRangeUI.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
