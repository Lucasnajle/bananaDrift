using UnityEngine;

public class WinController : MonoBehaviour
{
    void OnTriggerEnter(Collider theCollider) {
        if (theCollider.tag == "Banana") {
            GameController.Instance.Win();
        }
    }
}
