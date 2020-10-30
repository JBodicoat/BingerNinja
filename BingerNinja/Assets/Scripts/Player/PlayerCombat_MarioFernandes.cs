// Mário Fernandes
/// This class stores the current weapon on the player and make im abel to use it 

// Mário 17/10/2020 - Create class and Attack, PickUpFood, IIsHoldingFood Funcions
// Joao 25/10/2020 - Stop weapon usage while crouched in update

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_MarioFernandes : MonoBehaviour
{
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

  public  bool IsHoldingFood()
         {
       
        //Temp, should go the current weapon
        return false;
         }

    void PickUpFood()
    {

    }

    void Attack()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_playerStealthScript.m_crouched)
        {

        }
    }
}


