using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileCollided : MonoBehaviour
{
    public int SFXIdx = 6;

    void OnCollisionEnter(Collision collision)
    {
        if (IsRootOrDescendant(collision.gameObject, "Root"))
        {
            Debug.Log("Collision with Root or one of its descendants!");

            if (SFXIdx >= 0)
                AudioManager.Instance.PlayAudio(SFXIdx);
        }
    }

    bool IsRootOrDescendant(GameObject obj, string rootName)
    {
        // Check if the root object is "Root"
        if (obj.name.Contains(rootName))
            return true;

        // Traverse down the hierarchy from "Root" to check all descendants
        Transform rootTransform = GameObject.Find(rootName)?.transform;
        if (rootTransform == null)
            return false; // "Head" not found in the hierarchy

        foreach (Transform child in rootTransform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject == obj)
                return true; // The collided object is one of Root's descendants
        }

        return false; // Not Root or its descendants
    }

}
