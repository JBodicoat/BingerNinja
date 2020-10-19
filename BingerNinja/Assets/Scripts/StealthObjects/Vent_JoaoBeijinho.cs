//Joao Beijinho
///This class makes the player enter and exit vents aswell as restrict the player movement inside the vent

//Joao Beijinho 19/10/2020 - Created draft of the enter and exit states for the vents

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_JoaoBeijinho : MonoBehaviour
{
    StealthObject_JoaoBeijinho steathObjectScript;

    GameObject player;
    GameObject ventPath;

    //Vents and player position to make player enter vents
    private Transform playerPos;
    private Transform ventPos;
    private Transform ventPathPos;

    #region Enter and Exit triggers
    private void OnTriggerEnter2D(Collider2D collision)//Vent Enter
    {
        if (collision.tag == "Player")
        {

            VentEnter();

            print("vent enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//Vent Exit
    {
        if (collision.tag == "Player")
        {

            VentExit();

            print("vent exit");
        }
    }
    #endregion


    //On enter contact place player on the position of the vent but on the z position of the vent path
    //Change to be made - place player on vent position, disable any collisions except vent collisions
    private void VentEnter()
    {
        steathObjectScript.Hide();
        playerPos.position = new Vector3(ventPos.position.x, ventPos.position.y, ventPathPos.position.z);
    }
     
    //On exit contact place player on the position of the vent and back on the z position of the vent
    private void VentExit()
    {
        steathObjectScript.Hide();
    }

    // Start is called before the first frame update
    void Start()
    {
        steathObjectScript = FindObjectOfType<StealthObject_JoaoBeijinho>();

        //Get the player, its position and its collider
        player = GameObject.Find("Player");
        playerPos = player.GetComponent<Transform>();


        //Get the vent, vent path and their position
        ventPos = gameObject.GetComponent<Transform>();
        ventPath = GameObject.Find("VentPath");
        ventPathPos = ventPath.GetComponent<Transform>();
    }


    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
