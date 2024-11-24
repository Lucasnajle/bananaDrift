using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public GameObject projectile;
    public int SFXIdx = 3;
    public float launchSpeed = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Hips"))
        {
            if (SFXIdx >= 0)
                AudioManager.Instance.PlayAudio(SFXIdx);
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
