using UnityEngine;

public class FollowDrone : MonoBehaviour
{
    public Transform droneTransform;
    public Vector3 offsetPosition;

    void LateUpdate()
    {
        // Calculate the desired position based on drone's position and the offset
        Vector3 desiredPosition = droneTransform.position + offsetPosition;

        // Set the camera's position to this desired position
        transform.position = desiredPosition;

        // Make the camera look at the drone
        transform.LookAt(droneTransform.position);
    }
}
