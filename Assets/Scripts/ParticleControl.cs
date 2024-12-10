using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public float movementThreshold = 1f; // Threshold for movement, below this value, particles will not be emitted
    private Rigidbody rb;
    public ParticleSystem particleSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing! Please add one.");
        }

        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem is missing! Please add one.");
        }
    }

    void Update()
    {
        if (rb != null && particleSystem != null)
        {
            // Check if the object's velocity magnitude is greater than the threshold
            if (rb.linearVelocity.z > movementThreshold)
            {
                if (!particleSystem.isPlaying)
                {
                    Debug.Log("Play");
                    particleSystem.Play(); // Start emitting particles if moving
                }
            }
            else
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop(); // Stop emitting particles if not moving
                }
            }
        }
    }
}
