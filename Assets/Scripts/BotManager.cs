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
        if (!GM.isPlayerTurn)
        {

            AITurn();

            //print("player wins");


        }
        if (GM.isRevSelectedTrue)
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
        else if (GM.indexPawnPosition == 1)
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

        //**************************************************Turn   4***********************************
        else if (GM.indexPawnPosition == 2)
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
        else if (GM.indexPawnPosition == 3)
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
        else if (GM.indexPawnPosition == 4)
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
        else if (GM.indexPawnPosition == 5)
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
        else if (GM.indexPawnPosition == 6)
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
        else if (GM.indexPawnPosition == 7)
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
        else if (GM.indexPawnPosition == 8)
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
        else if (GM.indexPawnPosition == 9)
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
        else if (GM.indexPawnPosition == 10)
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
        else if (GM.indexPawnPosition == 11)
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
        else if (GM.indexPawnPosition == 12)
        {

            {

                GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");

                Confirming();

            }
        }
        /////******************************Turn    6 *********************
        else if (GM.indexPawnPosition == 13)
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
        else if (GM.indexPawnPosition == 14)
        {

            {

                //old,new
                GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                Confirming();

            }
        }
        else if (GM.indexPawnPosition == 15)
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
        else if (GM.indexPawnPosition == 16)
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
        else if (GM.indexPawnPosition == 17)
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
        else if (GM.indexPawnPosition == 18)
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
        else if (GM.indexPawnPosition == 19)
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
        else if (GM.indexPawnPosition == 20)
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
        else if (GM.indexPawnPosition == 21)
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
        else if (GM.indexPawnPosition == 22)
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
        else if (GM.indexPawnPosition == 23)
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
          
           GM.EndGame(true);
        }


    }
    private void Confirming()
    {
        GM.isPlayerTurn = true;
        interval = 3f;
    }
}
