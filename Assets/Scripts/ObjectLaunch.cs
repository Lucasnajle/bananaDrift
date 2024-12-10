using UnityEngine;

public class ObjectLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    GameObject playerBody;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float mass = 10f;
    public bool invertedModel = false;
    public bool aimHead = true;

    private void OnTriggerEnter(Collider other)
    {
        GameObject projectile;        
        bool shoot = false;

        if (aimHead)
        {
            if (other.gameObject.name.Contains("Head"))
            {
                playerBody = other.gameObject;
                shoot = true;
            }
        }
        else
        {
            if (other.gameObject.name.Contains("Root"))
            {
                playerBody = other.gameObject;
                shoot = true;
            }
        }

        if (shoot)
        {
            if (invertedModel)
            {
                projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.Euler(0, -90, 0));
            }
            else
            {
                projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            }

            ThrowProjectile(playerBody, projectile);
        }
    }

    void ThrowProjectile(GameObject target, GameObject projectile)
    {
        // Add a Rigidbody component to the projectile
        Rigidbody projectileRb = projectile.AddComponent<Rigidbody>();
        projectileRb.mass = mass;
        projectileRb.useGravity = false;

        // Calculate the direction to hit the target based on its velocity
        Vector3 aimDirection = CalculateInterceptDirection(target, projectile.transform.position, launchForce);     

        // Set the initial velocity of the projectile
        projectileRb.linearVelocity = aimDirection * launchForce;

        // Destroy the projectile after a specified lifespan
        Destroy(projectile, 3f);
    }

    Vector3 CalculateInterceptDirection(GameObject target, Vector3 projectilePosition, float projectileSpeed)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        if (targetRb == null)
        {
            Debug.LogError("Target does not have a Rigidbody!");
            return (target.transform.position - projectilePosition).normalized;
        }

        // Get target's velocity and position
        Vector3 targetVelocity = targetRb.linearVelocity;
        Vector3 targetPosition = target.transform.position;

        // Relative position and velocity
        Vector3 relativePosition = targetPosition - projectilePosition;
        Vector3 relativeVelocity = targetVelocity;

        // Solve the quadratic equation for time of impact
        float a = relativeVelocity.sqrMagnitude - projectileSpeed * projectileSpeed;
        float b = 2 * Vector3.Dot(relativeVelocity, relativePosition);
        float c = relativePosition.sqrMagnitude;

        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            // No real solution, aim directly at the target's current position
            Debug.LogWarning("No viable intercept course; aiming directly at target.");
            return relativePosition.normalized;
        }

        // Calculate the time of impact
        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        float timeToImpact = Mathf.Max(t1, t2);
        if (timeToImpact < 0)
        {
            // Negative time, no valid interception
            Debug.LogWarning("Negative intercept time; aiming directly at target.");
            return relativePosition.normalized;
        }

        // Calculate the predicted position of the target
        Vector3 futurePosition = targetPosition + targetVelocity * timeToImpact;

        // Return the direction to the future position
        return (futurePosition - projectilePosition).normalized;
    }
}
