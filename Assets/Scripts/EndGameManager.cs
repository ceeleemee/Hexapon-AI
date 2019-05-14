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
            Ending();
        }


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
        }



    }
    void Ending()
    {
        if (!GM.allPawnPositionsIntoLongString.Contains("W"))
        {
            gameObject.GetComponent<MeshRenderer>().material = aiWinPlayerCantMove;
            print("AI wins No more W piece");
            isEndGameTriggered = true;
        }
        else if (!GM.allPawnPositionsIntoLongString.Contains("B"))
        {
            gameObject.GetComponent<MeshRenderer>().material = playerWinAiCantMove;
            print("Player wins No more B piece");
            isEndGameTriggered = true;
        }
        else if (GM.firstThreeLetters.Contains("B"))
        {
            gameObject.GetComponent<MeshRenderer>().material = aiCross;
            print("AI wins crossed the finish line");
            isEndGameTriggered = true;
        }
        else if (GM.LastThreeLetters.Contains("W"))
        {
            gameObject.GetComponent<MeshRenderer>().material = playerCross;
            print("Player wins crossed the finish line");
            isEndGameTriggered = true;
        }

        if (!BM.isAIcanMove)
        {
            gameObject.GetComponent<MeshRenderer>().material = playerWinAiCantMove;
            print("Bot unable to move, player Wins!");
            isEndGameTriggered = true;
        }

        if (!GM.IsPlayerCanMove() && GM.isPlayerTurn)
        {
            gameObject.GetComponent<MeshRenderer>().material = aiWinPlayerCantMove;
            print("plyer cant move, bot wins");
            isEndGameTriggered = true;
        }
    }
}
