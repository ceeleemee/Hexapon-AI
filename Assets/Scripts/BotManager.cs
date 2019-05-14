using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    //allow to prefab and instantiate
    public static BotManager instance = null;

    private GameObject findGMGameObject;
    private GameManager GM;

    private float interval = 3f;

    private int random = 0;


    private readonly int BOT = 0;
    private readonly int MID = 1;
    private readonly int TOP = 2;
    private int LEFT = 0;
    private int RIGHT = 2;
    public bool isAIcanMove = true;

    private List<int> algList0 = new List<int>() { 0, 1, 2 };
    private List<int> algList1 = new List<int>() { 0, 1, };
    private List<int> algList2 = new List<int>() { 0, 1, 2, 3 };
    private List<int> algList3 = new List<int>() { 0, 1, 2, 3 };
    private List<int> algList4 = new List<int>() { 0, 1, 2, };
    private List<int> algList5 = new List<int>() { 0, 1, 2, };
    private List<int> algList6 = new List<int>() { 0, 1, 2, };
    private List<int> algList7 = new List<int>() { 0, 1, };
    private List<int> algList8 = new List<int>() { 0, 1, };
    private List<int> algList9 = new List<int>() { 0, 1, };
    private List<int> algList10 = new List<int>() { 0, 1, };
    private List<int> algList11 = new List<int>() { 0, 1, };
    private List<int> algList12 = new List<int>() { 0, };
    private List<int> algList13 = new List<int>() { 0, 1, };
    private List<int> algList14 = new List<int>() { 0, };
    private List<int> algList15 = new List<int>() { 0, 1 };
    private List<int> algList16 = new List<int>() { 0, 1, };
    private List<int> algList17 = new List<int>() { 0, 1, };
    private List<int> algList18 = new List<int>() { 0, 1, };
    private List<int> algList19 = new List<int>() { 0, 1, 2, };
    private List<int> algList20 = new List<int>() { 0, 1, };
    private List<int> algList21 = new List<int>() { 0, 1, };
    private List<int> algList22 = new List<int>() { 0, 1, };
    private List<int> algList23 = new List<int>() { 0, 1, };

    private void Start()
    {
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();


        print(algList0.Count);

    }

    private void Update()
    {
        if (!GM.isPlayerTurn)
        {

            AITurn();

            //print("player wins");


        }
        if (GM.isRevStringSelected)
        {
            LEFT = 2;
            RIGHT = 0;

        }
        else
        {
            LEFT = 0;
            RIGHT = 2;

        }

    }

    private void AITurn()
    {

        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
        //**************************************************Turn   2***********************************
        else if (GM.indexPawnPosition == 0)
        {
            {
                int MAX = algList0.Count;   
                random = Random.Range(0, MAX);
                //random = 1;
                if (algList0[random] == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, LEFT, MID, "B");
                }
                else if (algList0[random] == 1)
                {
                    GM.MovePiece(MID, TOP, MID, MID, "B");
                }
                else if(algList0[random] ==2)
                {
                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 1)
        {
            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, TOP, LEFT, MID, "B");
                }
                else
                {

                    GM.MovePiece(LEFT, TOP, MID, MID, "B");
                }
                Confirming();
            }
        }

        //**************************************************Turn   4***********************************
        else if (GM.indexPawnPosition == 2)
        {

            {
                random = Random.Range(0, 4);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, TOP, MID, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }
                else if (random == 2)
                {
                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");

                }
                else
                {

                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 3)
        {

            {
                random = Random.Range(0, 4);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, LEFT, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");

                }
                else if (random == 2)
                {
                    GM.MovePiece(MID, MID, RIGHT, BOT, "B");

                }
                else
                {

                    GM.MovePiece(MID, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 4)
        {

            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, TOP, MID, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 5)
        {

            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, LEFT, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(MID, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 6)
        {

            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, RIGHT, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(MID, MID, LEFT, BOT, "B");

                }
                else
                {

                    GM.MovePiece(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 7)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 8)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece(LEFT, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 9)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, TOP, MID, MID, "B");

                }

                else
                {

                    GM.MovePiece(MID, TOP, LEFT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 10)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 11)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 12)
        {

            {

                GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");

                Confirming();

            }
        }
        /////******************************Turn    6 *********************
        else if (GM.indexPawnPosition == 13)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 14)
        {

            {

                //old,new
                GM.MovePiece(LEFT, TOP, MID, MID, "B");

                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 15)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, RIGHT, MID, "B");

                }

                else
                {

                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 16)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, LEFT, MID, "B");

                }

                else
                {

                    GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 17)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 18)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, MID, RIGHT, BOT, "B");

                }

                else
                {

                    GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 19)
        {

            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 20)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, LEFT, MID, "B");

                }
                else
                {

                    GM.MovePiece(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 21)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(MID, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 22)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(LEFT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 23)
        {

            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece(RIGHT, TOP, MID, MID, "B");

                }

                else
                {

                    GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();
            }
        }
        else
        {
            isAIcanMove = false;
        }


    }
    private void Confirming()
    {
        GM.isPlayerTurn = true;
        interval = 3f;
    }
}
