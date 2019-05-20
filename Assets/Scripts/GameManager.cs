using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //allow to prefab and instantiate but doesnt allow to use them in script??? why must be cause he has clone name
    //Use find GameObject.FindGameObjectWithTag("tag"); and then use gameobject.GetComponent<GameManager>();
    public static GameManager instance = null;
    public GameObject tileGo;
    public GameObject tileGo1;
    public GameObject blackPawnGo;
    public GameObject whitePawnGO;
    public GameObject emptyPawnGo;
    public GameObject startButtonGo;

    private GameObject oldPref;
    private GameObject newPref;
    private GameObject[,] pawnObjArray;
    private GameObject[,] boardObjArray;
    private readonly int MAX = 3;
    private Transform boardHolder;
    private Transform pieceHolder;
    private List<string> initialPawnStringPosition;
    public string pawnPositionsIntoLongString;
    public int indexPawnPosition = 0;
    public string[] threeLetters = { "", "", "" }; // bottom, mid and top layer
    public string[] revThreeLetters = { "", "", "" };// bottom, mid and top layer( but mirror about the y axis)
    public string mirrorAllPawnPositionIntoLongString;
    private string[,] pawnPositionArray;
    private Color defaultColour = Color.white;
    private Color selectedColour = Color.yellow;
    public bool isPlayerTurn = true;
    public bool isEndGame = false;
    private int turnCount = 1;
    private GameObject findBMGameObject;
    private BotManager BM;
    private GameObject findEGMGameObject;
    private EndGameManager EGM;
    private WhitePawn[] WP;
    public bool isRevStringSelected = false;
    private Vector3 target;
    private LayerMask MaskStartPiece;
    private LayerMask MaskWhitePiece;
    private LayerMask MaskBlackPiece;
    private LayerMask MaskTilePiece;
    private readonly float rayLength = 100f;

    public bool isCanPlay = true;
    public bool isEnableResetButton = false;
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


    // Start is called before the first frame update
    void Start()
    {
        MaskStartPiece = LayerMask.GetMask("StartNewGame");
        MaskWhitePiece = LayerMask.GetMask("WhitePiece");
        MaskBlackPiece = LayerMask.GetMask("BlackPiece");
        MaskTilePiece = LayerMask.GetMask("Tile");
        oldPref = GetComponent<GameObject>();// rbPref = Rigidbody; is it different?
        newPref = GetComponent<GameObject>();
        initialPawnStringPosition = new List<string>();
        pawnPositionArray = new string[MAX, MAX];
        pawnObjArray = new GameObject[MAX, MAX];
        pawnPositionsIntoLongString = "";
        mirrorAllPawnPositionIntoLongString = "";
        boardObjArray = new GameObject[MAX, MAX];
        boardHolder = new GameObject("Board").transform;
        pieceHolder = new GameObject("Piece").transform;
        findBMGameObject = GameObject.FindGameObjectWithTag("BM");
        BM = findBMGameObject.GetComponent<BotManager>();
        findEGMGameObject = GameObject.FindGameObjectWithTag("EGM");
        EGM = findEGMGameObject.GetComponent<EndGameManager>();
        WP = new WhitePawn[MAX];



        CreateEverything();

    }
    void Update()
    {

        SelectPieceAndSaveData();
        ConfirmPlayerCanMove(WPPieceTrigging(0), WPPieceTrigging(1), WPPieceTrigging(2));

    }

    public void RestartGame()
    {
        for (int y = 0; y < MAX; y++)
            for (int x = 0; x < MAX; x++)
            {

                Destroy(pawnObjArray[x, y]);
                Destroy(boardObjArray[x, y]);
            }
        ClearStringList();
        BoardSetup();
        GeneratePieces(whitePawnGO, 0, "W");
        GeneratePieces(emptyPawnGo, 1, "E");
        GeneratePieces(blackPawnGo, 2, "B");
        BM.Confirming();//  isPlayerTurn = true;
        turnCount = 1;
        oldPref = null;
        newPref = null;
        EGM.isEndGameTriggered = false;
        EGM.isAILost = false;
        BM.isAIMoveRemoved = false;
        BM.isAICanMove = true;
        isRevStringSelected = false;
        isCanPlay = true;
        BM.interval = 3f;
        EGM.gameOverIndex = 0;
    }


    // Update is called once per frame
    private void CreateEverything()
    {
        if(isEnableResetButton)
            Instantiate(startButtonGo, new Vector3(0, 30, 1), Quaternion.identity);
        BoardSetup();
        GeneratePieces(whitePawnGO, 0, "W");
        GeneratePieces(emptyPawnGo, 1, "E");
        GeneratePieces(blackPawnGo, 2, "B");
    }

    private void BoardSetup()
    {
        int count = 1;
        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                //instance tile
                boardObjArray[x, y] = Instantiate(count % 2 == 0 ? tileGo : tileGo1, new Vector3(x * 10, y * 10, 1), Quaternion.identity);
                //boardObjArray[x, y].transform.localScale = new Vector3(,0,0);

                boardObjArray[x, y].transform.SetParent(boardHolder);
                //Debug.Log(instance.transform.position);
                count++;
            }
        }
    }


    private void GeneratePieces(GameObject go, int yPosition, string initialLetter)
    {
        //bool isPieceWhite = (y > 1) ? false : true;
        //GameObject instance = Instantiate(isPieceWhite ? whitePawn : blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
        for (int x = 0; x < MAX; x++)
        {

            pawnObjArray[x, yPosition] = GameObject.Instantiate(go, new Vector3(x * 10, yPosition * 10, 0), Quaternion.Euler(90, 0, 0));
            pawnPositionArray[x, yPosition] = initialLetter;
            if (go == whitePawnGO)
                WP[x] = pawnObjArray[x, yPosition].GetComponent<WhitePawn>();

            initialPawnStringPosition.Add(pawnPositionArray[x, yPosition]);
            //so there is codevalue as soon as object is instantiated
            pawnObjArray[x, yPosition].transform.SetParent(pieceHolder);
        }
        pawnPositionsIntoLongString = string.Concat(initialPawnStringPosition);//currently is looping 3 times because there is 3 different pieces.
    }
    private void SelectPieceAndSaveData()//selected white piece becomes yellow and deselected piece becomes white
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//mouse shoots an array       
        RaycastHit hit;

        //this clicks on mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlayerTurn)
            {
                if (Physics.Raycast(ray, out hit, rayLength, MaskWhitePiece))
                {
                    //refresh colour for whites but not the selected piece.
                    //
                    for (int y = 0; y < MAX; y++)
                    {
                        for (int x = 0; x < MAX; x++)
                        {
                            //constantly checking each pieces if it is a tagged whtiepawn
                            if (pawnObjArray[x, y].transform.gameObject.tag == "WhitePawn")
                                pawnObjArray[x, y].GetComponent<MeshRenderer>().material.color = defaultColour;

                        }
                    }

                    if (hit.transform.GetComponent<MeshRenderer>().material.color != selectedColour)
                    {
                        oldPref = hit.transform.gameObject;
                        oldPref.GetComponent<MeshRenderer>().material.color = selectedColour;

                    }
                }
                if (Physics.Raycast(ray, out hit, rayLength, MaskBlackPiece))
                {
                    //When white piece is yellow consume blackpiece
                    if ((newPref = hit.transform.gameObject) && newPref != null && oldPref != null)
                    {
                        //Mathf.Abs(newPref.position.x - curPref.transform.position.x) <= 10 && (newPref.position.y - curPref.transform.position.y)<=10||(newPref.position.y != curPref.transform.position.y) &&(newPref.position.x != curPref.transform.position.x)
                        if (Mathf.Abs(newPref.transform.position.x - oldPref.transform.position.x) == 10 
                            && (newPref.transform.position.y - oldPref.transform.position.y) == 10)
                        {
                            MovePiece((int)oldPref.transform.position.x / 10, (int)oldPref.transform.position.y / 10,
                                (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");
                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = defaultColour;
                            isPlayerTurn = false;
                            //print("player turn\n");
                        }
                        else
                        {
                            newPref.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }

                }
                else if (Physics.Raycast(ray, out hit, rayLength, MaskTilePiece))
                {
                    if ((newPref = hit.transform.gameObject) && newPref !=null && oldPref != null)
                    {
                        //control distance between selected pref and other objects
                        if ((newPref.transform.position.y - oldPref.transform.position.y) == 10 
                            && Mathf.Abs(newPref.transform.position.x - oldPref.transform.position.x) == 0)
                        {
                            MovePiece((int)oldPref.transform.position.x / 10, (int)oldPref.transform.position.y / 10,
                                (int)newPref.transform.position.x / 10, (int)newPref.transform.position.y / 10, "W");
                            oldPref.gameObject.GetComponent<MeshRenderer>().material.color = defaultColour;
                            isPlayerTurn = false;
                            //print("player turn\n");
                        }
                        else
                        {
                            newPref.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }
                }
            }
            if (Physics.Raycast(ray, out hit, rayLength, MaskStartPiece) )
            {
                RestartGame();
            }

        }
        //This follows mouse around
        { /* if (selectedPiece)
         {
             if (Physics.Raycast(ray, out hit))
             {
                 if (rb = hit.transform.GetComponent<Rigidbody>())
                     rb.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
             }
         }

        
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
    }


    public void MovePiece(int x1, int y1, int x2, int y2, string letter)
    {
        //print("Turn: " + (turnCount++ ) + "\n");
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

        //  if (pawnObjArray[x1, y1].transform.gameObject.tag == "WhitePawn") if(isPlayerTurn && pawnObjArray[x1, y1].transform.gameObject.tag == "BlackPawn"

        pawnObjArray[x1, y1].gameObject.SetActive(false);

        pawnPositionArray[x1, y1] = "E"; //save data
        pawnPositionArray[x2, y2] = letter;//save data
        UpdateBoardData();
    }

    private void ClearStringList()
    {
        initialPawnStringPosition.Clear();
        threeLetters = new string[] { "", "", "" };
        revThreeLetters = new string[] { "", "", "" };

    }


    private void UpdateBoardData()
    {
        ClearStringList();
        //refresh each turn

        //This whole step is required or it will print System.string[,]
        //Collect data from board and place it into a single string

        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                initialPawnStringPosition.Add(pawnPositionArray[x, y]);
                threeLetters[y] += pawnPositionArray[x, y];
            }
        }

        //reverse string preparation for mirror algorithm check
        //reverse in groups of three
        for (int y = MAX - 1; y >= 0; y--)
        {
            for (int x = MAX - 1; x >= 0; x--)
            {
                revThreeLetters[y] += pawnPositionArray[x, y];
            }
        }
        mirrorAllPawnPositionIntoLongString = string.Join("", revThreeLetters[0], revThreeLetters[1], revThreeLetters[2]);
        // concat each string into one string
        pawnPositionsIntoLongString = string.Concat(initialPawnStringPosition);
        print(pawnPositionsIntoLongString);
        // print("Algorithm         " + algList.IndexOf(allPawnPositionsIntoLongString));
        //print("Algorithm mirrored    " + algList.IndexOf(mirrorAllPawnPositionIntoLongString));
        //check for mirror algorithms and return a index value
        int temp = algList.IndexOf(pawnPositionsIntoLongString);
        int temp2 = algList.IndexOf(mirrorAllPawnPositionIntoLongString);

        if (temp == -1 || temp2 == 7 || temp2 == 18 || temp2 == 23 || temp2 == 15 || temp2 == 21)
        {
            indexPawnPosition = algList.IndexOf(mirrorAllPawnPositionIntoLongString);
            isRevStringSelected = true;
        }
        else if (temp == 1)
        {
            int random = Random.Range(0, 2);
            print(random + "random");
            if (random == 0)
                isRevStringSelected = true;
            else
                isRevStringSelected = false;
            indexPawnPosition = algList.IndexOf(pawnPositionsIntoLongString);
        }
        else
        {
            indexPawnPosition = algList.IndexOf(pawnPositionsIntoLongString);
            isRevStringSelected = false;
        }

        print("************");
        print("The first check algorithm " + indexPawnPosition + " and The whole whole string " + pawnPositionsIntoLongString + "\n");



    }
    // This method words with whitepawn ray casting
    private int WPPieceTrigging(int index)
    {


        if (!WP[index].WPCanMakeMove())
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }
    public void ConfirmPlayerCanMove(int aCount, int bCount, int cCount)
    {
        int countNumberOfPiece = 0;
        int sum = aCount + cCount + bCount;

        foreach (string stuff in initialPawnStringPosition)
        {
            if (stuff == "W")
                countNumberOfPiece++;
        }
        if (countNumberOfPiece == sum) // check if the amount of whitepiece on the board is equal to the amount trigged
        {
            //   print("plyer cant move, bot wins123");
            isCanPlay = false;
        }
        else
        {
            isCanPlay = true;
        }
    }
}
