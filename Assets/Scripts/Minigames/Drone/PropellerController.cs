using UnityEngine;

public class PropellerController : MonoBehaviour
{
    public GameObject[] propellers; // Array to hold the propeller GameObjects
    public float rotationSpeed = 1000f; // Rotation speed of the propellers

    void Update()
    {
        // Check if the Space key is being pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Rotate each propeller around its local Z-axis at the given rotation speed
            foreach (GameObject propeller in propellers)
            {
                propeller.transform.Rotate(0,  rotationSpeed * Time.deltaTime,0, Space.Self);
            }
        }
    }
}
