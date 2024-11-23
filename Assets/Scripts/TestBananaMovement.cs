using UnityEngine;

public class TestBananaMovement : MonoBehaviour
{
    public float amplitude = 0.1f; // How far it moves
    public float frequency = 10f; // Speed of vibration
    public float springStrength = 10f; // Strength of the spring force
    public float damping = 0.5f; // Damping to prevent oscillation over time

    private Rigidbody rb;
    private Vector3 originalPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Calculate the vibration offset for X and Z
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetZ = Mathf.Cos(Time.time * frequency) * amplitude;

        // Calculate the spring force to return the object to its original Y position
        float springForce = (originalPosition.y - rb.position.y) * springStrength;

        // Apply the vibration forces to the Rigidbody
        Vector3 targetPosition = new Vector3(offsetX, rb.position.y, offsetZ);
        Vector3 forceDirection = targetPosition - rb.position;

        // Apply the spring force to the Y-axis
        Vector3 springForceVector = new Vector3(0f, springForce, 0f);

        // Apply both the spring force and the vibration forces using velocity change
        rb.AddForce(forceDirection + springForceVector - velocity * damping, ForceMode.VelocityChange);

        // Update velocity for damping calculation
        velocity = rb.linearVelocity;
    }
}
