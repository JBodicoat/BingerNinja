//Joao Beijinho

//Joao Beijinho 06/11/2020 - Created this script and trigger events

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class checks if the enemy is in trigger area, to later call in the ControlPanelActivateObject Script
/// </summary>
public class FreezerTrigger_JoaoBeijinho : MonoBehaviour
{
    public bool m_enemyInFreezer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision is BoxCollider2D)//Player for testing
        {
            m_enemyInFreezer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision is BoxCollider2D)//Player for testing
        {
            m_enemyInFreezer = false;
        }
    }
}
