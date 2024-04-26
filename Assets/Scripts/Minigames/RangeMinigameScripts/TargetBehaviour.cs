using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Checks if a target is hit by a ray, then creates the destroyed target particles
/// before increasing the amount of targets hit in game behaviour
/// </summary>
public class TargetBehaviour : MonoBehaviour
{
    public GameBehaviour gameManager;
    public ParticleSystem targetParticles;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    // Method to check if the target is hit by a ray
    public void CheckRayHit(Ray ray)
    {
        RaycastHit hitInfo;

        // Check if the ray intersects with this target
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            // Instantiate particles, destroy the target, and update game manager
            Instantiate(targetParticles.gameObject, transform.position, transform.rotation);
            Destroy(gameObject);
            gameManager.Targets += 1;
        }
    }
}
