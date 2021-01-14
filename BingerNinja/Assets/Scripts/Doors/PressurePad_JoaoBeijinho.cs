//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created trigger for normal pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D

using UnityEngine;

/// <summary>
/// This script is for Normal Pressure Pads. Once the pressure pad has been activated the door will remain open
/// </summary>
public class PressurePad_JoaoBeijinho : MonoBehaviour
{
    //Reference the door collider to turn if on/off(closed/open)
    public Collider2D m_doorCollider;

     string a = "Player";
     string b = "Crate";
     string c = "MeleeWeapon";

     void OnTriggerEnter2D(Collider2D d)
    {
        if (d.tag == a || d.tag == b || d.tag == c)//Collision with specific objects
        {
            m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }
}