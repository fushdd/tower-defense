using System.Collections.Generic;
using UnityEngine;

public class FullAOEAttack : MonoBehaviour, ITowerAttack
{
    public void Attack(List<GameObject> enemiesInRadius, GameObject attackUI, float damage)
    {
        // CREATE VISUAL AOE EXPLOSION

        GameObject explosion = Instantiate(attackUI, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        // DEAL DAMAGE

        // create a snapshot in case someone dies mid loop
        GameObject[] snapshot = enemiesInRadius.ToArray();

        for (int i = 0; i < snapshot.Length; i++)
        {
            if (snapshot[i] == null) continue;

            snapshot[i].GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(explosion, 0.2f);
    }
}
