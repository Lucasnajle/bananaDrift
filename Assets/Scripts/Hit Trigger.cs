using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public GameObject projectile;
    public float launchSpeed = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Hips"))
        {
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (other.transform.position).normalized;

                Vector3 velocity = direction * launchSpeed;

                rb.linearVelocity = velocity;
            }
        }
    }
}
