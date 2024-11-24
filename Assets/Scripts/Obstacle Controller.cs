using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Vector3 direction;
    public float force;

    public int SFXIdx = 2;
    void OnTriggerEnter(Collider theCollider) {
        if (theCollider.gameObject.name.Contains("Root")) {
            if (SFXIdx >= 0)
                AudioManager.Instance.PlayAudio(SFXIdx);
            GameController.Instance.playerList[GameController.Instance.currentPlayerIndex].GetComponent<ApplyForceOnKeyPress>().ApplyForce(direction + 0.2f * Vector3.up, force);
        }
    }
}
