//Joao Beijinho
///This class allows player to interact with objects that can be used to crouch behind.

//Joao Beijinho 23/10/2020 - 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideBehindable_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    public bool m_crouchable = true;

    public bool IsCrouchable()
    {
        return m_crouchable;
    }
}
