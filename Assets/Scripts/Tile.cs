using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    private Color blueColour = new Color(0f, 0.1044993f, 1f);
    private float interval = 1f;
    private GameObject findGMGameObject;
    private GameManager GM;
    private Color defColor;

    private void Start()
    {
        findGMGameObject = GameObject.FindGameObjectWithTag("GM");
        GM = findGMGameObject.GetComponent<GameManager>();
        defColor = GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerStay(Collider other)
    {
        
        //prevents grid from tuirning red when a piece is on it
        if (other.gameObject.CompareTag("WhitePawn")|| other.gameObject.CompareTag("BlackPawn"))
        {
            gameObject.layer = 2;
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WhitePawn") || other.gameObject.CompareTag("BlackPawn"))
        {

            gameObject.layer = 8;

        }

    }

    private void Update()
    {
        
        if (gameObject.GetComponent<MeshRenderer>().material.color == Color.red && interval >0)
        {            
                interval -= Time.deltaTime;
        }
        else 
        {
            gameObject.GetComponent<MeshRenderer>().material.color = defColor;
            interval = 3f;
        }


    }
}
