using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float adjTrajFactor = 10f;
    public float mass = 10f;
    public int SFXIdx = 2;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name.Contains("Hips"))
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

            ThrowProjectile(other.gameObject.transform, projectile);
        }
    }


    void ThrowProjectile(Transform target, GameObject projectile)
    {
        if (SFXIdx >= 0)
            AudioManager.Instance.PlayAudio(SFXIdx);
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
    IEnumerator AdjustTrajectory(Rigidbody projectileRb, Transform initialTarget)
    {
        Transform currentTarget = initialTarget;

        Vector3 targetDirection = (currentTarget.position - projectileRb.position).normalized;

        // Calculate the adjusted direction
        Vector3 auxiliarVector = Vector3.Cross(projectileRb.linearVelocity.normalized, -targetDirection);

        Vector3 perpendicularDirection = Vector3.Cross(projectileRb.linearVelocity.normalized, auxiliarVector);

        // Apply a force to the projectile
        projectileRb.AddForce(perpendicularDirection * adjTrajFactor, ForceMode.VelocityChange); // Adjust the force as needed

        yield return null;
    }
}
