using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPawn : MonoBehaviour
{
    private Vector3 rightDiagonal;
    private Vector3 leftDiagonal;
    private Vector3 upDirection;
    private Vector3 downDirection;
    private readonly float RAYLENGTH = 13f;
    private readonly int IGNORERAY = 12;
    private readonly int BLACKPIECE = 10;
    private float interval = 1f;
    private int storeTempLayer;

    private GameObject findGMGameObject;
    private GameManager GM;
    private float interval3 = 3f;
    private void Start()
    {
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();

        rightDiagonal = new Vector3(Mathf.Sin(-45), Mathf.Sin(-45), 0);
        leftDiagonal = new Vector3(-Mathf.Sin(-45), Mathf.Sin(-45), 0);
        downDirection = Vector3.down;
    }
    private void Update()
    {
        //DetectPieceUp(upDirection);
        //DetectKillPieceDiagonal(rightDiagonal);
        //DetectKillPieceDiagonal(leftDiagonal);


       //     DetectPieceDown(downDirection);
         //   DetectKillPieceDiagonal(rightDiagonal);
        //    DetectKillPieceDiagonal(leftDiagonal);



        if (gameObject.tag == "BlackPawn")
        {

            if (gameObject.GetComponent<MeshRenderer>().material.color == Color.red && interval > 0)
            {

                interval -= Time.deltaTime;

            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                interval = 3f;
            }
        }


    }


    /// <summary>
    /// Methods below currently does nothing
    /// </summary>




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WhitePawn"))
        {
         //   other.gameObject.SetActive(false);
        }
    }




    //use rays to detect for black pieces
    private void DetectPieceDown(Vector3 rayDirection)
    {
        RaycastHit hit;

            Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.white);
            Ray rayUp = new Ray(transform.position, rayDirection);
            if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
            {

                Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.red);                    




            }


        
    }

    private void DetectKillPieceDiagonal(Vector3 rayDirection)
    {
        RaycastHit hit;

            Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.white);
            Ray rayUp = new Ray(transform.position, rayDirection);
            if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
            {
            if (!GM.playerTurn)
            {
                if (interval3 > 0)
                {
                    interval3 -= Time.deltaTime;
                }
                else
                {


                    if ((hit.collider.tag == "WhitePawn") )
                    {
                            Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.green);
                            gameObject.transform.position = hit.transform.position;
                        GM.playerTurn = true;
                        interval3 =3f;
                    }
                }
            }

        }

    }






    //use rays to detect for white pieces

    private void DetectPieceUpForWhite(Vector3 rayDirection)
    {
        RaycastHit hit;       
        
        if (gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.white);
            Ray rayUp = new Ray(transform.position, rayDirection);
            if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
            {
                
                if ((hit.collider.tag == "BlackPawn") )
                {
                    
                    Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.red);
                    hit.collider.gameObject.layer = IGNORERAY;///////////make sure to reset properties when it is AI turn
                    
                }


            }
            

        }

    }
    private bool DetectKillPieceDiagonalForWhite(Vector3 rayDirection)
    {
        RaycastHit hit;
        bool isDiagnoal = false;

        if (gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.white);
            Ray rayUp = new Ray(transform.position, rayDirection);
            if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
            {
                if ((hit.collider.tag == "BlackPawn"))
                {
                    Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, Color.green);
                    isDiagnoal = true;
                    hit.collider.gameObject.layer = BLACKPIECE;
                }
               
            }


        }
        return isDiagnoal;
    }

}