﻿//Joao Beijinho

//Joao 18/10/2020 - Added IsStealthed Function
//Joao 23/10/2020 - Added IsCrouched Function
//Joao 26/10/2020 - Simplified Crouch() function to only enable/disable crouching and stop/resume movement
//Joao 11/10/2020 - Added IsInVent()
//Louie 30/11/2020 - Crouch animation and crouch speed float

using UnityEngine;

/// <summary>
///This class toggles the player stealth and crouch on and off
/// </summary>
public class PlayerStealth_JoaoBeijinho : MonoBehaviour
{
    public bool A = false;
    public bool B = false;
    public bool C = false;

     PlayerAnimation_LouieWilliamson b;
    public float D;
    /// <summary>
    /// Call IsStealthed() to check if the player is in stealth, it will return true if it is
    /// </summary>
    public bool F()
    {
        return A;
    }

    /// <summary>
    /// Call IsCrouched() to check if the player is crouched, it will return true if it is
    /// </summary>
    public bool G()
    {
        return B;
    }

    /// <summary>
    /// Call IsInVent() to check if the player is inside a vent, it will return true if it is
    /// </summary>
    public bool H()
    {
        return C;
    }

    /// <summary>
    /// Crouch Function enables/disables player crouching when called    !!!NEED ENEMY CLASS TO TEST!!!
    /// </summary>
    public void J()
    {

        PlayerController_JamieG c = GetComponent<PlayerController_JamieG>();
        B = !B;

        if (B)
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().m_speed = D;
            c.m_attackTap.Disable();
            c.m_attackSlowTap.Disable();
            c.m_roll.Disable();
            c.m_eat.Disable();
            b.Crouching(true);
        }
        else
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().ResetSpeed();
            c.m_attackTap.Enable();
            c.m_attackSlowTap.Enable();
            c.m_roll.Enable();
            c.m_eat.Enable();
            b.Crouching(false);
        }
    }
     void Start()
    {
        b = GetComponentInChildren<PlayerAnimation_LouieWilliamson>();
    }
}
