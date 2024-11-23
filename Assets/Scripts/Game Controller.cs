using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform banana;
    public List<Transform> playerList;
    public int currentPlayerIndex = 0;
    public float waveHeight;

    private float pathLength = 0;
    private PathController pathController;
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
