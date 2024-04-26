using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;
    private bool isPaused = false;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length && !isPaused)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                // Stop NPC movement or perform any desired action
                return;
            }
        }
    }

    public void ResumeMovement()
    {
        isPaused = false;
    }
}
