using Unity.Cinemachine;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public CinemachineOrbitalFollow freeLookCamera;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.HorizontalAxis.Value += rotationSpeed * Time.deltaTime;
        }
    }
}
