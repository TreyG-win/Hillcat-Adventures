using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float speed = 5f;
    public float liftForce = 100f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;
    public bool isActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Initialize with the drone inactive, if desired
        SetDroneActive(isActive);
    }

    void Update()
    {
        // Toggle drone activity when the player presses a specific key, e.g., 'F'
        if (Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive;
            SetDroneActive(isActive);
        }
    }

   void FixedUpdate()
    {
       if (isActive)
       {
           Move();
            Lift();
            Rotate();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void Lift()
    {
        if (Input.GetKey(KeyCode.Space)) // Ascend
        {
            rb.AddForce(Vector3.up * liftForce);
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // Descend
        {
            rb.AddForce(Vector3.down * liftForce);
        }
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.Q)) // Rotate Left
        {
            rb.AddTorque(Vector3.down * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.E)) // Rotate Right
        {
            rb.AddTorque(Vector3.up * rotationSpeed);
        }
    }

    private void SetDroneActive(bool active)
    {
        if (active)
        {
            rb.constraints = RigidbodyConstraints.None;
            // Enable physics interactions fully, if needed.
            rb.useGravity = true;
        }
        else
        {
            // When not active, prevent falling and keep upright.
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            // Optionally, disable gravity to ensure it absolutely doesn't fall.
            rb.useGravity = false;
        }
    }
}
