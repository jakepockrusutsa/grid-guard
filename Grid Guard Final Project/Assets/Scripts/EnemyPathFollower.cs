using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float reachThreshold = 0.1f;
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Automatically assign waypoints from the manager
        if (waypoints == null || waypoints.Length == 0)
        {
            if (WaypointManager.Instance != null)
                waypoints = WaypointManager.Instance.waypoints;
            else
                Debug.LogError("WaypointManager not found in scene!");
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= reachThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Destroy enemy here if thats what i want
                return;
            }
            targetWaypoint = waypoints[currentWaypointIndex];
            direction = targetWaypoint.position - transform.position;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
