using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform banana;
    public Transform bananaTarget;
    public List<Transform> playerList;
    public int currentPlayerIndex = 0;
    public float waveHeight;

    private float pathLength = 0;
    private PathController pathController;
    public List<GameObject> cameras = new List<GameObject>();
    public void StartGame()
    {
        cameras[1].SetActive(true);
        float bananaTargetSpeed = 0f;
        float MAX_TARGET_SPEED = 15f;
        float duration = 10f;
        DOTween.To(() => bananaTargetSpeed, x => bananaTargetSpeed = x, MAX_TARGET_SPEED, duration).OnUpdate(() =>{
            bananaTarget.GetComponent<PathController>().speed = bananaTargetSpeed;
        }).OnComplete(()=>{AudioManager.Instance.PlayEngine();});
        float bananaForce = 0f;
        float MAX_BANANA_FORCE = 8000f;
        DOTween.To(() => bananaForce, x => bananaForce = x, MAX_BANANA_FORCE, duration).OnUpdate(() =>{
            banana.GetComponent<BananaMovementController>().bananaForce = bananaForce;
        }).SetDelay(1);
        GetCurrentPlayer().GetComponent<Player>().SetKinematic(false);
        banana.GetComponent<Rigidbody>().isKinematic = false;
    }
    public enum Status {
        IN_GAME,
        PAUSE,
        GAME_OVER
    }
    public Status currentStatus = Status.IN_GAME;
    public float timer;


    private void Awake()
    {
        Instance = this;
        banana.GetComponent<Rigidbody>().isKinematic = true;
    }

    public Transform GetCurrentPlayer()
    {
        return playerList[currentPlayerIndex];
    }


}
