using UnityEngine;

public class EndGameManager : MonoBehaviour
{

    //allow to prefab and instantiate
    public static EndGameManager instance = null;

    private GameObject findBMGameObject;
    private BotManager BM;
    private GameObject findGMGameObject;
    private GameManager GM;
    public Material playerTurn;
    public Material aiTurn;
    public Material gameOver;
    public bool isEndGameTriggered = false;
    public bool isAILost = false;

    public int playerScoreCount = 0;
    public int aiScoreCount = 0;
    public int gameOverIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        findBMGameObject = GameObject.FindGameObjectWithTag("BM");
        BM = findBMGameObject.GetComponent<BotManager>();
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();
        gameObject.GetComponent<MeshRenderer>().material = playerTurn;
    }

    // Update is called once per frame
    void Update()
    {


        if (isEndGameTriggered)
        {
            gameObject.GetComponent<MeshRenderer>().material = gameOver;
        }
        else if (GM.isPlayerTurn)
        {
            gameObject.GetComponent<MeshRenderer>().material = playerTurn;

        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = aiTurn;

        }
        if (!isEndGameTriggered)
        {
            Ending(); // the above line will trigger but if end game is triggered it will be replaced
            //ResetGameInterval();
        }
    }

    void Ending()
    {

        if (!GM.pawnPositionsIntoLongString.Contains("W"))//aiWinPlayerCantMove
        {
            isEndGameTriggered = true;
            aiScoreCount++;
            gameOverIndex = 1;
        }
        else if (!GM.pawnPositionsIntoLongString.Contains("B"))//playerWinAiCantMove
        {
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
            gameOverIndex = 2;
        }
        else if (GM.threeLetters[0].Contains("B"))//ai Cross finish line
        {
            isEndGameTriggered = true;
            aiScoreCount++;
            gameOverIndex = 3;
        }
        else if (GM.threeLetters[2].Contains("W")) //player Cross finish line
        {
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
            gameOverIndex = 4;
        }
        else if (!BM.isAICanMove) //print("Bot unable to move, player won!");
        {        
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
            gameOverIndex = 5;
        }
        else if (!GM.isCanPlay && GM.isPlayerTurn)//print("plyer cant move, bot won");
        {
            aiScoreCount++;
            isEndGameTriggered = true;
            gameOverIndex = 6;
        }

    }
}
