using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Vector3 direction;
    public float force;
    void OnTriggerEnter(Collider theCollider) {
        if (theCollider.gameObject.name.Contains("Root")) {
            GameController.Instance.playerList[GameController.Instance.currentPlayerIndex].GetComponent<ApplyForceOnKeyPress>().ApplyForce(direction + 0.2f * Vector3.up, force);
        }
    }
}
