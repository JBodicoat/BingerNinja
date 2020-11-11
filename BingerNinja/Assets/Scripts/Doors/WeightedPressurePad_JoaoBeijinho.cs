//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created triggers for weighted pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Weighted Pressure Pads. An object needs to stay on top of it so that the door can remain open
/// </summary>
public class WeightedPressurePad_JoaoBeijinho : MonoBehaviour
{
    public GameObject m_door;

    private void OnTriggerEnter2D(Collider2D collision)//Open door when an Object is on top of it
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_crateTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_meleeWeaponTag))//collision with every array object except Projectile
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)//Close door when an object is removed from it
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_crateTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_meleeWeaponTag))//if this scirpt is in a Pressure Pad
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}