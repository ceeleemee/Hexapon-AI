using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateManagers : MonoBehaviour
{

    public GameObject GM;
    public GameObject BM;
    // Start is called before the first frame update

    void Awake()
    {
        if (GameManager.instance == null)

            //Instantiate gameManager prefab
         Instantiate(GM);


        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
           if (BotManager.instance == null)

        //Instantiate SoundManager prefab
           Instantiate(BM);

    }

}
