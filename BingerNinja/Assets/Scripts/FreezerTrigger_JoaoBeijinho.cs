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

     void OnTriggerEnter2D(Collider2D a)
    {
        if (a is BoxCollider2D && a.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag))
        {
            if (!m_enemyList.Contains(a))
            {
                m_enemyList.Add(a);
            }
        }
    }

    void OnTriggerExit2D(Collider2D b)
    {
        if (b is BoxCollider2D && b.gameObject.CompareTag(Tags_JoaoBeijinho.m_enemyTag))
        {
            if (m_enemyList.Contains(b))
            {
                m_enemyList.Remove(b);
            }
        }
    }
}
