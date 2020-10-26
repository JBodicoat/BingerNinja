//Joao Beijinho
///This class toggles the player stealth and crouch on and off

//Joao 18/10/2020 - Added IsStealthed Function
//Joao 23/10/2020 - Added IsCrouched Function
//Joao 26/10/2020 - Simplified Crouch() function to only enable/disable crouching

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth_JoaoBeijinho : MonoBehaviour
{
    public bool m_stealthed = false;
    public bool m_crouched = false;

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
    /// Crouch Function enables/disables player crouching when called    !!!NEED ENEMY CLASS TO TEST!!!
    /// </summary>
    public void Crouch()
    {
        m_crouched = !m_crouched;

        if (m_crouched == true)
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().m_speed = 0f;
        }
        else
        {
            gameObject.GetComponent<PlayerMovement_MarioFernandes>().ResetSpeed();
        }
    }
}
