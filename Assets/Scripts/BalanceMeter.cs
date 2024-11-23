using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
public class BalanceMeter : MonoBehaviour
{
    private const float MAX_ANGLE = 90;
    private const float MAX_ARROW_ANGLE = 45;
    private Image arrow;
    [Range(-MAX_ANGLE, MAX_ANGLE)]
    public float currentAngle = 0;
    private float currentAngleRad = 0;

    public float elipseA = 10;
    public float elipseB = 10;

    public Vector3 startingPos = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrow = GetComponentsInChildren<Image>()[1];

    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = Mathf.Clamp(currentAngle, -MAX_ANGLE, MAX_ANGLE);
        currentAngleRad = currentAngle * Mathf.Deg2Rad;
        arrow.transform.localPosition = startingPos + Mathf.Sin(currentAngleRad) * elipseA * Vector3.right + (Mathf.Cos(currentAngleRad) * elipseB) * Vector3.up;
        //arrow.transform.LookAt(startingPos);
        //arrow.transform.rotation = Quaternion.Euler(arrow.transform.rotation.eulerAngles.x,0,arrow.transform.rotation.eulerAngles.z);
        //arrow.transform.rotation = Quaternion.Euler(0,0,MAX_ARROW_ANGLE * (MAX_ANGLE - Mathf.Abs(currentAngle)));
    }
}
