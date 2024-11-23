using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mainMenu;
    public Image gameplayUI;
     void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameStart()
    {
        mainMenu.transform.DOLocalMoveY(1000,.75f).SetEase(Ease.OutCubic);
        gameplayUI.transform.DOLocalMoveY(0,.75f).SetEase(Ease.OutCubic).SetDelay(.3f);
        AudioManager.Instance.PlayEngine();
        GameController.Instance.StartGame();
    }
    


}
