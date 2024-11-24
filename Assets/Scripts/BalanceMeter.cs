using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using DG.Tweening;
public class BalanceMeter : MonoBehaviour
{
    private const float MAX_ANGLE = 90;
    private const float MAX_ARROW_ANGLE = 60;
    private Image arrow;
    [Range(-MAX_ANGLE, MAX_ANGLE)]
    public float currentAngle = 0;
    private float currentAngleRad = 0;

    public float elipseA = 10;
    public float elipseB = 10;

    public Vector3 startingPos = Vector3.zero;

    public Transform hips;
    public Transform handle;

    private float initialDistance = 0;
    private float currentDistance = 0;
    private float initialPosX = 0;
    public float HIPS_OFFSET = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrow = GetComponentsInChildren<Image>()[1];
        initialPosX = hips.localPosition.x;
        initialDistance = 0;//handle.forward.x - hips.forward.x;

    }

    // Update is called once per frame
    void Update()
    {
        currentDistance =  initialPosX - hips.localPosition.x;//handle.transorm.position.x - hips.transorm.position.x;
        currentAngle = (initialDistance - currentDistance) * HIPS_OFFSET;
        currentAngle = Mathf.Clamp(currentAngle, -MAX_ANGLE, MAX_ANGLE);
        currentAngleRad = currentAngle * Mathf.Deg2Rad;
        arrow.transform.localPosition = startingPos + Mathf.Sin(currentAngleRad) * elipseA * Vector3.right + Mathf.Cos(currentAngleRad) * elipseB * Vector3.up;
        arrow.transform.rotation = Quaternion.Euler(0,0,MAX_ARROW_ANGLE * (-currentAngle/MAX_ANGLE));

    }
}
