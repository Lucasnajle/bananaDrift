using System.Collections.Generic;
using UnityEngine;

public class BananaMovementController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float bananaForce = 10f;
    public float speedRotation = 2f;
    public float waypointDistance = 1f;
    private int currentIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Count == 0) return;

        Transform waypointActual = waypoints[currentIndex];
        Vector3 direccionHaciaWaypoint = (waypointActual.position - transform.position).normalized;

        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, speedRotation * Time.fixedDeltaTime);

        float anguloDiferencia = Vector3.Angle(transform.forward, direccionHaciaWaypoint);
        float alineacion = Mathf.Clamp01(1f - anguloDiferencia / 90f);

        rb.AddForce(transform.forward * bananaForce * alineacion, ForceMode.Force);

        if (Vector3.Distance(transform.position, waypointActual.position) < waypointDistance)
        {
            currentIndex = (currentIndex + 1) % waypoints.Count;
        }
    }
}
