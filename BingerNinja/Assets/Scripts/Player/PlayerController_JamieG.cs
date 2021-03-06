﻿//Jamie G
///This script is used to interact with the other Player scripts. It also takes in input from the InputAction component which can be passed to the other player scripts. The enemy script will use this script to interact with the player.

//Jamie 18/10/20 - first implemented
//Joao 23/10/20 - Added input for interactions.
//Joao 25/10/20 - Added interaction in update.
//Joao 26/10/20 - Added input for crouching, the input used for this was interact.
// Elliott 21/11/2020 - changed the function onEnable and ondisable to public
//Mario 22/11/20 - Added tap Attack and slow tap attack
//Louie 30/11/20 - Roll and crouch animations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController_JamieG : MonoBehaviour
{
    //The player gameobject itself
    protected GameObject m_player;

    //Reference to the InputAction component
    public InputAction m_movement;
    public InputAction m_interact;
    public InputAction m_crouch;
    public InputAction m_eat;
    public InputAction m_attackTap;
    public InputAction m_attackSlowTap;
    public InputAction m_aim;
    public InputAction m_roll;
    public InputAction m_changeLevel;

    public InputAction m_switchWeapons;
    public InputAction m_dropWeapons;
    public InputAction m_passDialogue;

    protected int CurrentLevel;

    //Reference to the other player scripts
    private PlayerMovement_MarioFernandes m_playerMovementScript;
    //private PlayerHealthHunger_MarioFernandes playerHHScript;
    //PlayerCombat script here
    private PlayerStealth_JoaoBeijinho m_playerStealthScript;

    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        m_playerMovementScript = m_player.GetComponent<PlayerMovement_MarioFernandes>();
        m_playerStealthScript = gameObject.GetComponent<PlayerStealth_JoaoBeijinho>();
        m_passDialogue.Enable();
    }
    
    void FixedUpdate()
    {
        //Read input from the InputAction and associate it with the movementVector 
        Vector2 movementvector = m_movement.ReadValue<Vector2>();

        //Pass the movement vector from InputAction component to the PlayerMovement script
        m_playerMovementScript.RecieveVector(movementvector);
    }

    private void Update()
    {
        
        if (CurrentLevel >= 13)
        {
            if (m_roll.triggered && m_playerMovementScript.isRolling == false)
            {
                m_playerMovementScript.isRolling = true;
                m_playerMovementScript.RollMovement();
            }
        }

        if (m_crouch.triggered)
        {
            m_playerStealthScript.Crouch();
        }
    }

    #region InputAction Functions
    //These functions are required for the InputAction component to work
    public void OnEnable()
    {
        m_movement.Enable();
        m_interact.Enable();
        m_crouch.Enable();
        m_eat.Enable();
        m_attackTap.Enable();
        m_attackSlowTap.Enable();
        m_roll.Enable();
        m_switchWeapons.Enable();
        m_dropWeapons.Enable();
        m_aim.Enable();
        m_changeLevel.Enable();
    }

    public void OnDisable()
    {
        m_movement.Disable();
        m_interact.Disable();
        m_crouch.Disable();
        m_eat.Disable();
        m_attackTap.Disable();
        m_attackSlowTap.Disable();
        m_roll.Disable();
        m_switchWeapons.Disable();
        m_dropWeapons.Disable();
        m_aim.Disable();
        m_changeLevel.Disable();
    }
    #endregion

}
