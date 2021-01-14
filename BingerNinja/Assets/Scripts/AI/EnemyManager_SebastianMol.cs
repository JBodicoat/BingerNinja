//sebastian mol
//sebastian mol ------ enemy manager complete
using UnityEngine;

/// <summary>
/// class that manages enemy can ask it questions like has the player been seen
/// </summary>
public class EnemyManager_SebastianMol : MonoBehaviour
{
    private GameObject[] q;

    /// <summary>
    /// is the player seen by any enemy
    /// </summary>
    /// <returns>weather it has been seen by any enemy</returns>
    public bool w()
    {
        for (int i = 0; i < q.Length; i++)
        {
            q = GameObject.FindGameObjectsWithTag("Enemy");
            if(q[i].GetComponent<BaseEnemy_SebastianMol>().Q)
            {
                return true;
            }
        }
        return false;
    }
}
