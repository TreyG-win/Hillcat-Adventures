using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the raycasting / shooting.
/// Essentially when the left mouse button is clicked, it will send out a ray
/// to the center of the screen and then check if it hit a target.
/// </summary>
public class RayCastShooter : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Transform FirePoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        // Get the center of the screen in viewport coordinates
        Vector3 centerOfScreen = new Vector3(0.5f, 0.5f, 0f);

        // Cast a ray from the camera through the center of the screen
        Ray ray = mainCamera.ViewportPointToRay(centerOfScreen);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);

            // Check if the object hit has a TargetBehaviour component
            TargetBehaviour target = hit.collider.GetComponent<TargetBehaviour>();
            if (target != null)
            {
                // Call the CheckRayHit method to handle the hit
                target.CheckRayHit(ray);
            }
        }
    }
}
