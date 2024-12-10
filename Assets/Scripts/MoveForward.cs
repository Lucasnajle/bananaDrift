using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 50f; // Speed of movement
    public float rotationSpeed = 50f; // Speed of rotation around the X-axis
    public bool elBoolMasLadri = false;

    private Rigidbody rb;
    private Vector3 worldForwardDirection; // To store world-space forward direction

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing! Add one to this GameObject.");
        }

        // Get the object's forward direction in world space
        worldForwardDirection = transform.forward; // world-space direction
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the object forward along its original world-space forward direction
            Vector3 movement;

            if (elBoolMasLadri)
            {
                // Move the object forward along its original world-space forward direction
                movement = worldForwardDirection * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);

                // Rotate the object around its local X-axis
                Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime, 0, 0);
                rb.MoveRotation(rb.rotation * deltaRotation);  // Apply local rotation
            }
            else
            {
                // Move the object forward along its local forward direction
                movement = transform.forward * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);

                // Rotate the object around world Y-axis
                Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime, 0);
                rb.MoveRotation(rb.rotation * deltaRotation);  // Apply local rotation
            }
            
        }
    }
}

