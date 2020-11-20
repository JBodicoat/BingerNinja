﻿//Joao Beijinho

//Joao Beijinho 23/10/2020 - Created class to work with crouching
//Joao Beijinho 26/10/2020 - Removed Crouching. Created trigger that toggles stealth/unstealth in the player and disable/enable movement
//Joao Beijinho 27/10/2020 - Changed order of if statemants in update. Created m_isCrouching so that the player can't hide while crouched and n_changeLayer
<<<<<<< HEAD
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
=======
//Elliott Desouza 07/11/2020 - 
// Jack 07/11/2020 - Set canHide & isHiding to private
//                   Removed isCrouching
//                   Added comments within Update
//                   Moved particle system onto the prefab rather than on the player
>>>>>>> Elliott/EnterStealthEffect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///This class allows player to interact with objects that can be used to crouch behind.
/// </summary>
public class HideBehindable_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    //change layer in sprite renderer to render this gameObject above or below the player
    SpriteRenderer m_changeLayer;

    private ParticleSystem m_smokeParticleSystem;
    private bool m_canHide = false;
    private bool m_isHiding = false;

    private string m_playerTag = "Player";

    /// <summary>
    /// Enable player ability to hide
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canHide = true;
        }
    }

    /// <summary>
    /// Disable player ability to hide
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canHide = false;
        }
    }

    void Start()
    {
        m_changeLayer = GetComponent<SpriteRenderer>();
        m_smokeParticleSystem = GetComponentInChildren<ParticleSystem>();
    
    }

    /// <summary>
    /// Check if player can hide a chooses to do so, also checks if player is already hiding and chooses to get out of hiding
    /// </summary>
    void Update()
    {
        // if interact pressed whilst hiding behind this object
        if (m_playerControllerScript.m_interact.triggered && m_isHiding)
        {
            // stop hiding behind this object
            m_canHide = true;
            m_isHiding = false;
            m_changeLayer.sortingOrder = 9;
            Hide();
            m_playerControllerScript.m_movement.Enable();
            m_playerControllerScript.m_crouch.Enable();
           
        }
        // if interact pressed while next to this object & not crouching
        else if (m_playerControllerScript.m_interact.triggered && m_canHide && !m_playerStealthScript.m_crouched)
        {
            // hide behind this object
            m_canHide = false;
            m_isHiding = true;
            m_changeLayer.sortingOrder = 11;
            Hide();
            m_playerControllerScript.m_movement.Disable();
            m_playerControllerScript.m_crouch.Disable();

            m_smokeParticleSystem.Play();
        }
    }
}