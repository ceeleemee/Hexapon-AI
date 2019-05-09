using UnityEngine;

public class BotManager : MonoBehaviour
{

    //allow to prefab and instantiate
    public static BotManager instance = null;

    private GameObject findGMGameObject;
    private GameManager GM;

    private float interval = 3f;

    private int r = 0;


    private readonly int BOT = 0;
    private readonly int MID = 1;
    private readonly int TOP = 2;
    private int LEFT = 0;
    private int RIGHT = 2;
    public bool noMoreBotMoves = false;
    private void Start()
    {
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();




    }

    private void Update()
    {
        if (!GM.playerTurn)
        {

            AITurn();

            //print("player wins");


        }
        if (GM.revSelectedTrue)
        {
            LEFT = 2;
            RIGHT = 0;
        }
        else
        {
            LEFT = 2;
            RIGHT = 0;
        }

    }

    private void AITurn()
    {

        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
        //**************************************************Turn   2***********************************
        else if (GM.SendIndexToAlgorithms() == 0)
        {
            {
                r = Random.Range(0, 3);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");
                }
                else if (r == 1)
                {
                    GM.MovePiece2(MID, TOP, MID, MID, "B");
                }
                else
                {
                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 1)
        {
            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, LEFT, MID, "B");
                }
                else
                {

                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");
                }
                Confirming();
            }
        }
        /* else if (GM.SendIndexToAlgorithms() == 2)   ///This is the extra not shown on the algorithm photo(bassically mirror of the first one)
         {
             {
                 r = Random.Range(0,3);
                 if (r == 0)
                 {
                     //old,new
                     GM.MovePiece2(MID,TOP,RIGHT,MID,"B");
                 }
                 else if (r == 1)
                 {
                     GM.MovePiece2(MID,TOP,MID,MID,"B");
                 }
                 else
                 {
                     GM.MovePiece2(LEFT,TOP,LEFT,MID,"B");
                 }
                 Confirming();

             }
         }*/
        //**************************************************Turn   4***********************************
        else if (GM.SendIndexToAlgorithms() == 2)
        {

            {
                r = Random.Range(0, 4);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else if (r == 2)
                {
                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");

                }
                else
                {

                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 3)
        {

            {
                r = Random.Range(0, 4);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");

                }
                else if (r == 2)
                {
                    GM.MovePiece2(MID, MID, RIGHT, BOT, "B");

                }
                else
                {

                    GM.MovePiece2(MID, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 4)
        {

            {
                r = Random.Range(0, 3);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 5)
        {

            {
                r = Random.Range(0, 3);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(MID, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 6)
        {

            {
                r = Random.Range(0, 3);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(MID, MID, LEFT, BOT, "B");

                }
                else
                {

                    GM.MovePiece2(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 7)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 8)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece2(LEFT, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 9)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }

                else
                {

                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 10)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 11)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 12)
        {

            {

                GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");

                Confirming();

            }
        }
        /////******************************Turn    6 *********************
        else if (GM.SendIndexToAlgorithms() == 13)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece2(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 14)
        {

            {

                //old,new
                GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 15)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");

                }

                else
                {

                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 16)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }

                else
                {

                    GM.MovePiece2(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 17)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");

                }

                else
                {

                    GM.MovePiece2(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 18)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, MID, RIGHT, BOT, "B");

                }

                else
                {

                    GM.MovePiece2(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 19)
        {

            {
                r = Random.Range(0, 3);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else if (r == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 20)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }
                else
                {

                    GM.MovePiece2(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 21)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");

                }
                else
                {

                    GM.MovePiece2(MID, MID, MID, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 22)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }
                else
                {

                    GM.MovePiece2(LEFT, MID, LEFT, BOT, "B");
                }
                Confirming();

            }
        }
        else if (GM.SendIndexToAlgorithms() == 23)
        {

            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    //old,new
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }

                else
                {

                    GM.MovePiece2(RIGHT, MID, RIGHT, BOT, "B");
                }
                Confirming();
            }
        }
        else
        {
            noMoreBotMoves = true;
        }


    }
    private void Confirming()
    {
        GM.playerTurn = true;
        interval = 3f;
    }
}
