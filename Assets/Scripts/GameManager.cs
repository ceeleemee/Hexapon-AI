﻿using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //allow to prefab and instantiate but doesnt allow to use them in script??? why must be cause he has clone name
    //Use find GameObject.FindGameObjectWithTag("tag"); and then use gameobject.GetComponent<GameManager>();
    public static GameManager instance = null;
    public GameObject tileGo;
    public GameObject blackPawnGo;
    public GameObject whitePawnGO;
    public GameObject emptyPawnGo;
    public GameObject startButtonGo;
    private readonly int MAX = 3;
    private Transform boardHolder;
    private Transform pieceHolder;
    private Transform emptyHolder;
    private List<string> collectPawnStringCodeList;
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
    private Color defaultColour = Color.white;
    private Color selectedColour = Color.yellow;
    private bool selectedPiece = false;
    private GameObject[,] pawnObjArray;
    private GameObject[,] boardObjArray;
    public bool isPlayerTurn = true;
    public bool isEndGame = false;
    private int turnCount = 1;
    private GameObject findBMGameObject;
    private BotManager BM;
    private GameObject findWPGameObject;
    private WhitePawn[] WP;
    private bool isEndTriggered = false;


    //Note alogithm 21 and 15 never happens because , algorithm 1 is always symmeterically about the middle coloumn.
    // AI always moves the left piece .
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
        collectPawnStringCodeList = new List<string>();

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
        WP = new WhitePawn[MAX];
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
    private void LateUpdate()
    {
        
        //if (isEndTriggered) // if win/lose pop up appears, stop this loop
        {
            EndGame();

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

                        Destroy(pawnObjArray[x, y]);
                        Destroy(boardObjArray[x, y]);
                    }

                turnCount = 1;
                isPlayerTurn = true;

                oldPref = null;
                newPref = null;

                BoardSetup();
                GeneratePieces();
                UpdateBoardData();
                PlayerCanPlay();
                isEndTriggered = false;
                BM.isAIcanMove = true;

            }

        }


    }


    // Update is called once per frame
    private void CreateEverything()
    {
        Instantiate(startButtonGo, new Vector3(30, 20, 1), Quaternion.identity);
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
                boardObjArray[x, y] = Instantiate(tileGo, new Vector3(x * 10, y * 10, 1), Quaternion.identity);
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
                    pawnObjArray[x, y] = GameObject.Instantiate(whitePawnGO, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));


                    oldPawnPositionArray[x, y] = "W";

                    WP[x] = pawnObjArray[x, y].GetComponent<WhitePawn>();

                }
                else if (y == 1)
                {
                    pawnObjArray[x, y] = GameObject.Instantiate(emptyPawnGo, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    oldPawnPositionArray[x, y] = "E";

                }
                else
                {
                    pawnObjArray[x, y] = GameObject.Instantiate(blackPawnGo, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));

                    oldPawnPositionArray[x, y] = "B";

                }

                collectPawnStringCodeList.Add(oldPawnPositionArray[x, y]);
                //so there is codevalue as soon as object is instantiated
                pawnObjArray[x, y].transform.SetParent(pieceHolder);
                allPawnPositionsIntoLongString = string.Concat(collectPawnStringCodeList);


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

                            pawnObjArray[x, y].GetComponent<MeshRenderer>().material.color = defaultColour;
                    }
                }

                if (hit.transform.GetComponent<MeshRenderer>().material.color != selectedColour)
                {
                    oldPref = hit.transform.gameObject;
                    oldPref.GetComponent<MeshRenderer>().material.color = selectedColour;
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


                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = defaultColour;
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
                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = defaultColour;
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

        if (pawnObjArray[x1, y1].transform.gameObject.tag == "WhitePawn")
        {
            pawnObjArray[x1, y1].gameObject.SetActive(false);

        }
        else if (isPlayerTurn && pawnObjArray[x1, y1].transform.gameObject.tag == "BlackPawn")
        {
            pawnObjArray[x1, y1].gameObject.SetActive(false);
        }


        oldPawnPositionArray[x1, y1] = "E"; //save data
        oldPawnPositionArray[x2, y2] = letter;//save data

        UpdateBoardData();
        print("Turn: " + turnCount++);

    }


    public void UpdateBoardData()
    {
        //refresh each turn
        collectPawnStringCodeList.Clear();
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
                collectPawnStringCodeList.Add(oldPawnPositionArray[x, y]);
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
            allPawnPositionsIntoLongString = string.Concat(collectPawnStringCodeList);

            print("Algorithm         " + algList.IndexOf(allPawnPositionsIntoLongString));
            print("Algorithm mirrored    " + algList.IndexOf(mirrorAllPawnPositionIntoLongString));
        }
        //check for mirror algorithms and return a index value
        int temp = algList.IndexOf(allPawnPositionsIntoLongString);
        int temp2 = algList.IndexOf(mirrorAllPawnPositionIntoLongString);

        if (temp == -1 || temp2 == 7 || temp2 == 18 || temp2 == 23)
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


    }
    private bool PlayerCanPlay()
    {

        int countNumberOfPiece = 0;
        int countNumberConfirmPieces1 = 0;
        int countNumberConfirmPieces2 = 0;
        int countNumberConfirmPieces3 = 0;
        int countNumberConfirmPiecesSum = 0;
        bool canPlay=true;
        foreach (string stuff in collectPawnStringCodeList)
        {
            if (stuff == "W")
                countNumberOfPiece++;

        }
        if (!WP[0].WPCanMakeMove())
        {
            countNumberConfirmPieces1 = 1;
        }
        else
        {
            countNumberConfirmPieces1 = 0;
        }
        if (!WP[1].WPCanMakeMove())
        {
            countNumberConfirmPieces2 = 1;
        }
        else
        {
            countNumberConfirmPieces2 = 0;
        }
        if (!WP[2].WPCanMakeMove())
        {
            countNumberConfirmPieces3 = 1;
        }
        else
        {
            countNumberConfirmPieces3 = 0;
        }
        //print("" + WP[0].WPCanMakeMove() + "" + WP[1].WPCanMakeMove() + "" + WP[2].WPCanMakeMove()+"");
        countNumberConfirmPiecesSum = countNumberConfirmPieces1 + countNumberConfirmPieces2 + countNumberConfirmPieces3;

        if (countNumberOfPiece == countNumberConfirmPiecesSum) // && if white pawn Cannot move
        {
         //   print("plyer cant move, bot wins123");
            canPlay = false;
        }
        else
        {
            canPlay =  true;
        }
        //print("white pieces " + countNumberOfPiece + ", Can't move white pieces " + countNumberConfirmPiecesSum);
        return canPlay;

    }
    public void EndGame()
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
        else if (!BM.isAIcanMove)
        {
            print("Bot unable to move, player Wins!");


        }

        if (!PlayerCanPlay() && isPlayerTurn)
        {

            print("plyer cant move, bot wins");

        }
        /*      else if ((algList.IndexOf(boardStringValue) != algList.IndexOf(revBoardStringValue)) && isPlayerTurn)
              {
                  print("Player unable to move, Bot Wins!");
                  return true;
              }*/

    }
}
