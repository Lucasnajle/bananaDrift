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
    public bool isFinished = false;

    public float movementThreshold = 1f;
    public ParticleSystem particleSystem;


    private void Awake()
    {
        splineLength = spline.CalculateLength();
        remainingDistance = splineLength;
        //SetDistanceText();
    }

    // Update is called once per frame
    void Update()
    {
        BoatParticles();

        if (!isFinished) {
            if (distancePercentage >= 1)
                return;
            distancePercentage += speed * Time.deltaTime / splineLength;
            remainingDistance = splineLength - (splineLength * distancePercentage);

            SetDistanceText();
            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

            if (distancePercentage > 1f)
            {
                GameController.Instance.Win();
            }

            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.005f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
        }
    }

    private void SetDistanceText()
    {
        UIController.Instance.SetRemainingText($"{remainingDistance.ToString("00")}");
    }

    private void BoatParticles()
    {
        if (particleSystem != null)
        {
            // Check if the object's velocity magnitude is greater than the threshold
            if (speed > movementThreshold)
            {
                if (!particleSystem.isPlaying)
                {
                    Debug.Log("Play");
                    particleSystem.Play(); // Start emitting particles if moving
                }
            }
            else
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop(); // Stop emitting particles if not moving
                }
            }
        }
    }
}