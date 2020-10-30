//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created trigger for normal pressure pads

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Normal Pressure Pads. Once the pressure pad has been activated the door will remain open
/// </summary>
public class PressurePad_JoaoBeijinho : MonoBehaviour
{
    public GameObject m_door;

    private string m_playerTag = "Player";
    private string m_crateTag = "Crate";
    private string m_meleeWeaponTag = "MeleeWeapon";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//Collision with specific objects
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}