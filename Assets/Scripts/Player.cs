using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform hip;
    void Awake()
    {
        SetKinematic(true);
    }

    public void SetKinematic(bool active)
    {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = active;
            if (rb.GetComponent<Collider>() != null)
                rb.GetComponent<Collider>().enabled = !active;
        }

    }
    private void OnCollisionEnter(Collision col)
    {
        print(col.transform.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
    }
}
