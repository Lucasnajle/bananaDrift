using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 50f; // Speed of movement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing! Add one to this GameObject.");
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
    }
}

