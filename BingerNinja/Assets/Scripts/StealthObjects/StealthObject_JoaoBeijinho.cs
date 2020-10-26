//Joao Beijinho
///This class handles interactions with stealth objects

//Joao Beijinho 18/10/2020 - Hide function and variables to find stealth objects scripts aswell as the player stealth script
//Joao Beijinho 20/10/2020 - Implemented trigger for entering and exiting the vent
//Joao Beijinho 25/10/2020 - Moved Trigger back into Vent script. Made class abstract

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StealthObject_JoaoBeijinho : MonoBehaviour
{
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    public void Awake()
    {
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
    }

    public void Hide()
    {
        m_playerStealthScript.m_stealthed = !m_playerStealthScript.m_stealthed;
    }
}