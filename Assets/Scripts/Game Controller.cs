using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameController Instance;
    public Transform banana;
    public List<Transform> playerList;
    public int currentPlayerIndex = 0;
    public float waveHeight;
    public enum Status {
        IN_GAME,
        PAUSE,
        GAME_OVER
    }
    public Status currentStatus = Status.IN_GAME;
    public float timer;


    private void Awake()
    {
        this.Instance = this;
    }
}
