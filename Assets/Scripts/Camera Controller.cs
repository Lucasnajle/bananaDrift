using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    void Start()
    {
        cameras[1].SetActive(true);
    }
}
