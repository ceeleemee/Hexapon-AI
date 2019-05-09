using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWithOutArray : MonoBehaviour
{

    public GameObject tile;

    public GameObject blackPawn;

    public GameObject whitePawn;
    public GameObject emptyPawn;
    private int MAX = 3;
    private Transform boardHolder;
    private Transform pieceHolder;
    private Transform emptyHolder;
    private GameObject[,] pawnObj;
    private GameObject curPref;
    private Rigidbody newPref;
    private Transform curPos;
    private Color curColour  = Color.white;
    private Color yellowColour = Color.yellow;
    private bool selectedPiece = false;

    public bool playerTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        curPref = GetComponent<GameObject>();// rbPref = Rigidbody; is it different?
        curPos = GetComponent<Transform>(); // curPos = transform; is it different?
        pawnObj = new GameObject[MAX,MAX];
         boardHolder = new GameObject("Board").transform;
        pieceHolder = new GameObject("Piece").transform;

        BoardSetup();
        GeneratePieces();
        //print(pawnObj[0, 0]);
        //print(pawnObj[0, 0].transform.position);

    }

    // Update is called once per frame
    void Update()
    {

       // if (playerTurn)
        {


            SelectPieceAndTryMove();

        }
        
        

    }
    void LateUpdate()
    {

    }

    private void BoardSetup()
    {
        
        for (int y = 0; y < MAX; y++)
        {
            for (int x= 0; x < MAX; x++)
            {
                //instance tile
                GameObject instance = Instantiate(tile, new Vector3(x*10,y*10,1),Quaternion.identity);
                instance.transform.SetParent(boardHolder);
                //Debug.Log(instance.transform.position);
            }
        }

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
                if (y ==0)
                {

                     instance = Instantiate(whitePawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
 
                }else if (y ==1)
                {
                    //instance = Instantiate(emptyPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                    instance = new GameObject();
                }
                else
                {
                    instance = Instantiate(blackPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                }
                    pawnObj[x, y] = instance;
                    instance.transform.SetParent(pieceHolder);


                //instance = Instantiate(emptyPawn, new Vector3(x * 10, y * 10, 0), Quaternion.Euler(90, 0, 0));
                //instance.transform.SetParent(emptyHolder);
            }
        }

    }
    private Vector3 target;
    private void SelectPieceAndTryMove()
    {
          
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//mouse shoots an array
        float rayLength = 100f;
        RaycastHit hit;
        LayerMask MaskWhitePiece = LayerMask.GetMask("WhitePiece");
        LayerMask MaskBlackPiece = LayerMask.GetMask("BlackPiece");
        LayerMask MaskTile = LayerMask.GetMask("Tile");
        
        //this clicks on mouse
        if (Input.GetMouseButtonDown(0))
        {
           // if (Physics.Raycast(ray, out hit, rayLength)){
           //     print(hit.transform.position);
          //  }
            //Select whitepiece and highlight it with yellow
            if ( Physics.Raycast(ray, out hit, rayLength, MaskWhitePiece))
            {
                for (int x = 0; x < MAX; x++)
                {
                    //pawnObj[x, 0].GetComponent<MeshRenderer>().material.color = curColour;
                }

                if (hit.transform.GetComponent<MeshRenderer>().material.color != yellowColour)
                {
                    curPref = hit.transform.gameObject;
                    curPref.GetComponent<MeshRenderer>().material.color = yellowColour;
                    selectedPiece = true;
                    GetPawnPositions();
                }
                else
                {
                    hit.transform.GetComponent<MeshRenderer>().material.color = curColour;
                    //rbPref = hit.transform.gameObject;
                    selectedPiece = false;
                }
            }           

            if (selectedPiece)
            {
                if (Physics.Raycast(ray, out hit, rayLength, MaskBlackPiece))
                {
                    //When white piece is yellow consume blackpiece
                    if ((newPref = hit.transform.GetComponent<Rigidbody>()))
                    {
                        //Mathf.Abs(newPref.position.x - curPref.transform.position.x) <= 10 && (newPref.position.y - curPref.transform.position.y)<=10||(newPref.position.y != curPref.transform.position.y) &&(newPref.position.x != curPref.transform.position.x)
                        if (Mathf.Abs(newPref.position.x - curPref.transform.position.x) == 10 && (newPref.position.y - curPref.transform.position.y) == 10)
                        {

                            curPref.transform.position = new Vector3(newPref.transform.position.x, newPref.transform.position.y, 0);
                            //pawnObj[(int)curPref.transform.position.x, (int)curPref.transform.position.y] = curPref.transform.gameObject;
                            curPref.gameObject.GetComponent<MeshRenderer>().material.color = curColour;
                            // replace piece when white eats it
                            newPref.gameObject.SetActive(false);
                            selectedPiece = false;
                            playerTurn = false;
                            GetPawnPositions();
                        }
                        else { 
                            newPref.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }

                }
                else if (Physics.Raycast(ray, out hit, rayLength, MaskTile))
                {
                    if ((newPref = hit.transform.GetComponent<Rigidbody>()))
                    {
                        //control distance between selected pref and other objects
                        if ((newPref.position.y - curPref.transform.position.y) == 10 && Mathf.Abs(newPref.position.x - curPref.transform.position.x) == 0)
                        {
                            //print(curPref.transform.position);
                            

                            curPref.transform.position = new Vector3(newPref.position.x, newPref.position.y, 0);
                            //pawnObj[(int)curPref.transform.position.x, (int)curPref.transform.position.y] = curPref.transform.gameObject;
                            curPref.gameObject.GetComponent<MeshRenderer>().material.color = curColour;
                            selectedPiece = false;
                            playerTurn = false;
                            GetPawnPositions();
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
    int count = 0;
    void GetPawnPositions()
    {
                print("******Move: "+count+"*******");
                count++;
        
        for (int y = 0; y < MAX; y++)
        {
            for (int x = 0; x < MAX; x++)
            {
                print(pawnObj[x, y]);
                print(pawnObj[x, y].transform.position);
                
            }
        }
    }

}
