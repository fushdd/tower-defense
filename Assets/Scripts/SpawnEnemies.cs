using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public WaypointPath path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // spawn enemy at spawner's position every 5 seconds (or so)
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // TEMPORARY FOR TESTING ENEMIES AND SO
            GameObject enemy = Instantiate(randomEnemy, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().SetWaypointPath(path);

            yield return new WaitForSeconds(4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
