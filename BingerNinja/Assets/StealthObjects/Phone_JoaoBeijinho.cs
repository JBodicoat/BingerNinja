//Joao Beijinho

//Joao Beijinho 18/10/2020 - Created draft for Phone interaction, making the player stealth and unable to move for a few seconds
//Joao Beijinho 19/10/2020 - Updated Movement restriction using PlayerController, and now the player can move after a specific set of time
//Joao Beijinho 26/10/2020 - Removed reference to PlayerController script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class handles interaction with phone, makes player stealth for a while but unable to move
/// </summary>
public class Phone_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(PhoneDuration());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    //Go into stealth and stop movement while on phone, unstealth and resume movement after a set time
    IEnumerator PhoneDuration()
    {
        Hide();
        m_playerControllerScript.m_movement.Disable();

        yield return new WaitForSeconds(5);

        Hide();
        m_playerControllerScript.m_movement.Enable();
    }
}
