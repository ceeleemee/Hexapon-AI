using System.Collections.Generic;
using UnityEngine;

public class GameManagerListAttempt : MonoBehaviour
{
    //allow to prefab and instantiate but doesnt allow to use them in script??? why must be cause he has clone name
    //Use find GameObject.FindGameObjectWithTag("tag"); and then use gameobject.GetComponent<GameManager>();
   // public static GameManager instance = null;

    public GameObject tile;

    public GameObject blackPawn;

    public GameObject whitePawn;
    public GameObject emptyPawn;
    public GameObject startButton;
    private int MAX = 3;
    private Transform boardHolder;
    private Transform pieceHolder;
    private Transform emptyHolder;
    private List<string> collectBoardDate;
    private string compareBoardToAlgo;
    private List<string> firstThreeLetters;
    private List<string> LastThreeLetters;
    private string[,] curBoardStringInfo;
    private GameObject curPref;
    private GameObject newPref;
    private Color curColour = Color.white;
    private Color yellowColour = Color.yellow;
    private bool selectedPiece = false;
    public GameObject[,] pawnObj;
    private List<GameObject> listGP;

    public bool playerTurn = true;
    public bool endGame = false;

    private GameObject findBMGameObject;
    private BotManager BM;

    private static readonly List<string> algList = new List<string>()
        {
            //TURN 2
            "EWWWEEBBB","WEWEWEBBB","WWEEEWBBB",
            //TURN 4
            "EEWBWEBEB","EEWWBEEBB",
            "EWEWWEBEB","EEWWEWBBE","WEEEBWEBB","WEEBWWEBB",
            "EWEBEWBEB","EEWWWBBBE","EEWEWEEBB","WEEEWEEBB",
            "EEWWEEBEB",
            //TURN6
            "EEEBBWEEB","EEEWWWBEE","EEEBWWEBE",
            "EEEWWBEBE","EEEBBWBEE","EEEWBBEEB","EEEBWEEEB",
            "EEEWBEEBE","EEEEBWEBE","EEEBWEBEE","EEEEWBEEB",
        };

    // Start is called before the first frame update
    void Start()
    {
        curPref = GetComponent<GameObject>();// rbPref = Rigidbody; is it different?
        collectBoardDate = new List<string>();
        firstThreeLetters = new List<string>();
        LastThreeLetters = new List<string>();
        listGP = new List<GameObject>();
        compareBoardToAlgo = "";
        curBoardStringInfo = new string[MAX, MAX];
        pawnObj = new GameObject[MAX, MAX];
        boardHolder = new GameObject("Board").transform;
        pieceHolder = new GameObject("Piece").transform;


        findBMGameObject = GameObject.FindGameObjectWithTag("BM");
        BM = findBMGameObject.GetComponent<BotManager>();

        BoardSetup();
        GeneratePieces();
        foreach (GameObject stuff in pawnObj)
        {

            //    print(stuff+"\n");
            //    print(stuff.transform.position + "\n");
        }



    }

    void RestartGame()
    {
        LayerMask MaskStartPiece = LayerMask.GetMask("StartNewGame");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//mouse shoots an array
        float rayLength = 100f;
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, rayLength, MaskStartPiece))
            {
                




            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (playerTurn)
        {


            SelectPieceAndSaveData();
            //print(SendIndexToAlgorithms());

        }
        RestartGame();


    }
    void LateUpdate()
    {

    }

    private void BoardSetup()
    {

        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                //instance tile
                GameObject instance = Instantiate(tile, new Vector3(x * 10, y * 10, 1), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
                //Debug.Log(instance.transform.position);
            }
        }

        Instantiate(startButton, new Vector3(30, 20, 1), Quaternion.identity);

    }


    private void GeneratePieces()
    {
        //bool isPieceWhite = (y > 1) ? false : true;
        //GameObject instance = Instantiate(isPieceWhite ? whitePawn : blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
        GameObject instance;
        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                if (y == 0)
                {
                    instance = Instantiate(whitePawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    instance.name = "White" + x;
                    curBoardStringInfo[x, y] = "W";

                }
                else if (y == 1)
                {
                    instance = Instantiate(new GameObject(), new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    instance.name = "Emptyj" + x;
                    //instance = Instantiate(emptyPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));

                    //instance = new GameObject();
                    //instance.transform.position = new Vector3(x * 10, y * 10, 0);
                    //instance.transform.rotation = Quaternion.Euler(90, 0, 0);
                    curBoardStringInfo[x, y] = "E";
                }
                else
                {
                    instance = Instantiate(blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    instance.name = "Black" + x;
                    curBoardStringInfo[x, y] = "B";
                }
                listGP.Add(instance);
                instance.transform.SetParent(pieceHolder);


            }
        }
        foreach (GameObject stuff in listGP)
        {
            print(stuff+"\n");
        }
       
    }
    private Vector3 target;
    private void SelectPieceAndSaveData()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//mouse shoots an array
        float rayLength = 100f;
        RaycastHit hit;
        LayerMask MaskWhitePiece = LayerMask.GetMask("WhitePiece");
        LayerMask MaskBlackPiece = LayerMask.GetMask("BlackPiece");
        LayerMask MaskEmptyPiece = LayerMask.GetMask("EmptyPiece");
        LayerMask MaskTilePiece = LayerMask.GetMask("Tile");

        //this clicks on mouse
        if (Input.GetMouseButtonDown(0))
        {
            // if (Physics.Raycast(ray, out hit, rayLength)){
            //     print(hit.transform.position);
            //  }
            //Select whitepiece and highlight it with yellow
            if (Physics.Raycast(ray, out hit, rayLength, MaskWhitePiece))
            {
                //refresh colour for whites but not selection.
                for (int y = 0; y < MAX; y++)
                {
                    for (int x = 0; x < MAX; x++)
                    {
                        foreach(GameObject stuff in listGP)
                        {
                            if(stuff.transform.gameObject.tag == "WhitePawn")
                                stuff.GetComponent<MeshRenderer>().material.color = curColour;
                        }

                    }
                }

                if (hit.transform.GetComponent<MeshRenderer>().material.color != yellowColour)
                {
                    curPref = hit.transform.gameObject;
                    curPref.GetComponent<MeshRenderer>().material.color = yellowColour;
                    selectedPiece = true;
                    //GetPawnPositions();
                }

            }

            if (selectedPiece)
            {
                if (Physics.Raycast(ray, out hit, rayLength, MaskBlackPiece))
                {
                    //When white piece is yellow consume blackpiece
                    if ((newPref = hit.transform.gameObject))
                    {
                        //Mathf.Abs(newPref.position.x - curPref.transform.position.x) <= 10 && (newPref.position.y - curPref.transform.position.y)<=10||(newPref.position.y != curPref.transform.position.y) &&(newPref.position.x != curPref.transform.position.x)
                        if (Mathf.Abs(newPref.transform.position.x - curPref.transform.position.x) == 10 && (newPref.transform.position.y - curPref.transform.position.y) == 10)
                        {



                            //                            MovePiece(curPref, newPref, "W");
                            //MovePiece(pawnObj[(int)curPref.transform.position.x/10, (int)curPref.transform.position.y/10], pawnObj[(int)newPref.transform.position.x/10, (int)newPref.transform.position.y/10], "W");
                            

                            curPref.gameObject.GetComponent<MeshRenderer>().material.color = curColour;
                            selectedPiece = false;
                            playerTurn = false;
                        }
                        else
                        {
                            newPref.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }

                }
                else if (Physics.Raycast(ray, out hit, rayLength, MaskTilePiece))
                {
                    if ((newPref = hit.transform.gameObject))
                    {
                        //control distance between selected pref and other objects
                        if ((newPref.transform.position.y - curPref.transform.position.y) == 10 && Mathf.Abs(newPref.transform.position.x - curPref.transform.position.x) == 0)
                        {

                            MovePiece2(curPref, newPref, "W");
                            curPref.gameObject.GetComponent<MeshRenderer>().material.color = curColour;
                            selectedPiece = false;
                            playerTurn = false;
                        }
                        else
                        {
                            newPref.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }
                }
            }

        }

        //This follows mouse around
        /* if (selectedPiece)
         {
             if (Physics.Raycast(ray, out hit))
             {
                 if (rb = hit.transform.GetComponent<Rigidbody>())
                     rb.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
             }
         }*/

        /*
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                selectedPiece = false;
                rbPref.gameObject.GetComponent<MeshRenderer>().material.color = curColour;
                //if (rb = hit.transform.GetComponent<Rigidbody>())
                //rb.transform.position = curPos.position;
            }
        }
        */
    }



    public void MovePiece2(GameObject firstSelected ,GameObject secondSelected, string letter)
    {
        int index1 = 0;
        int index2 = 0;
        foreach(GameObject stuff in listGP)
        {

                index1 =  listGP.IndexOf(firstSelected);
 
        }
        foreach (GameObject stuff in listGP)
        {

            index2 = listGP.IndexOf(secondSelected);

        }

        listGP[index1] = secondSelected;
        listGP[index2] = firstSelected;

        /*
        //Swapping the gameobject inside the array
        GameObject temp = pawnObj[x1, y1];
        pawnObj[x1, y1] = pawnObj[x2, y2];
        pawnObj[x2, y2] = temp;


        //swapping the position of the array
        Vector3 tempPos = pawnObj[x1, y1].transform.position;
        pawnObj[x1, y1].transform.position = pawnObj[x2, y2].transform.position;
        pawnObj[x2, y2].transform.position = tempPos;

        // If black piece attacks white, remove it this is for AI
        //Should do the same for player

        if (pawnObj[x1, y1].name == "White Pawn(Clone)")
        {
            pawnObj[x1, y1].gameObject.SetActive(false);

        }
        else if (playerTurn && pawnObj[x1, y1].name == "Black Pawn(Clone)")
        {
            pawnObj[x1, y1].gameObject.SetActive(false);
        }

        //print(pawnObj[x1, y1] + "\n");
        //print(pawnObj[x1, y1].transform.position + "\n");
        //print(pawnObj[x2, y2] + "\n");
        //print(pawnObj[x2, y2].transform.position + "\n");

        curBoardStringInfo[x1, y1] = "E"; //save data
        curBoardStringInfo[x2, y2] = letter;//save data*/
        UpdateBoardData();
    }
    int count = 1;

    void UpdateBoardData()
    {

        collectBoardDate.Clear();
        firstThreeLetters.Clear();
        LastThreeLetters.Clear();
        //This whole step is required or it will print System.string[,]
        //Collect data from board and place it into a single string
        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                collectBoardDate.Add(curBoardStringInfo[x, y]);
                if (y == 0)
                {
                    firstThreeLetters.Add(curBoardStringInfo[x, y]);
                }
                if (y == MAX - 1)
                {

                    LastThreeLetters.Add(curBoardStringInfo[x, y]);
                }
            }
        }
        // concat each string into one string
        compareBoardToAlgo = string.Concat(collectBoardDate);
        print(string.Concat(compareBoardToAlgo));
        print("Turn: " + count++);

        EndGame();
    }
    public bool EndGame()
    {
        //EEWWEBBEE // AI WINS
        //WEEBEWEEB// AI WINS
        //EWEWBWBEB // PLAYER WINS
        //EWEWBEBEE // AI WINS
        //EEEEWEEBE   //PLAYER WINS
        //EEWEWBEBE //AI WINS
        //WEEBWEEBE //AI WINS
        if (!compareBoardToAlgo.Contains("W"))
        {

            print("AI wins No more W piece");
            return true;
        }
        else if (!compareBoardToAlgo.Contains("B"))
        {
            print("Player wins No more B piece");
            return true;
        }
        else if (firstThreeLetters.Contains("B"))
        {

            print("AI wins crossed the finish line");
            return true;
        }
        else if (LastThreeLetters.Contains("W"))
        {
            print("Player wins crossed the finish line");
            return true;
        }
        else if (BM.noMoreBotMoves)
        {
            print("Bot unable to move, player Wins!");
            return true;
        }
        else
        {
            return false;
        }
    }

    public int SendIndexToAlgorithms()
    {
        //print(algList.IndexOf(compareBoardToAlgo));
        return algList.IndexOf(compareBoardToAlgo);
    }

}
