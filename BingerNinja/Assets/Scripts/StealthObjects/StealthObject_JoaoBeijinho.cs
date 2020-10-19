//Joao Beijinho
///This class handles interactions with stealth objects

//Joao Beijinho 18/10/2020 - Hide function and variables to find stealth objects scripts aswell as the player stealth script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthObject_JoaoBeijinho : MonoBehaviour
{
    PlayerStealth_JoaoBeijinho playerStealthScript;
    Vent_JoaoBeijinho ventScript;

    public void Hide()
    {
        playerStealthScript.m_stealthed = !playerStealthScript.m_stealthed;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        ventScript = FindObjectOfType<Vent_JoaoBeijinho>();
    }



    // Update is called once per frame
    //void Update()
    //{
    //
    //}
}
