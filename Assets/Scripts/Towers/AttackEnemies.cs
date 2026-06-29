using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRadius = new();
    public float damage = 2f;
    public float attackInterval = 1f;
    public GameObject attackProjectileSprite;

    private GameObject curEnemy = null;
    private float cooldown = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // add enemies in the list as they enter tower's radius
        enemiesInRadius.Add(collision.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemiesInRadius.Remove(collision.gameObject);
    }


    private void Update()
    {
        // determine the enemy closest to finish
        float mostProgress = float.MinValue;
        curEnemy = null;
        foreach (GameObject e in enemiesInRadius)
        {
            if (e.GetComponent<EnemyMovement>().GetProgress() > mostProgress)
            {
                mostProgress = e.GetComponent<EnemyMovement>().GetProgress(); // ew
                curEnemy = e;
            }
        }

        // track global attack cooldown for this tower
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        // if enemies are in range and attack is not on cooldown -> attack and start the cooldown
        if (enemiesInRadius.Count > 0)
        {
            // CREATE VISUAL PROJECTILE (LASER) FOR EACH ATTACK

            // create the projectile
            GameObject projectile = Instantiate(attackProjectileSprite, transform.position, Quaternion.identity);

            // place it between tower and enemy
            projectile.transform.position = Vector3.Lerp(projectile.transform.position, curEnemy.transform.position, 0.5f);

            // scale it to right length
            float distanceToEnemy = Vector3.Distance(transform.position, curEnemy.transform.position);
            projectile.transform.localScale = new Vector3(
                distanceToEnemy,
                projectile.transform.localScale.y,
                projectile.transform.localScale.z
                );

            // rotate
            Vector3 vectorToEnemy = curEnemy.transform.position - transform.position;
            float radiansToRotate = Mathf.Atan(vectorToEnemy.y / vectorToEnemy.x);
            projectile.transform.rotation = Quaternion.Euler(0, 0, radiansToRotate * Mathf.Rad2Deg);

            Destroy(projectile, 0.1f);

            // deal damage every {attackInterval} seconds
            curEnemy.GetComponent<EnemyHealth>().TakeDamage(damage);

            // start cooldown
            cooldown += attackInterval;

            // remove all destroyed (killed) objects
            enemiesInRadius.RemoveAll(enemy => enemy == null);
        }

    }

}
