//Joao Beijinho

//Joao Beijinho 06/11/2020 - Created this script and trigger events
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Joao Beijinho 19/11/2020 - Changed triggers to add each enemy to a list on enter, and discard them on exit

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class checks if the enemy is in trigger area, to later call in the ControlPanelActivateObject Script
/// </summary>
public class FreezerTrigger_JoaoBeijinho : MonoBehaviour
{
    public List<Collider2D> m_enemyList = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag))
        {
            if (!m_enemyList.Contains(collision))
            {
                m_enemyList.Add(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag))
        {
            if (m_enemyList.Contains(collision))
            {
                m_enemyList.Remove(collision);
            }
        }
    }
}
