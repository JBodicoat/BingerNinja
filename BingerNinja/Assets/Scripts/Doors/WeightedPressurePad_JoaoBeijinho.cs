//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created triggers for weighted pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Weighted Pressure Pads. An object needs to stay on top of it so that the door can remain open
/// </summary>
public class WeightedPressurePad_JoaoBeijinho : MonoBehaviour
{
    //Reference the door collider to turn if on/off(closed/open)
    public Collider2D m_doorCollider;
    public Sprite activatedPressurePad, inactivePressurePad;
    private string m_playerTag = "Player";
    private string m_crateTag = "Crate";
    private string m_meleeWeaponTag = "MeleeWeapon";

    private void OnTriggerEnter2D(Collider2D collision)//Open door when an Object is on top of it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//collision with every array object except Projectile
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = activatedPressurePad;
            m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)//Close door when an object is removed from it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//if this scirpt is in a Pressure Pad
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = inactivePressurePad;
            m_doorCollider.GetComponent<Collider2D>().enabled = true;//Close door
        }
    }
}