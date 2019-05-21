using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    //allow to prefab and instantiate
    public static BotManager instance = null;

    private GameObject findGMGameObject;
    private GameManager GM;
    private GameObject findEGMGameObject;
    private EndGameManager EGM;
    public float interval = 3f;

    private int randMoveIndexAlglist = 0;


    private readonly int BOT = 0;
    private readonly int MID = 1;
    private readonly int TOP = 2;
    private int LEFT = 0;
    private int RIGHT = 2;
    public bool isAICanMove = true;
    public bool isAIMoveRemoved = false;
    /*private List<int> algList0 = new List<int>() { 0, 1, 2 };
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
    private List<string> algList = new List<string>()
    {
        //not the comma has to be together but it isnt when concat happens because i didnt code it to be like htat
        "0,1,2" , "0,1," , "0,1,2,3" , "0,1,2,3",
        "0,1,2," , "0,1,2," , "0,1,2," , "0,1,",
        "0,1," , "0,1," , "0,1," , "0,1,",
        "0," , "0,1," , "0," , "0,1",
        "0,1," , "0,1," , "0,1," , "0,1,2,",
        "0,1," , "0,1," , "0,1," , "0,1,",
    };*/
    private string[] algArray = new string[] {
        "012","01","0123","0123",
        "012","012","012","01",
        "01","01","01","01",
        "0","01","0","01",
        "01","01","01","012",
        "01","01","01","01"
    };
    List<string> algList;
    //int indexAlgArray;
    int randMove = 0;
    int savedAlgIndex;
    int saveAlgNumber;
    private void Start()
    {

        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();
        findEGMGameObject = GameObject.FindGameObjectWithTag("EGM");
        EGM = findEGMGameObject.GetComponent<EndGameManager>();
        algList = new List<string>();


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

    public int AlgSplitMethod(int indexAlgArray)
    {
        algList.Clear();
        //string value = algList[tempIndexPawnPosition];
        string stringAlg = algArray[indexAlgArray];
        print("The index/algorithm # " + indexAlgArray + ", and the string value is " + stringAlg + "\n");
        //har delimiter = '';
        //string[] substrings = value.Split(delimiter);

        foreach (char stuff in stringAlg)
        {
            string temp = stuff.ToString();
            algList.Add(temp);
            //print(stuff);
        }

        randMoveIndexAlglist = Random.Range(0, algList.Count);
        //random = 2;
        //print("subList[random] " + subList[random] + " random = " +random +"\n");
        randMove = int.Parse(algList[randMoveIndexAlglist]);
        print("The selected move is " + randMove + "\n");
        saveAlgNumber = GM.indexPawnPosition;
        return randMove;
        /*foreach (string stuff in subList)
        {
            print(stuff);
        }
        print(subList[0]);*/
    }


    public void AILostRemoveMoves2()
    {
        //print("subList.Count " + algList.Count);
        //print("tempIndexValue2  " + randIndexAlgList);
        if (algList.Count > 1 && !isAIMoveRemoved) // only delete if there are more than 1 in the sublist
        {


            //print("algList[GM.indexPawnPosition] " + algList[tempIndexPawnPosition] + "\n");
            algList.RemoveAt(randMoveIndexAlglist);
            //algList[tempIndexPawnPosition] = string.Concat(subList);
            algArray[saveAlgNumber] = string.Concat(algList);
            print("The selected move has been removed and new algorithm string is "
                + algArray[saveAlgNumber] + "\n");
            //alg.Remove(algValue);
            //print("alg" + algValue + "removed\n");
            isAIMoveRemoved = true;

        }
    }

    public void AILostRemoveMoves(List<int> alg, int algValue)
    {
        if (alg.Count > 1) // only delete if there are more than 1 in the list
        {
            if (!GM.pawnPositionsIntoLongString.Contains("B") || GM.threeLetters[0].Contains("W") || !isAICanMove)
            {
                alg.Remove(algValue);
                print("alg" + algValue + "removed\n");
            }

        }
    }

    private void AITurn()
    {
        if (GM.indexPawnPosition < 0) isAICanMove = false;

        if (interval > 0) interval -= Time.deltaTime;
        else
        {
            if (!EGM.isEndGameTriggered) print("ai turn\n");            
            //**************************************************Turn   2***********************************
            if (GM.indexPawnPosition == 0)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, LEFT, MID, "B");//old,new
                else if (savedAlgIndex == 1) GM.MovePiece(MID, TOP, MID, MID, "B");
                else GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 1)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");

                if (savedAlgIndex == 0) GM.MovePiece(LEFT, TOP, LEFT, MID, "B");
                else GM.MovePiece(LEFT, TOP, MID, MID, "B");
                Confirming();
            }
            //**************************************************Turn   4***********************************
            else if (GM.indexPawnPosition == 2)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, TOP, MID, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else if (savedAlgIndex == 2) GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                else GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 3)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, LEFT, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                else if (savedAlgIndex == 2) GM.MovePiece(MID, MID, RIGHT, BOT, "B");
                else GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 4)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, TOP, MID, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 5)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, LEFT, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(MID, TOP, MID, MID, "B");
                else GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 6)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(MID, MID, LEFT, BOT, "B");
                else GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 7)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                else GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 8)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                else GM.MovePiece(LEFT, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 9)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, TOP, MID, MID, "B");
                else GM.MovePiece(MID, TOP, LEFT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 10)
            {

                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 11)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();

            }
            else if (GM.indexPawnPosition == 12)//bot always wins
            {
                GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                Confirming();
            }
            /////******************************Turn    6 *********************
            else if (GM.indexPawnPosition == 13)
            {

                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                else GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 14) //bot will always win on this move
            {
                GM.MovePiece(LEFT, TOP, MID, MID, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 15)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                else GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 16)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, LEFT, MID, "B");
                else GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                Confirming();
            }

            else if (GM.indexPawnPosition == 17)
            {

                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                else GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();

            }
            else if (GM.indexPawnPosition == 18)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, MID, MID, BOT, "B");
                else GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 19)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else if (savedAlgIndex == 1) GM.MovePiece(RIGHT, TOP, RIGHT, MID, "B");
                else GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                Confirming();

            }
            else if (GM.indexPawnPosition == 20)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0) GM.MovePiece(MID, TOP, LEFT, MID, "B");
                else  GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 21)
            {

                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0)  GM.MovePiece(MID, TOP, RIGHT, MID, "B");
                else GM.MovePiece(MID, MID, MID, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 22)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0)  GM.MovePiece(LEFT, TOP, MID, MID, "B");
                else  GM.MovePiece(LEFT, MID, LEFT, BOT, "B");
                Confirming();
            }
            else if (GM.indexPawnPosition == 23)
            {
                savedAlgIndex = AlgSplitMethod(GM.indexPawnPosition);
                print("tempIndexValue " + randMove + "\n");
                if (savedAlgIndex == 0)  GM.MovePiece(RIGHT, TOP, MID, MID, "B");
                else GM.MovePiece(RIGHT, MID, RIGHT, BOT, "B");
                Confirming();
            }
        }
    }
    public void Confirming()
    {
        GM.isPlayerTurn = true;
        interval = 3f;
    }

}
