//Joao Beijinho

//Joao 18/10/2020 - Added IsStealthed Function
//Joao 23/10/2020 - Added IsCrouched Function
//Joao 26/10/2020 - Simplified Crouch() function to only enable/disable crouching and stop/resume movement
//Joao 11/10/2020 - Added IsInVent()
// Jack 18/11/2020 - Removed getter functions in favour of directly accessing public variables to save on file size
//                   Minor optimization to Crouch function

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

    /// <summary>
    /// Crouch Function enables/disables player crouching when called    !!!NEED ENEMY CLASS TO TEST!!!
    /// </summary>
    public void Crouch()
    {
        m_crouched = !m_crouched;

        PlayerMovement_MarioFernandes playerMovementScript = GetComponent<PlayerMovement_MarioFernandes>();
        if (m_crouched)
        {
            playerMovementScript.m_speed = 0f;
        }
        else
        {
            playerMovementScript.ResetSpeed();
        }
    }
}
