using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public List<GameObject> boats = new List<GameObject>();
    void Start()
    {
        boats[Random.Range(0, boats.Count)].SetActive(true);
    }
}
