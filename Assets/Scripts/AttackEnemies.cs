using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttackEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRadius = new();
    public float damage = 2f;
    public float attackInterval = 1f;
    public GameObject attackProjectileSprite;

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

            GameObject enemy = enemiesInRadius[0];

            // CREATE VISUAL PROJECTILE (LASER) FOR EACH ATTACK

            // create the projectile
            GameObject projectile = Instantiate(attackProjectileSprite, transform.position, Quaternion.identity);

            // place it between tower and enemy
            projectile.transform.position = Vector3.Lerp(projectile.transform.position, enemy.transform.position, 0.5f);

            // scale it to right length
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            projectile.transform.localScale = new Vector3(
                distanceToEnemy,
                projectile.transform.localScale.y,
                projectile.transform.localScale.z
                );

            // rotate
            Vector3 vectorToEnemy = enemy.transform.position - transform.position;
            float radiansToRotate = Mathf.Atan(vectorToEnemy.y / vectorToEnemy.x);
            projectile.transform.rotation = Quaternion.Euler(0, 0, radiansToRotate * Mathf.Rad2Deg);

            Destroy(projectile, 0.1f);

            // deal damage every {attackInterval} seconds
            enemiesInRadius[0].GetComponent<EnemyHealth>().TakeDamage(damage);
            
            yield return new WaitForSeconds(attackInterval);
            
        }

        
    }

}
