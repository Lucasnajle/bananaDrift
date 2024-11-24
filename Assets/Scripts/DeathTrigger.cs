using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
    }

    private void OnCollisionEnter(Collision col)
    {
        print($"OnCollisionEnter {col.collider.name}");
    }
}
