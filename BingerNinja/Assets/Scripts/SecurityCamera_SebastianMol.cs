using UnityEngine;

public class SecurityCamera_SebastianMol : MonoBehaviour
{
    public float m_alertRadius;

    void OnTriggerEnter2D(Collider2D a)
    {
        if(a.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            //if whoithin radius
            GameObject[] b = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_enemyTag);
            foreach (var enemy in b)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) < m_alertRadius)
                {
                    BaseEnemy_SebastianMol c = enemy.GetComponent<BaseEnemy_SebastianMol>();
                    if (c.m_currentState == state.WONDER)
                    {
                        float x = Random.Range(-0.3f, 0.3f);
                        float y = Random.Range(-0.3f, 0.3f);
                        Vector3 e = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
                        c.ForceCuriosity(e);
                    }
                }
            }
           
            //initiate curious enemy state
        }
    }
}
