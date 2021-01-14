﻿//Joao Beijinho

//Joao Beijinho 18/10/2020 - Hide function and variables to find stealth objects scripts aswell as the player stealth script
//Joao Beijinho 20/10/2020 - Implemented trigger for entering and exiting the vent
//Joao Beijinho 25/10/2020 - Moved Trigger back into Vent script. Made class abstract
//Joao Beijinho 11/10/2020 - Added ToggleVent()

using UnityEngine;

/// <summary>
///This class handles interactions with stealth objects
/// </summary>
public abstract class StealthObject_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG A;
    protected PlayerStealth_JoaoBeijinho B;

    public void Awake()
    {
        A = FindObjectOfType<PlayerController_JamieG>();
        B = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
    }

    /// <summary>
    /// Hide() Function toggles stealth on/off
    /// </summary>
    public void W()
    {
        B.A = !B.A;
    }

    public void Q()
    {
        B.C = !B.C;
    }
}