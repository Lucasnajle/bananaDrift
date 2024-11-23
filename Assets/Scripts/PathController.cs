using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
public class PathController : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 1f;
    private float distancePercentage = 0f;

    private float splineLength;

    public float remainingDistance = 0;

    public TextMeshProUGUI remainingDistanceText;

    private void Awake()
    {
        splineLength = spline.CalculateLength();
        remainingDistance = splineLength;
        SetDistanceText();
    }

    // Update is called once per frame
    void Update()
    {
        if (distancePercentage >= 1)
            return;
        distancePercentage += speed * Time.deltaTime / splineLength;
        remainingDistance = splineLength - (splineLength * distancePercentage);

        SetDistanceText();
        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition;

        if (distancePercentage > 1f)
        {
            distancePercentage = 0f;
        }

        Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.05f);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    private void SetDistanceText()
    {
        remainingDistanceText.text = $"{remainingDistance.ToString("00.00")} m";
    }
}