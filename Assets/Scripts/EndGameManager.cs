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
    public Material playerCross;
    public Material aiCross;
    public Material playerWinAiCantMove;
    public Material aiWinPlayerCantMove;
    public bool isEndGameTriggered = false;
    public bool isAILost = false;
    public float interval = 3f;
    public int playerScoreCount = 0;
    public int aiScoreCount = 0;
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
        if (!isEndGameTriggered)
        {


            if (GM.isPlayerTurn)
            {
                gameObject.GetComponent<MeshRenderer>().material = playerTurn;

            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = aiTurn;


            }
            Ending(); // the above line will trigger but if end game is triggered it will be replaced
        }
        else
        {
            ResetGameInterval();

        }



    }

    void Ending()
    {

        if (!GM.pawnPositionsIntoLongString.Contains("W"))//aiWinPlayerCantMove
        {
            gameObject.GetComponent<MeshRenderer>().material = aiWinPlayerCantMove;
            print("AI wins No more W piece");
            isEndGameTriggered = true;
            aiScoreCount++;
        }
        else if (!GM.pawnPositionsIntoLongString.Contains("B"))//playerWinAiCantMove
        {
            gameObject.GetComponent<MeshRenderer>().material = playerWinAiCantMove; //ai lose
            print("Player wins No more B piece");
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
        }
        else if (GM.threeLetters[0].Contains("B"))//ai Cross finish line
        {

            gameObject.GetComponent<MeshRenderer>().material = aiCross;
            print("AI wins crossed the finish line");
            isEndGameTriggered = true;
            aiScoreCount++;

        }
        else if (GM.threeLetters[2].Contains("W")) //player Cross finish line
        {
            gameObject.GetComponent<MeshRenderer>().material = playerCross; //ai lose
            print("Player wins crossed the finish line");
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
        }
        else

            //what was the reason why i separate this? to separte the different lose
            // or they will trigger at the same time
            if (!BM.isAIcanMove)
        {
            gameObject.GetComponent<MeshRenderer>().material = playerWinAiCantMove; //ai lose
            print("Bot unable to move, player Wins!");
            isAILost = true;
            BM.AILostRemoveMoves2();
            isEndGameTriggered = true;
            playerScoreCount++;
        }
        else

            if (!GM.isCanPlay && GM.isPlayerTurn)
        {

            gameObject.GetComponent<MeshRenderer>().material = aiWinPlayerCantMove;
            print("plyer cant move, bot wins");
            isEndGameTriggered = true;
            aiScoreCount++;

        }


    }
    private void ResetGameInterval()
    {

        if (interval > 0)
        {
            //    interval -= Time.deltaTime;
        }
        else
        {
            isEndGameTriggered = true;
            GM.RestartGame();
        }

    }
}
