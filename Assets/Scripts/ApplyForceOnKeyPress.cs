using UnityEngine;

public class ApplyForceOnKeyPress : MonoBehaviour
{
    public Rigidbody targetRigidbody;  // Assign the Rigidbody in the Inspector
    public float forceAmount = 10f;    // Amount of force to apply

    void Update()
    {
        // Check for Left Arrow key press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ApplyForce(Vector3.left);  // Apply force to the left (local space)
        }

        // Check for Right Arrow key press
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ApplyForce(Vector3.right); // Apply force to the right (local space)
        }
    }

    void ApplyForce(Vector3 direction)
    {
        if (targetRigidbody != null)
        {
            // Apply the force in the local space of the Rigidbody
            targetRigidbody.AddForce(targetRigidbody.transform.TransformDirection(direction) * forceAmount, ForceMode.Impulse);
        }
    }
}
