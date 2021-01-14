//sebastian mol
//sebastian mol ------ enemy manager complete
using UnityEngine;

/// <summary>
/// class that manages enemy can ask it questions like has the player been seen
/// </summary>
public class EnemyManager_SebastianMol : MonoBehaviour
{
    private GameObject[] a;

    /// <summary>
    /// is the player seen by any enemy
    /// </summary>
    /// <returns>weather it has been seen by any enemy</returns>
    public bool IsPlayerSeen()
    {
        for (int i = 0; i < a.Length; i++)
        {
            a = GameObject.FindGameObjectsWithTag("Enemy");
            if(a[i].GetComponent<BaseEnemy_SebastianMol>().m_playerDetected)
            {
                return true;
            }
        }
        return false;
    }
}
