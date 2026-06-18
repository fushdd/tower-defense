using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float curHealth = 5f;
    public float MaxHealth = 5f;

    private Transform healthFillVisual;
    public TMP_Text healthTextVisual;

    public void TakeDamage(float damage)
    {
        curHealth -= damage;

        // scale and reposition health fill ui
        healthFillVisual.localScale = new Vector3(curHealth / MaxHealth, 1, 1);
        healthFillVisual.localPosition = new Vector3((curHealth / MaxHealth - 1) / 2, 0, 0);

        // change text values
        healthTextVisual.text = $"{curHealth}/{MaxHealth}";

        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        //healthFillVisual = transform.Find("HealthFill");
        healthFillVisual = transform.GetChild(0).Find("HealthFill");
        healthTextVisual.text = $"{curHealth}/{MaxHealth}";
    }
}
