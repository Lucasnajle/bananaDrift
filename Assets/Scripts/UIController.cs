using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mainMenu;
    public Image gameplayUI;
    public Image[] lifes;
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

    public void SetHealthAmt(int health)
    {
        for(int i = lifes.Length -1; i > health - 1; i--)
        {
            lifes[i].enabled = false;
        }
    }
    


}
