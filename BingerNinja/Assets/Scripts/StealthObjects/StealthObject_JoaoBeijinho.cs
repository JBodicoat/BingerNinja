//Joao Beijinho
///This class handles interactions with stealth objects

//Joao Beijinho 18/10/2020 - Hide function and variables to find stealth objects scripts aswell as the player stealth script
//Joao Beijinho 20/10/2020 - Implemented trigger for entering and exiting the vent

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthObject_JoaoBeijinho : MonoBehaviour
{
    PlayerStealth_JoaoBeijinho playerStealthScript;
    public Vent_JoaoBeijinho ventScript;

    public void Hide()
    {
        playerStealthScript.m_stealthed = !playerStealthScript.m_stealthed;
    }

    #region Enter and Exit triggers
    private void OnTriggerEnter2D(Collider2D collision)//Enter and Exit vent
    {
        if (collision.tag == "Player")
        {
            if (playerStealthScript.m_stealthed == false)//If player isn't stealthed he's not inside the vent
            {
                ventScript.VentEnter();
            }
            else
            {
                ventScript.VentExit();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        ventScript = gameObject.GetComponent<Vent_JoaoBeijinho>();
    }
}