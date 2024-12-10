using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BirdLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float adjTrajFactor = 10f;
    public float mass = 10f;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.name.Contains("Head"))
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

            ThrowProjectile(other.gameObject, projectile);
        }
    }

    void ThrowProjectile(GameObject target, GameObject projectile)
    {
        // Add a Rigidbody component to the projectile
        Rigidbody projectileRb = projectile.AddComponent<Rigidbody>();
        projectileRb.mass = mass;
        projectileRb.useGravity = false;

        // Set the initial velocity of the projectile
        projectileRb.linearVelocity = launchPoint.forward * launchForce;

        StartCoroutine(AdjustTrajectory(projectileRb, target));

        Destroy(projectile, 10f); // Destroy the projectile after the specified lifespan

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
            projectileRb.rotation = Quaternion.LookRotation(targetDirection);

            // Calculate correction force perpendicular to current velocity
            Vector3 auxiliarVector = Vector3.Cross(projectileRb.linearVelocity.normalized, -targetDirection);
            Vector3 perpendicularDirection = Vector3.Cross(projectileRb.linearVelocity.normalized, auxiliarVector);

            // Apply correction force
            projectileRb.AddForce(perpendicularDirection * adjTrajFactor, ForceMode.VelocityChange);

            yield return new WaitForFixedUpdate(); // Wait for the next physics update
        }
    }
}
