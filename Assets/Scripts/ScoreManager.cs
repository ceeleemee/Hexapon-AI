
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameObject findEGMGameObject;
    private EndGameManager EGM;
    public Text playerScoreText;
    public Text aiScoreText;
    public Text lastFiveWinsText;
    private int playerTempScore = 0;
    private int aiTempScore = 0;
    private List<string> lastFiveWinData = new List<string>() { "", "", "", "", "" };
    private void Start()
    {

        findEGMGameObject = GameObject.FindGameObjectWithTag("EGM");
        EGM = findEGMGameObject.GetComponent<EndGameManager>();

    }
    // Update is called once per frame
    void Update()
    {
        if (playerScoreText != null || aiScoreText != null)
        {

            playerScoreText.text = EGM.playerScoreCount.ToString();
            aiScoreText.text = EGM.aiScoreCount.ToString();
        }
        if (playerTempScore < EGM.playerScoreCount || aiTempScore < EGM.aiScoreCount)
        {
            lastFiveWinData.Add("player");
            playerTempScore = EGM.playerScoreCount;
            aiTempScore = EGM.aiScoreCount;
        }

        if (lastFiveWinData.Count > 5)
            lastFiveWinData.RemoveAt(0);
        if (lastFiveWinsText != null)
        {
            
            lastFiveWinsText.text =
                "" + lastFiveWinData[4] + "\n" +
                "" + lastFiveWinData[3] + "\n" +
                "" + lastFiveWinData[2] + "\n" +
                "" + lastFiveWinData[1] + "\n" +
                "" + lastFiveWinData[0] + "\n";
        }
    }
}
