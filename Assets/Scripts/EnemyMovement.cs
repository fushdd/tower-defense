using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private WaypointPath path;
    public float speed = 3f;

    private int currentWaypointIndex = 0;

    // track progress to define the enemy closest to finish (to attack them)
    private Vector3 previousPosition;
    private float progress;
    public void SetWaypointPath(WaypointPath path)
    {
        this.path = path;
    }

    public float GetProgress()
    {
        return progress;
    }

    private void Start()
    {
        previousPosition = transform.position;
    }

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

        // add distance to progress
        progress += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;

        if (Vector3.Distance(transform.position, target.position) == 0)
        {
            currentWaypointIndex++;
        }
    }
}
