using UnityEngine;

public class AiPatrol : MonoBehaviour
{
    public Transform[] waypoints;   // Array to store the patrol waypoints
    public float speed = 3f;        // Movement speed of the object
    public float rollSpeed = 100f;  // Roll speed of the object
    public float pitchSpeed = 100f; // Pitch speed of the object
    public Transform visuals;
    private int currentWaypointIndex = 0;   // Index of the current waypoint

    void Update()
    {
        // Check if there are waypoints available
        if (waypoints.Length > 0)
        {
            // Move towards the current waypoint
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Calculate direction to the target waypoint
            Vector3 directionToTarget = targetPosition - transform.position;

            // Calculate rotation to look at the target waypoint
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate towards the target rotation (pitch and roll)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rollSpeed);

            // Check if the object has reached the current waypoint
            if (transform.position == targetPosition)
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw lines between waypoints for visualization in the scene view
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(waypoints[i].position, 0.1f);

            if (i > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
            }
        }

        // Draw line between last and first waypoint to create a loop
        if (waypoints.Length > 1)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }
    }
}
