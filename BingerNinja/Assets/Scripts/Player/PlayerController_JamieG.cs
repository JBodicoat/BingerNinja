//Jamie G
///This script is used to interact with the other Player scripts. It also takes in input from the InputAction component which can be passed to the other player scripts. The enemy script will use this script to interact with the player.

//Jamie 18/10/20 - first implemented
//Joao 23/10/20 - Added input for interactions.
//Joao 25/10/20 - Added interaction in update.
//Joao 26/10/20 - Added input for crouching, the input used for this was interact.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_JamieG : MonoBehaviour
{
    //The player gameobject itself
    protected GameObject m_player;

    //Reference to the InputAction component
    public InputAction m_movement;
    public InputAction m_interact;
    public InputAction m_crouch;
    public InputAction m_eat;
    public InputAction m_attack;
    public InputAction m_roll;

    public InputAction m_switchWeapons;

    //Reference to the other player scripts
    private PlayerMovement_MarioFernandes m_playerMovementScript;
    //private PlayerHealthHunger_MarioFernandes playerHHScript;
    //PlayerCombat script here
    private PlayerStealth_JoaoBeijinho m_playerStealthScript;
    
    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_playerMovementScript = m_player.GetComponent<PlayerMovement_MarioFernandes>();
        m_playerStealthScript = gameObject.GetComponent<PlayerStealth_JoaoBeijinho>();
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
        if(m_roll.triggered&& m_playerMovementScript.isRolling==false)
        {
            m_playerMovementScript.isRolling = true;
            m_playerMovementScript.RollMovement();
        }
        if (m_crouch.triggered)

        {
            m_playerStealthScript.Crouch();
        }
    }

    #region InputAction Functions
    //These functions are required for the InputAction component to work
    private void OnEnable()
    {
        m_movement.Enable();
        m_interact.Enable();
        m_crouch.Enable();
        m_eat.Enable();
        m_attack.Enable();
        m_roll.Enable();
        m_switchWeapons.Enable();
    }

    private void OnDisable()
    {
        m_movement.Disable();
        m_interact.Disable();
        m_crouch.Disable();
        m_eat.Disable();
        m_attack.Disable();
    }
    #endregion

}
