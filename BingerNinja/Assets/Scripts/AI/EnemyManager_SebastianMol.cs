using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//sebastian mol
//class that manages enemy can ask it questions like has tegh player been seen
public class EnemyManager_SebastianMol : MonoBehaviour
{
    private GameObject[] m_allEnemies;
    void Start()
    {
        m_allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

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
