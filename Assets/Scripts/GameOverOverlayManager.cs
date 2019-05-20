using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlayManager : MonoBehaviour
{
    public GameObject gameOverOverlay;
    public Text gameOverText;


    private GameObject findEGMGameObject;
    private EndGameManager EGM;
    private GameObject findGMGameObject;
    private GameManager GM;
    public float interval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        findEGMGameObject = GameObject.FindGameObjectWithTag("EGM");
        EGM = findEGMGameObject.GetComponent<EndGameManager>();
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();
        //gameOverResetGame = GetComponent<Button>();
    }

    public void ResetBUtton()
    {
        GM.RestartGame();
        gameOverOverlay.gameObject.SetActive(false);
        interval = 2f;
    }
    // Update is called once per frame
    void Update()
    {
        switch (EGM.gameOverIndex)
        {
            case 0:
                gameOverOverlay.gameObject.SetActive(false);
                break;
            case 1:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "AI won, no more white pawn!";
                }
                break;
            case 2:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "Player won, no more black pawn!";
                }
                break;
            case 3:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "AI won, crossed the finish line!";
                }
                break;
            case 4:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "Player won, crossed the finish line!";
                }
                break;
            case 5:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "AI unable to move, player won!";
                }
                break;
            case 6:
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                else
                {
                    gameOverOverlay.gameObject.SetActive(true);
                    gameOverText.text = "Player cannot move, AI won";
                }
                break;
        }

    }

}
