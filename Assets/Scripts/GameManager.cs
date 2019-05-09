using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //allow to prefab and instantiate but doesnt allow to use them in script??? why must be cause he has clone name
    //Use find GameObject.FindGameObjectWithTag("tag"); and then use gameobject.GetComponent<GameManager>();
    public static GameManager instance = null;
    public GameObject tile;
    public GameObject blackPawn;
    public GameObject whitePawn;
    public GameObject emptyPawn;
    public GameObject startButton;
    private int MAX = 3;
    private Transform boardHolder;
    private Transform pieceHolder;
    private Transform emptyHolder;
    private List<string> collectListBoardDate;
    private string boardStringValue;
    private string revBoardStringValue;
    private string firstThreeLetters="";
    private string midThreeLetters="";
    private string LastThreeLetters = "";
    private string[,] curBoardStringInfo;
    private string firstRevThreeLetters = "";
    private string midRevThreeLetters = "";
    private string LastRevThreeLetters = "";
    private string[,] curRevBoardStringInfo;
    private GameObject curPref;
    private GameObject newPref;
    private Color curColour = Color.white;
    private Color yellowColour = Color.yellow;
    private bool selectedPiece = false;
    private GameObject[,] pawnObj;
    private GameObject[,] boardObj;
    public bool playerTurn = true;
    public bool endGame = false;
    private int turnCount = 1;
    private GameObject findBMGameObject;
    private BotManager BM;

    private static readonly List<string> algList = new List<string>()
        {
            //TURN 2
            "EWWWEEBBB","WEWEWEBBB",
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
        collectListBoardDate = new List<string>();

        curBoardStringInfo = new string[MAX, MAX];

        curRevBoardStringInfo = new string[MAX, MAX];
        boardStringValue = "";
        revBoardStringValue = "";
        pawnObj = new GameObject[MAX, MAX];

        boardObj = new GameObject[MAX, MAX];
        boardHolder = new GameObject("Board").transform;
        pieceHolder = new GameObject("Piece").transform;


        findBMGameObject = GameObject.FindGameObjectWithTag("BM");
        BM = findBMGameObject.GetComponent<BotManager>();

        CreateEverything();
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

                for (int y = 0; y < MAX; y++)
                    for (int x = 0; x < MAX; x++)
                    {

                        Destroy(pawnObj[x, y]);
                        Destroy(boardObj[x, y]);
                    }

                turnCount = 1;
                playerTurn = true;

                curPref = null;
                newPref = null;
 
                BoardSetup();
                GeneratePieces();
                EndGame(true);

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
    private void CreateEverything()
    {
        Instantiate(startButton, new Vector3(30, 20, 1), Quaternion.identity);
        BoardSetup();
        GeneratePieces();
    }

    private void BoardSetup()
    {

        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                //instance tile
                boardObj[x,y] = Instantiate(tile, new Vector3(x * 10, y * 10, 1), Quaternion.identity);
                boardObj[x, y].transform.SetParent(boardHolder);
                //Debug.Log(instance.transform.position);
            }
        }

       

    }


    private void GeneratePieces()
    {
        //bool isPieceWhite = (y > 1) ? false : true;
        //GameObject instance = Instantiate(isPieceWhite ? whitePawn : blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));

        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                if (y == 0)
                {
                    pawnObj[x, y] = Instantiate(whitePawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    curBoardStringInfo[x, y] = "W";
                }
                else if (y == 1)
                {
                    pawnObj[x, y] = Instantiate(emptyPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    curBoardStringInfo[x, y] = "E";
                }
                else
                {
                    pawnObj[x, y] = Instantiate(blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));

                    curBoardStringInfo[x, y] = "B";
                }

                pawnObj[x, y].transform.SetParent(pieceHolder);
            }
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
                        if (pawnObj[x, y].transform.gameObject.tag == "WhitePawn")

                            pawnObj[x, y].GetComponent<MeshRenderer>().material.color = curColour;
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
                            MovePiece2((int)curPref.transform.position.x / 10, (int)curPref.transform.position.y / 10, (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");


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
                            MovePiece2((int)curPref.transform.position.x / 10, (int)curPref.transform.position.y / 10, (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");
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

    public void MovePiece
        (GameObject oldPositionPref, GameObject targetPosition, string letter)
    {
        //Swap current position with new position and save position to array. 
        //Old position store an empty cube and give it an old position

        // oldPositionPref.transform.position =targetPosition.transform.position; // replace old position with target position
        // targetPosition.transform.position = tempObjectPosition;


        //  GameObject tempGameObject = pawnObj[(int)oldPositionPref.transform.position.x / 10, (int)oldPositionPref.transform.position.y / 10];

        //  pawnObj[(int)oldPositionPref.transform.position.x / 10, (int)oldPositionPref.transform.position.y / 10] = pawnObj[(int)targetPosition.transform.position.x / 10, (int)targetPosition.transform.position.y / 10];
        //  pawnObj[(int)targetPosition.transform.position.x / 10, (int)targetPosition.transform.position.y / 10] = tempGameObject;
        // print(pawnObj[(int)oldPositionPref.transform.position.x / 10, (int)oldPositionPref.transform.position.y / 10].transform.position);
        // print(pawnObj[(int)targetPosition.transform.position.x / 10, (int)targetPosition.transform.position.y / 10].transform.position);



        // Vector3 tempObjectPosition = pawnObj[(int)oldPositionPref.transform.position.x / 10, (int)oldPositionPref.transform.position.y / 10].transform.position; // set position for Empty Object
        //  print("tempposition: "+tempObjectPosition);

        //pawnObj[(int)oldPositionPref.transform.position.x / 10, (int)oldPositionPref.transform.position.y / 10].transform.position = pawnObj[(int)targetPosition.transform.position.x / 10, (int)targetPosition.transform.position.y / 10].transform.position;
        //pawnObj[(int)targetPosition.transform.position.x / 10, (int)targetPosition.transform.position.y / 10].transform.position = tempObjectPosition;
        //save the position of the gameobject
        //save gameobject in pawnObject
        print(oldPositionPref + "\n");
        print(oldPositionPref.transform.position + "\n");
        print(targetPosition + "\n");
        print(targetPosition.transform.position + "\n");


        //        pawnObj[0, 0].transform.position = 
        print("******\n");
        print(pawnObj[0, 0] + "\n");
        print(pawnObj[0, 0].transform.position + "\n");
        print(pawnObj[0, 1] + "\n");
        print(pawnObj[0, 1].transform.position + "\n");




        GameObject tempGameObject = targetPosition;
        targetPosition = oldPositionPref;
        oldPositionPref = tempGameObject;
        //    pawnObj[0, 0] = targetPosition;
        //  pawnObj[0, 1] = oldPositionPref;




        print(pawnObj[0, 0] + "\n");
        print(pawnObj[0, 0].transform.position + "\n");
        print(pawnObj[0, 1] + "\n");
        print(pawnObj[0, 1].transform.position + "\n");
        //saves the data
        curBoardStringInfo[(int)(oldPositionPref.transform.position.x / 10), (int)(oldPositionPref.transform.position.y / 10)] = "E"; //save data
        curBoardStringInfo[(int)(targetPosition.transform.position.x / 10), (int)(targetPosition.transform.position.y / 10)] = letter;//save data

        //curBoardStringInfo[] = "E"; //save data
        //curBoardStringInfo[] = letter;//save data


        UpdateBoardData();//send data

        foreach (GameObject stuff in pawnObj)
        {

            // print(stuff+"\n");
            // print(stuff.transform.position + "\n");
        }
    }

    public void MovePiece2(int x1, int y1, int x2, int y2, string letter)
    {
        //print(pawnObj[x1, y1] + "\n");
        //print(pawnObj[x1, y1].transform.position + "\n");
        //print(pawnObj[x2, y2] + "\n");
        //print(pawnObj[x2, y2].transform.position + "\n");

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

        curBoardStringInfo[x1, y1] = "E"; //save data
        curBoardStringInfo[x2, y2] = letter;//save data
        UpdateBoardData();
    }


    void UpdateBoardData()
    {

        collectListBoardDate.Clear();
        firstThreeLetters="";
        midThreeLetters = "";
        LastThreeLetters = "";
        firstRevThreeLetters = "";
        midRevThreeLetters = "";
        LastRevThreeLetters = "";
        //This whole step is required or it will print System.string[,]
        //Collect data from board and place it into a single string
        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                collectListBoardDate.Add(curBoardStringInfo[x, y]);
                if (y == 0)
                {
                    firstThreeLetters += curBoardStringInfo[x, y];
                }
                else if (y == 1)
                {

                    midThreeLetters += curBoardStringInfo[x, y];

                }
                else
                {

                    LastThreeLetters += curBoardStringInfo[x, y];

                }
            }
        }

        //reverse string preparation for mirror algorithm check
        //reverse in groups of three
        for (int y = MAX - 1; y >= 0; y--)
        {
            for (int x = MAX - 1; x >= 0; x--)
            {
                if (y == 0)
                {
                    firstRevThreeLetters += curBoardStringInfo[x, y];
                }
                else if (y == 1)
                {

                    midRevThreeLetters += curBoardStringInfo[x, y];
                }
                else
                {

                    LastRevThreeLetters += curBoardStringInfo[x, y];
                }
            }
        }



        revBoardStringValue = string.Join("",firstRevThreeLetters, midRevThreeLetters, LastRevThreeLetters);
        //List<> revCompareBoardtoalgo = firstRevThreeLetters.Concat(firstRevThreeLetters).Concat(midRevThreeLetters)
        // concat each string into one string
        boardStringValue = string.Concat(collectListBoardDate);
        print(string.Concat(boardStringValue));

        print("Turn: " + turnCount++);


        EndGame(false);
    }
    public bool EndGame(bool isRestartGame)
    {
        //EEWWEBBEE // AI WINS
        //WEEBEWEEB// AI WINS
        //EWEWBWBEB // PLAYER WINS
        //EWEWBEBEE // AI WINS
        //EEEEWEEBE   //PLAYER WINS
        //EEWEWBEBE //AI WINS
        //WEEBWEEBE //AI WINS
        if (!boardStringValue.Contains("W") &&!isRestartGame)
        {

            print("AI wins No more W piece");
            return true;
        }
        else if (!boardStringValue.Contains("B") && !isRestartGame)
        {
            print("Player wins No more B piece");
            return true;
        }
        else if (firstThreeLetters.Contains("B") && !isRestartGame)
        {

            print("AI wins crossed the finish line");
            return true;
        }
        else if (LastThreeLetters.Contains("W") && !isRestartGame)
        {
            print("Player wins crossed the finish line");
            return true;
        }
        else if (BM.noMoreBotMoves && !isRestartGame)
        {
            print("Bot unable to move, player Wins!");
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool revSelectedTrue = false;
    public int SendIndexToAlgorithms()
    {
        print(algList.IndexOf(boardStringValue));
        print(algList.IndexOf(revBoardStringValue));
        int idexList = algList.IndexOf(boardStringValue);

        if((idexList ==7)|| (idexList == 9) || (idexList == 15) || (idexList == 16) || (idexList == 17) || (idexList == 18) || (idexList == 20) ||
            (idexList == 21) || (idexList == 22) || (idexList == 23) )
        {
            revSelectedTrue = false;
            return algList.IndexOf(boardStringValue);

        }
        else if (algList.IndexOf(boardStringValue) == -1)
        {
            revSelectedTrue = true;
            return algList.IndexOf(revBoardStringValue);
        }
        else
        {
            revSelectedTrue = false;
            return algList.IndexOf(boardStringValue);
        }
    }

}
