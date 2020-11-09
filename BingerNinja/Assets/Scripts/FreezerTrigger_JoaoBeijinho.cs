//Joao Beijinho

//Joao Beijinho 06/11/2020 - Created this script and trigger events
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

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
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag) && collision is BoxCollider2D)//Player for testing
        {
            m_enemyInFreezer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag) && collision is BoxCollider2D)//Player for testing
        {
            m_enemyInFreezer = false;
        }
    }
}
