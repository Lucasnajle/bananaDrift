using System.Collections.Generic;
using UnityEngine;

public class BananaModelController : MonoBehaviour
{
    public List<GameObject> bananas = new List<GameObject>();
    void Start()
    {
        bananas[Random.Range(0, bananas.Count)].SetActive(true);
    }
}
