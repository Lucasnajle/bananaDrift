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
        float MAX_TARGET_SPEED = 2f;
        float duration = 2f;
        DOTween.To(() => bananaTargetSpeed, x => bananaTargetSpeed = x, MAX_TARGET_SPEED, duration).OnUpdate(() =>{
            bananaTarget.GetComponent<PathController>().speed = bananaTargetSpeed;
        });
        float bananaForce = 0f;
        float MAX_BANANA_FORCE = 1000f;
        DOTween.To(() => bananaForce, x => bananaForce = x, MAX_BANANA_FORCE, duration).OnUpdate(() =>{
            banana.GetComponent<BananaMovementController>().bananaForce = bananaForce;
        }).SetDelay(duration);
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
    }


}
