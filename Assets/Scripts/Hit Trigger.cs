using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HitTrigger : MonoBehaviour
{
    public GameObject projectile;
    public int SFXIdx = 3;
    public float launchSpeed = 10f;
    public float adjTrajFactor = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Root"))
        {
            if (SFXIdx >= 0)
                AudioManager.Instance.PlayAudio(SFXIdx);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (other.transform.position - rb.transform.position).normalized;

                Vector3 velocity = -direction * launchSpeed;

                rb.linearVelocity = velocity;

                StartCoroutine(AdjustTrajectory(rb, other.gameObject));
            }
        }
    }

    // Coroutine to continuously adjust the trajectory of the projectile
    IEnumerator AdjustTrajectory(Rigidbody projectileRb, GameObject target)
    {
        while (target != null && projectileRb != null)
        {
            // Calculate direction to target
            Vector3 sub = target.transform.position - projectileRb.position;
            Vector3 targetDirection = sub.normalized;

            // Rotate the projectile to face the target
            //projectileRb.rotation = Quaternion.LookRotation(targetDirection);

            // Calculate correction force perpendicular to current velocity
            Vector3 auxiliarVector = Vector3.Cross(projectileRb.linearVelocity.normalized, -targetDirection);
            Vector3 perpendicularDirection = Vector3.Cross(projectileRb.linearVelocity.normalized, auxiliarVector);

            // Apply correction force
            projectileRb.AddForce(perpendicularDirection * adjTrajFactor, ForceMode.VelocityChange);

            if (sub.magnitude > 0.1f)
            {
                yield return new WaitForFixedUpdate(); // Wait for the next physics update
            }
            else
            {
                // Fly away
                projectileRb.linearVelocity = Vector3.zero;
                projectileRb.AddForce(projectileRb.transform.up, ForceMode.VelocityChange);
                if (SFXIdx >= 0)
                    AudioManager.Instance.PlayAudio(SFXIdx);
                break;
            }

        }
    }
}
