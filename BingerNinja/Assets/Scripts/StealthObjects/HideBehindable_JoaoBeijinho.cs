//Joao Beijinho

//Joao Beijinho 23/10/2020 - Created class to work with crouching
//Joao Beijinho 26/10/2020 - Removed Crouching. Created trigger that toggles stealth/unstealth in the player and disable/enable movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///This class allows player to interact with objects that can be used to crouch behind.
/// </summary>
public class HideBehindable_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    public bool m_canHide = false;
    public bool m_isHiding = false;

    /// <summary>
    /// Enable player ability to hide
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_canHide = true;
        }
    }

    /// <summary>
    /// Disable player ability to hide
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_canHide = false;
        }
    }
    
    /// <summary>
    /// Check if player can hide a chooses to do so, also checks if player is already hiding and chooses to get out of hiding
    /// </summary>
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canHide == true)
        {
            m_canHide = false;
            m_isHiding = true;
            Hide();
            m_playerControllerScript.m_movement.Disable();
        }
        else if (m_playerControllerScript.m_interact.triggered && m_isHiding == true)
        {
            m_isHiding = false;
            Hide();
            m_playerControllerScript.m_movement.Enable();
        }
    }
}