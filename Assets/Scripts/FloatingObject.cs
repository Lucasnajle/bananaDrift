using UnityEngine;
using System.Collections;

public class FloatingObject : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isReturning = false;
    public float returnDuration = 2f;  // Time it takes to return to the original position
    public float returnDelay = 1f;     // Time to wait before returning

    void Start()
    {
        // Store the initial position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the object collides with something and is not already returning
        if (!isReturning)
        {
            // Start the return process after a delay
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        isReturning = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(returnDelay);

        Rigidbody rb = GetComponent<Rigidbody>();

        // Disable physics interactions temporarily
        rb.detectCollisions = false;  // Disable collisions
        rb.isKinematic = true;

        // Smoothly interpolate back to the original position and rotation
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, originalPosition, elapsedTime / returnDuration);
            transform.rotation = Quaternion.Lerp(initialRotation, originalRotation, elapsedTime / returnDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and rotation is exactly the original
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Re-enable physics interactions after returning
        rb.isKinematic = false;
        rb.detectCollisions = true;  // Re-enable collisions

        isReturning = false;  // Allow the object to return again if it collides later
    }
}

