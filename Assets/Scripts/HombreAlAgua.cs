using UnityEngine;

public class HombreAlAgua : MonoBehaviour
{
    public Collider planeCollider;       // Assign the Plane's Collider in the Inspector
    public GameObject particlePrefab;    // Assign the Particle System prefab in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other == planeCollider)
        {
            Debug.Log("Entered the trigger zone of the plane!");
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
    }
}
