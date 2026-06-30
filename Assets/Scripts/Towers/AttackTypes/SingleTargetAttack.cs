using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : MonoBehaviour, ITowerAttack
{
    public void Attack(List<GameObject> enemiesInRadius, GameObject attackUI, float damage)
    {
        // create a snapshot in case someone dies mid loop
        GameObject[] snapshot = enemiesInRadius.ToArray();

        float mostProgress = float.MinValue;
        GameObject curEnemy = null;

        foreach (GameObject e in snapshot)
        {

            float progress = e.GetComponent<EnemyMovement>().GetProgress();
            if (progress > mostProgress)
            {
                mostProgress = progress;
                curEnemy = e;
            }
        }

        // CREATE VISUAL PROJECTILE (LASER) FOR EACH ATTACK

        // create the projectile
        GameObject projectile = Instantiate(attackUI, transform.position, Quaternion.identity);

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
    }
}
