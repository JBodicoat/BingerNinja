//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created script, enum for the type of item, a switch to activate the objects functionality and the door functionality

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class declares which type of item the object is and what functionality each type of item has
/// </summary>
public class ControlPanelActivateObject_JoaoBeijinho : MonoBehaviour
{
    public enum ObjectType
    {
        Door,
        Light,
        Computer,
    }

    public ObjectType m_functionality;

    public void ActivateObject()//Call this function to activate object functionality
    {
        switch (m_functionality)//Define object functionality
        {
            case ObjectType.Door:
                print("This a door");
                gameObject.GetComponent<Collider2D>().enabled = false;
                break;
            case ObjectType.Light:
                print("This a light");
                break;
            case ObjectType.Computer:
                print("This a computer");
                //make computer sound
                break;
        }
    }
}
