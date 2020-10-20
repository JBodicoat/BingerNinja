//Joao Beijinho
///This class toggles the player stealth on and off

//Joao 18/10/2020 - Added IsStealthed Function

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth_JoaoBeijinho : MonoBehaviour
{
    public bool m_stealthed = false;

    //Call IsStealthed() to check if the player is in stealth, it will return true if it is
    public bool IsStealthed()
    { 
        //Test
        //var curKeyboard = Keyboard.current;
        //
        //if (curKeyboard.spaceKey.isPressed)
        //{
        //    stealthed = !stealthed;
        //    print(stealthed);
        //}

        return m_stealthed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
