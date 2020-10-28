//Jamie G
///This script is used to interact with the other Player scripts. It also takes in input from the InputAction component which can be passed to the other player scripts. The enemy script will use this script to interact with the player.

//Jamie 18/10/20 - first implemented

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
    

    //Reference to the other player scripts
    private PlayerMovement_MarioFernandes m_playerMovementScript;
    //private PlayerHealthHunger_MarioFernandes playerHHScript;
    //PlayerCombat script here
    //PlayerStealth script here
    
    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_playerMovementScript = m_player.GetComponent<PlayerMovement_MarioFernandes>();
    }

    
    void FixedUpdate()
    {
        //Read input from the InputAction and associate it with the movementVector 
        Vector2 movementvector = m_movement.ReadValue<Vector2>();

        //Pass the movement vector from InputAction component to the PlayerMovement script
        m_playerMovementScript.RecieveVector(movementvector);



    }

    #region InputAction Functions
    //These functions are required for the InputAction component to work
    private void OnEnable()
    {
        m_movement.Enable();
    }

    private void OnDisable()
    {
        m_movement.Disable();
    }
    #endregion

}
