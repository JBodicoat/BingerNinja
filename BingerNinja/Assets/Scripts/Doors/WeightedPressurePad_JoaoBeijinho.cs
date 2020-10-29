//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created triggers for weighted pressure pads

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Weighted Pressure Pads. An object needs to stay on top of it so that the door can remain open
/// </summary>
public class WeightedPressurePad_JoaoBeijinho : MonoBehaviour
{
    public GameObject m_door;

    private string m_playerTag = "Player";
    private string m_crateTag = "Crate";
    private string m_meleeWeaponTag = "MeleeWeapon";

    private void OnTriggerEnter2D(Collider2D collision)//Open door when an Object is on top of it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//collision with every array object except Projectile
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)//Close door when an object is removed from it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//if this scirpt is in a Pressure Pad
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}