using UnityEngine;public class EnemyManager_SebastianMol : M{private GameObject[] m_allEnemies;public bool IsPlayerSeen(){for (int i = 0; i < m_allEnemies.Length; i++){m_allEnemies = GameObject.FindGameObjectsWithTag("Enemy");if(m_allEnemies[i].GetComponent<BaseEnemy_SebastianMol>().m_playerDetected){return true;}}return false;}}