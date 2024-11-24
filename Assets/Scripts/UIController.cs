using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mainMenu;
    public Image winUI;
    public Image loseUI;
    public Image creditsUI;
    public TextMeshProUGUI remainingDistanceText;
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

    public void SetRemainingText(string txt)
    {
        remainingDistanceText.text = txt;
    }

    public void GameStart()
    {
        
        gameplayUI.transform.DOLocalMoveY(100,.75f).SetEase(Ease.OutCubic).SetDelay(2f);
        AudioManager.Instance.PlayEngine();
        GameController.Instance.StartGame();
    }

    public void Win()
    {
        gameplayUI.transform.DOLocalMoveY(1000,.75f).SetEase(Ease.OutCubic);
        winUI.transform.DOLocalMoveY(0,.75f).SetEase(Ease.OutCubic).SetDelay(2f);
    }

    public void Lose()
    {
        gameplayUI.transform.DOLocalMoveY(1000,.75f).SetEase(Ease.OutCubic);
        loseUI.transform.DOLocalMoveY(0,.75f).SetEase(Ease.OutCubic).SetDelay(2f);
    }

    public void CreditsShow()
    {
        mainMenu.transform.DOLocalMoveY(1000,.75f).SetEase(Ease.OutCubic);
        creditsUI.transform.DOLocalMoveY(0,.75f).SetEase(Ease.OutCubic);

        
    }
    public void CreditsHide()
    {
        mainMenu.transform.DOLocalMoveY(75,.75f).SetEase(Ease.OutCubic);
        creditsUI.transform.DOLocalMoveY(-1400,.75f).SetEase(Ease.OutCubic);

        
    }
    

    public void SetHealthAmt(int health)
    {
        for(int i = lifes.Length -1; i > health - 1; i--)
        {
            lifes[i].enabled = false;
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }
    
    public void Restart()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

}
