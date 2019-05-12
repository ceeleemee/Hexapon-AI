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
               random = Random.Range(0, 3);
                //random = 1;
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");
                }
                else if (random == 1)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 4);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else if (random == 2)
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
                random = Random.Range(0, 4);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }
                else if (random == 1)
                {
                    GM.MovePiece2(RIGHT, TOP, RIGHT, MID, "B");

                }
                else if (random == 2)
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
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(LEFT, TOP, MID, MID, "B");

                }
                else if (random == 1)
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
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, LEFT, MID, "B");

                }
                else if (random == 1)
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
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(MID, TOP, RIGHT, MID, "B");

                }
                else if (random == 1)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    //old,new
                    GM.MovePiece2(RIGHT, TOP, MID, MID, "B");

                }
                else if (random == 1)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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
                random = Random.Range(0, 2);
                if (random == 0)
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

            isAIcanMove = false;
        }


    }
    private void Confirming()
    {
        GM.isPlayerTurn = true;
        interval = 3f;
    }
}
