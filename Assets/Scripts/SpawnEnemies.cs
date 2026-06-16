using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public GameObject enemyPrefab;
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
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyFollowPath>().SetWaypointPath(path);

            yield return new WaitForSeconds(5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
