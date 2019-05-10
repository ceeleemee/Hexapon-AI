﻿using UnityEngine;

public class WhitePawn : MonoBehaviour
{
 
    private Vector3 rightDiagonal;
    private Vector3 leftDiagonal;
    private Vector3 upDirection;
    private Vector3 downDirection;
    private readonly float RAYLENGTH = 13f;
    private readonly int IGNORERAY = 12;
    private readonly int BLACKPIECE = 10;
    private readonly float interval = 1f;
    private readonly int storeTempLayer;

    private GameObject findGMGameObject;
    private GameManager GM;
    private readonly float interval3 = 3f;
    private Color white = Color.white;
    private Color red = Color.red;
    private Color blue = Color.blue;
    int pawnIndex = 0;
    public bool pawn0 = false;
    public bool pawn1 = false;
    public bool pawn2 = false;
    private void Start()
    {
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();

        rightDiagonal = new Vector3(Mathf.Sin(45), Mathf.Sin(45), 0);
        leftDiagonal = new Vector3(-Mathf.Sin(45), Mathf.Sin(45), 0);
        upDirection = Vector3.up;
        string firstLetter = gameObject.name[0].ToString();
        pawnIndex = int.Parse(firstLetter);
        print(pawnIndex);
    }
    private void Update()
    {
        DetectKillPieceDiagonalForWhite(leftDiagonal);
        DetectPieceUpForWhite(upDirection);
        DetectKillPieceDiagonalForWhite(rightDiagonal);

    }

    private bool DetectPieceUpForWhite(Vector3 rayDirection)
    {
        RaycastHit hit;
        bool isRayHitUp = false;

        Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, white);
        Ray rayUp = new Ray(transform.position, rayDirection);
        if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
        {

            if ((hit.collider.tag == "BlackPawn"))
            {

                Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, red);

                isRayHitUp = true;
            }
            else
            {
                isRayHitUp = false;
            }


        }

        return isRayHitUp;


    }
    private bool DetectKillPieceDiagonalForWhite(Vector3 rayDirection)
    {
        RaycastHit hit;
        bool isDiagnoal = false;


        Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, white);
        Ray rayUp = new Ray(transform.position, rayDirection);
        if (Physics.Raycast(rayUp, out hit, RAYLENGTH))
        {
            if ((hit.collider.tag == "BlackPawn"))
            {
                Debug.DrawRay(transform.position, rayDirection * RAYLENGTH, blue);
                isDiagnoal = true;

            }
            else
            {
                isDiagnoal = false;
            }

        }



        return isDiagnoal;
    }
}
