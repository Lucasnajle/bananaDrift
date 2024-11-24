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

    private Vector3 originalPosition;
    private Vector3 velocity = Vector3.zero;
    public float oscilationAmpX = 0.1f;
    public float oscilationAmpY = 0.1f;
    public float oscilationAmpZ = 0.1f;
    public float frequency = 10f; // Speed of vibration
    public float springStrength = 10f; // Strength of the spring force
    public float damping = 0.5f; // Damping to prevent oscillation over time

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {

        //MOVIMIENTO GLOBAL - RUTA
        if (waypoints.Count == 0) return;

        Transform waypointActual = waypoints[currentIndex];
        Vector3 direccionHaciaWaypoint = (waypointActual.position - transform.position).normalized;

        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaWaypoint);
        rotacionObjetivo = Quaternion.Euler(0, rotacionObjetivo.eulerAngles.y, 0); // Restrict rotation to Y axis only
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, speedRotation * Time.fixedDeltaTime);

        float anguloDiferencia = Vector3.Angle(transform.forward, direccionHaciaWaypoint);
        float alineacion = Mathf.Clamp01(1f - anguloDiferencia / 90f);

        rb.AddForce(transform.forward * bananaForce * alineacion, ForceMode.Force);

        if (Vector3.Distance(transform.position, waypointActual.position) < waypointDistance)
        {
            currentIndex = (currentIndex + 1) % waypoints.Count;
        }


        //MOVIMIENTO LOCAL

        // Vibrations
        float offsetX = Mathf.Sin(Time.time * frequency) * oscilationAmpX;
        float offsetY = Mathf.Sin(Time.time * frequency) * oscilationAmpY;
        float offsetZ = Mathf.Cos(Time.time * frequency) * oscilationAmpZ;

        Vector3 targetPosition = new Vector3(offsetX, offsetY, offsetZ);
        Vector3 forceDirection = targetPosition;

        // Spring force to return the object to its original Y position
        float springForce = (originalPosition.y - rb.position.y) * springStrength;
        Vector3 springForceVector = new Vector3(0f, springForce, 0f);

        // Total force
        rb.AddForce(forceDirection + springForceVector - velocity * damping, ForceMode.VelocityChange);
        // Update velocity for damping calculation
        velocity = rb.linearVelocity;
    }
}
