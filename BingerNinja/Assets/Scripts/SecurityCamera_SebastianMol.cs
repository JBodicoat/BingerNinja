using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera_SebastianMol : MonoBehaviour
{
    public float m_alertRadius;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            //if whoithin radius
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_enemyTag);
            foreach (var enemy in allEnemies)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) < m_alertRadius)
                {
                    float randx = Random.Range(-0.3f, 0.3f);
                    float randy = Random.Range(-0.3f, 0.3f);
                    Vector3 pos = new Vector3(transform.position.x + randx, transform.position.y + randy, transform.position.z);
                    enemy.GetComponent<BaseEnemy_SebastianMol>().ForceCuriosity(pos);
                }
            }
           
            //initiate curious enemy state
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_alertRadius);
    }
}
