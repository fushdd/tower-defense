using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttackEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRadius = new();
    public float damage = 2f;
    public float attackInterval = 1f;

    private Coroutine attackCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // add enemies in the list as they enter tower's radius
        enemiesInRadius.Add(collision.gameObject);

        // start attacking enemies in radius
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackLoop());
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemiesInRadius.Remove(collision.gameObject);
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            // remove all destroyed (killed) objects
            enemiesInRadius.RemoveAll(enemy => enemy == null);

            // stop attacking coroutine when no enemies in radius
            if (enemiesInRadius.Count == 0)
            {
                attackCoroutine = null;
                yield break;
            }
            
            // deal damage every {attackInterval} seconds
            enemiesInRadius[0].GetComponent<EnemyHealth>().TakeDamage(damage);
            yield return new WaitForSeconds(attackInterval);
            
        }

        
    }

}
