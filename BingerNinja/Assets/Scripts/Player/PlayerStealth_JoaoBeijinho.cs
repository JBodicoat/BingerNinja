//Joao Beijinho
///This class toggles the player stealth and crouch on and off

//Joao 18/10/2020 - Added IsStealthed Function
//Joao 23/10/2020 - Added IsCrouched Function

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth_JoaoBeijinho : MonoBehaviour
{
    public bool m_stealthed = false;
    public bool m_crouched = false;

    //Call IsStealthed() to check if the player is in stealth, it will return true if it is
    public bool IsStealthed()
    {
        return m_stealthed;
    }

    public bool IsCrouched()
    {
        return m_crouched;
    }

    public void Crouch()
    {
        List<Collider2D> playerCollisions = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        
        if (gameObject.GetComponent<BoxCollider2D>().OverlapCollider(filter, playerCollisions) > 0)
        {
            foreach (BoxCollider2D crouchObjectCollider in playerCollisions)
            {
                if (crouchObjectCollider.GetComponentInParent<HideBehindable_JoaoBeijinho>().IsCrouchable())
                {
                    m_stealthed = !m_stealthed;
                    m_crouched = !m_crouched;
                }
            }
        }
    }
}
