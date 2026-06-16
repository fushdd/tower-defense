using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    private WaypointPath path;
    public float speed = 3f;

    private int currentWaypointIndex = 0;

    public void SetWaypointPath(WaypointPath path)
    {
        this.path = path;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex >= path.waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        Transform target = path.waypoints[currentWaypointIndex];

        // move towards waypoints (in order)
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, target.position) == 0)
        {
            currentWaypointIndex++;
        }
    }
}
