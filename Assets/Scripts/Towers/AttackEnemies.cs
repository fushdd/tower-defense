using System.Collections.Generic;
using UnityEngine;

public class AttackEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRadius = new();
    public float damage;
    public float attackInterval;
    public float attackRadius;
    public GameObject attackUI;

    private float cooldown = 0;

    private ITowerAttack attackInterface;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // add enemies in the list as they enter tower's radius
        enemiesInRadius.Add(collision.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemiesInRadius.Remove(collision.gameObject);
    }

    private void Awake()
    {
        transform.localScale = new Vector3(attackRadius * 2, attackRadius * 2, attackRadius * 2);
        attackInterface = GetComponent<ITowerAttack>();
    }

    private void Update()
    {
        // track global attack cooldown for this tower
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        // if enemies are in range and attack is not on cooldown -> attack and start the cooldown
        if (enemiesInRadius.Count > 0)
        {
            // attack
            attackInterface.Attack(enemiesInRadius, attackUI, damage);

            // start cooldown
            cooldown += attackInterval;

            // remove all destroyed (killed) objects
            enemiesInRadius.RemoveAll(enemy => enemy == null);
        }

    }

}
