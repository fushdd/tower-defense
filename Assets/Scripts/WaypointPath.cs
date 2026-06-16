using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // collect path waypoints in an array
    public Transform[] waypoints;
    void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
