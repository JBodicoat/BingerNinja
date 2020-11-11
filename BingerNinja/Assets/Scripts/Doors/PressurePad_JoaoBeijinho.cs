//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created trigger for normal pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Normal Pressure Pads. Once the pressure pad has been activated the door will remain open
/// </summary>
public class PressurePad_JoaoBeijinho : MonoBehaviour
{
    public GameObject m_door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_crateTag) || collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_meleeWeaponTag))//Collision with specific objects
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}