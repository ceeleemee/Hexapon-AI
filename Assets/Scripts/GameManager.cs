﻿using System.Collections.Generic;
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
    private List<string> collectPawnList;
    private string allPawnPositionsIntoLongString;
    public int indexPawnPosition = 0;
    private string mirrorAllPawnPositionIntoLongString;
    private string firstThreeLetters = "";
    private string midThreeLetters = "";
    private string LastThreeLetters = "";
    private string[,] oldPawnPositionArray;
    private string firstRevThreeLetters = "";
    private string midRevThreeLetters = "";
    private string LastRevThreeLetters = "";
    private string[,] curRevBoardStringInfo;
    private GameObject oldPref;
    private GameObject newPref;
    private Color oldColour = Color.white;
    private Color yellowColour = Color.yellow;
    private bool selectedPiece = false;
    private GameObject[,] pawnObjArray;
    private GameObject[,] boardObjArray;
    public bool isPlayerTurn = true;
    public bool isEndGame = false;
    private int turnCount = 1;
    private GameObject findBMGameObject;
    private BotManager BM;


        //Note alogithm 21 and 15 never happens because , algorithm 1 is always going to move the left piece.
        //starting number is 0
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
  /*  private static readonly List<string> algList = new List<string>()
        {
            //TURN 2
            "EWWWEEBBB","WEWEWEBBB",
            //TURN 4
            "EEWBWEBEB","EEWWBEEBB",
            "EWEWWEBEB","EEWWEWBBE",
            "WEEEBWEBB","WEEBWWEBB",
            "EWEBEWBEB","EEWWWBBBE",

            "EEWEWEEBB","WEEEWEEBB",
            "EEWWEEBEB",
            //TURN6
            "EEEBBWEEB","EEEWWWBEE",
            "EEEBWWEBE","EEEWWBEBE",
            "EEEWBBEEB","EEEBBWBEE",
            "EEEBWEEEB","EEEWBEEBE",
            "EEEEBWEBE","EEEEWBEEB",
            "EEEBWEBEE",
        };*/

    public bool isRevSelectedTrue = false;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {


        oldPref = GetComponent<GameObject>();// rbPref = Rigidbody; is it different?
        collectPawnList = new List<string>();

        oldPawnPositionArray = new string[MAX, MAX];

        curRevBoardStringInfo = new string[MAX, MAX];
        allPawnPositionsIntoLongString = "";
        mirrorAllPawnPositionIntoLongString = "";
        pawnObjArray = new GameObject[MAX, MAX];

        boardObjArray = new GameObject[MAX, MAX];
        boardHolder = new GameObject("Board").transform;
        pieceHolder = new GameObject("Piece").transform;


        findBMGameObject = GameObject.FindGameObjectWithTag("BM");
        BM = findBMGameObject.GetComponent<BotManager>();

        CreateEverything();
        foreach (GameObject stuff in pawnObjArray)
        {

            //    print(stuff+"\n");
            //    print(stuff.transform.position + "\n");
        }



    }
    void Update()
    {

        if (isPlayerTurn)
        {


            SelectPieceAndSaveData();
            //print(SendIndexToAlgorithms());

        }
        RestartGame();


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

                        Destroy(pawnObjArray[x, y]);
                        Destroy(boardObjArray[x, y]);
                    }

                turnCount = 1;
                isPlayerTurn = true;

                oldPref = null;
                newPref = null;

                BoardSetup();
                GeneratePieces();
                collectPawnList.Clear();
                firstThreeLetters = "";
                midThreeLetters = "";
                LastThreeLetters = "";
                firstRevThreeLetters = "";
                midRevThreeLetters = "";
                LastRevThreeLetters = "";


            }

        }


    }


    // Update is called once per frame
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
                boardObjArray[x, y] = Instantiate(tile, new Vector3(x * 10, y * 10, 1), Quaternion.identity);
                boardObjArray[x, y].transform.SetParent(boardHolder);
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
                    pawnObjArray[x, y] = Instantiate(whitePawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    oldPawnPositionArray[x, y] = "W";
                }
                else if (y == 1)
                {
                    pawnObjArray[x, y] = Instantiate(emptyPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    oldPawnPositionArray[x, y] = "E";
                }
                else
                {
                    pawnObjArray[x, y] = Instantiate(blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));

                    oldPawnPositionArray[x, y] = "B";
                }

                pawnObjArray[x, y].transform.SetParent(pieceHolder);
            }
        }


    }
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
                        if (pawnObjArray[x, y].transform.gameObject.tag == "WhitePawn")

                            pawnObjArray[x, y].GetComponent<MeshRenderer>().material.color = oldColour;
                    }
                }

                if (hit.transform.GetComponent<MeshRenderer>().material.color != yellowColour)
                {
                    oldPref = hit.transform.gameObject;
                    oldPref.GetComponent<MeshRenderer>().material.color = yellowColour;
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
                        if (Mathf.Abs(newPref.transform.position.x - oldPref.transform.position.x) == 10 && (newPref.transform.position.y - oldPref.transform.position.y) == 10)
                        {
                            MovePiece2((int)oldPref.transform.position.x / 10, (int)oldPref.transform.position.y / 10, (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");


                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = oldColour;
                            selectedPiece = false;
                            isPlayerTurn = false;
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
                        if ((newPref.transform.position.y - oldPref.transform.position.y) == 10 && Mathf.Abs(newPref.transform.position.x - oldPref.transform.position.x) == 0)
                        {
                            MovePiece2((int)oldPref.transform.position.x / 10, (int)oldPref.transform.position.y / 10, (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");
                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = oldColour;
                            selectedPiece = false;
                            isPlayerTurn = false;
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


public void MovePiece2(int x1, int y1, int x2, int y2, string letter)
    {
        //print(pawnObj[x1, y1] + "\n");
        //print(pawnObj[x1, y1].transform.position + "\n");
        //print(pawnObj[x2, y2] + "\n");
        //print(pawnObj[x2, y2].transform.position + "\n");

        //Swapping the gameobject inside the array
        GameObject temp = pawnObjArray[x1, y1];
        pawnObjArray[x1, y1] = pawnObjArray[x2, y2];
        pawnObjArray[x2, y2] = temp;


        //swapping the position of the array
        Vector3 tempPos = pawnObjArray[x1, y1].transform.position;
        pawnObjArray[x1, y1].transform.position = pawnObjArray[x2, y2].transform.position;
        pawnObjArray[x2, y2].transform.position = tempPos;

        // If black piece attacks white, remove it this is for AI
        //Should do the same for player

        if (pawnObjArray[x1, y1].name == "White Pawn(Clone)")
        {
            pawnObjArray[x1, y1].gameObject.SetActive(false);

        }
        else if (isPlayerTurn && pawnObjArray[x1, y1].name == "Black Pawn(Clone)")
        {
            pawnObjArray[x1, y1].gameObject.SetActive(false);
        }

        oldPawnPositionArray[x1, y1] = "E"; //save data
        oldPawnPositionArray[x2, y2] = letter;//save data

        UpdateBoardData();

    }


    public void UpdateBoardData()
    {

        collectPawnList.Clear();
        firstThreeLetters = "";
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
                collectPawnList.Add(oldPawnPositionArray[x, y]);
                if (y == 0)
                {
                    firstThreeLetters += oldPawnPositionArray[x, y];
                }
                else if (y == 1)
                {

                    midThreeLetters += oldPawnPositionArray[x, y];

                }
                else
                {

                    LastThreeLetters += oldPawnPositionArray[x, y];

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
                    firstRevThreeLetters += oldPawnPositionArray[x, y];
                }
                else if (y == 1)
                {

                    midRevThreeLetters += oldPawnPositionArray[x, y];
                }
                else
                {

                    LastRevThreeLetters += oldPawnPositionArray[x, y];
                }
            }
        }

        if (isPlayerTurn)
        {
            mirrorAllPawnPositionIntoLongString = string.Join("", firstRevThreeLetters, midRevThreeLetters, LastRevThreeLetters);
            //List<> revCompareBoardtoalgo = firstRevThreeLetters.Concat(firstRevThreeLetters).Concat(midRevThreeLetters)
            // concat each string into one string
            allPawnPositionsIntoLongString = string.Concat(collectPawnList);

            print("Algorithm         " + algList.IndexOf(allPawnPositionsIntoLongString));
            print("Algorithm mirrored    " + algList.IndexOf(mirrorAllPawnPositionIntoLongString));
        }

        int temp = algList.IndexOf(allPawnPositionsIntoLongString);
        int temp2 = algList.IndexOf(mirrorAllPawnPositionIntoLongString);

        if (temp == -1 || temp2 ==7 || temp2 ==18 || temp2 ==23)
        {
            indexPawnPosition = algList.IndexOf(mirrorAllPawnPositionIntoLongString);
            isRevSelectedTrue = true;
        }
        else
        {
            indexPawnPosition = algList.IndexOf(allPawnPositionsIntoLongString);
            isRevSelectedTrue = false;
        }








        print(string.Concat(allPawnPositionsIntoLongString));
        print("Turn: " + turnCount++);


        EndGame(false);
    }
    private int DetectNumberPiece()
    {

        int countNumberOfPiece = 0;
        int countNumberConfirmPieces = 0;
        foreach (string stuff in collectPawnList)
        {
            if (stuff == "W")
                countNumberOfPiece++;
        }

        if (countNumberConfirmPieces == countNumberOfPiece) // && if white pawn Cannot move
        {
            print("plyer cant move, bot wins");
        }

        return countNumberOfPiece;
    }
    public void EndGame(bool isBotNoMoves)
    {
        //EEWWEBBEE // AI WINS
        //WEEBEWEEB// AI WINS
        //EWEWBWBEB // PLAYER WINS
        //EWEWBEBEE // AI WINS
        //EEEEWEEBE   //PLAYER WINS
        //EEWEWBEBE //AI WINS
        //WEEBWEEBE //AI WINS
        if (!allPawnPositionsIntoLongString.Contains("W"))
        {

            print("AI wins No more W piece");

        }
        else if (!allPawnPositionsIntoLongString.Contains("B"))
        {
            print("Player wins No more B piece");

        }
        else if (firstThreeLetters.Contains("B"))
        {

            print("AI wins crossed the finish line");

        }
        else if (LastThreeLetters.Contains("W"))
        {
            print("Player wins crossed the finish line");

        }
        else if (isBotNoMoves)
        {
            print("Bot unable to move, player Wins!");

        }
        /*      else if ((algList.IndexOf(boardStringValue) != algList.IndexOf(revBoardStringValue)) && isPlayerTurn)
              {
                  print("Player unable to move, Bot Wins!");
                  return true;
              }*/

    }
}
