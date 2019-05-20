using UnityEngine;

public class InitiateManagers : MonoBehaviour
{

    public GameObject gameManger;
    public GameObject botManager;
    public GameObject endGameManager;
    
    // Start is called before the first frame update

    void Awake()
    {  //Instantiate gameManager prefab
        if (GameManager.instance == null)
            Instantiate(gameManger);
        //Instantiate SoundManager prefab
        if (BotManager.instance == null)
            Instantiate(botManager);
        if (EndGameManager.instance == null)
            Instantiate(endGameManager);

    }

}
