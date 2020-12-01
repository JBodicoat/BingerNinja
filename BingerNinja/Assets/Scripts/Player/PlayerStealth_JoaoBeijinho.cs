//Joao Beijinho

//Joao 18/10/2020 - Added IsStealthed Function
//Joao 23/10/2020 - Added IsCrouched Function
//Joao 26/10/2020 - Simplified Crouch() function to only enable/disable crouching and stop/resume movement
//Joao 11/10/2020 - Added IsInVent()
//Louie 30/11/2020 - Crouch animation and crouch speed float

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class toggles the player stealth and crouch on and off
/// </summary>
public class PlayerStealth_JoaoBeijinho : MonoBehaviour
{
    public bool m_stealthed = false;
    public bool m_crouched = false;
    public bool m_inVent = false;

    private PlayerAnimation_LouieWilliamson m_pAnimation;
    public float crouchSpeed;
    /// <summary>
    /// Call IsStealthed() to check if the player is in stealth, it will return true if it is
    /// </summary>
    public bool IsStealthed()
    {
        return m_stealthed;
    }

    /// <summary>
    /// Call IsCrouched() to check if the player is crouched, it will return true if it is
    /// </summary>
    public bool IsCrouched()
    {
        return m_crouched;
    }

    /// <summary>
    /// Call IsInVent() to check if the player is inside a vent, it will return true if it is
    /// </summary>
    public bool IsinVent()
    {
        return m_inVent;
    }

    /// <summary>
    /// Crouch Function enables/disables player crouching when called    !!!NEED ENEMY CLASS TO TEST!!!
    /// </summary>
    public void Crouch()
    {
        m_crouched = !m_crouched;

        if (m_crouched == true)
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().m_speed = crouchSpeed;
            m_pAnimation.Crouching(true);
        }
        else
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().ResetSpeed();
            m_pAnimation.Crouching(false);
        }
    }
    private void Start()
    {
        m_pAnimation = GetComponentInChildren<PlayerAnimation_LouieWilliamson>();
    }
}
