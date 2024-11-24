using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform banana;
    public Transform bananaTarget;
    public List<Transform> playerList;
    public const int MAX_PLAYER = 1;
    public int currentPlayerIndex = 0;
    public float waveHeight;

    private float pathLength = 0;
    private PathController pathController;
    public List<GameObject> cameras = new List<GameObject>();
    public void StartGame()
    {
        cameras[1].SetActive(true);
        float bananaTargetSpeed = 0f;
        float MAX_TARGET_SPEED = 6f;
        float duration = 5f;
        DOTween.To(() => bananaTargetSpeed, x => bananaTargetSpeed = x, MAX_TARGET_SPEED, duration).OnUpdate(() =>{
            bananaTarget.GetComponent<PathController>().speed = bananaTargetSpeed;
        }).OnComplete(()=>{AudioManager.Instance.PlayEngine();});
        float bananaForce = 0f;
        float MAX_BANANA_FORCE = 5000f;
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

    public void Win()
    {
        cameras[0].SetActive(false);
        cameras[1].SetActive(false);
        cameras[2].SetActive(true);

        bananaTarget.GetComponent<PathController>().speed = 0f;
        banana.GetComponent<BananaMovementController>().bananaForce = 0;
 
        GetCurrentPlayer().GetComponent<Player>().SetKinematic(true);
        banana.GetComponent<Rigidbody>().isKinematic = true;

        UIController.Instance.Win();
    }

    public void Lose()
    {
        cameras[0].SetActive(false);
        cameras[1].SetActive(false);
        cameras[2].SetActive(true);

        bananaTarget.GetComponent<PathController>().speed = 0f;
        banana.GetComponent<BananaMovementController>().bananaForce = 0;

        banana.GetComponent<Rigidbody>().isKinematic = true;

        UIController.Instance.Lose();
    }
}
