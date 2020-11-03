//sebastian mol
//sebastian mol ------ enemy manager complete
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// class that manages enemy can ask it questions like has the player been seen
/// </summary>
public class EnemyManager_SebastianMol : MonoBehaviour
{
    private GameObject[] m_allEnemies;
    void Start()
    {
        m_allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    /// <summary>
    /// is the player seen by any enemy
    /// </summary>
    /// <returns>weather it has been seen by any enemy</returns>
    public bool IsPlayerSeen()
    {
        for (int i = 0; i < m_allEnemies.Length; i++)
        {
            if(m_allEnemies[i].GetComponent<BaseEnemy_SebastianMol>().m_playerDetected)
            {
                return true;
            }
        }
        return false;
    }
}
