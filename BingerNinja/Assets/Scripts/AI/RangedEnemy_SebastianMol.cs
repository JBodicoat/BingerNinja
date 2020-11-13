//sebastian mol
//sebastian mol 30/10/20 removed patrol function
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

class RangedEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    [Header("projectile prefabs")]
    public GameObject m_aimer;
    public GameObject m_projectile;
    [Header("projectile Variables")]
    [Tooltip("how fast the projectile moves")]
    public float m_shootDeley;
    [Tooltip("speed of the projectile")]
    public float m_projectileSpeed;

    void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attckRange);
   }

    internal override void AttackBehaviour()
    {
        if (m_playerTransform != null)
        {
            Vector3 dir = Vector3.Normalize(m_playerTransform.position - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            m_aimer.transform.eulerAngles = new Vector3(0, 0, angle);

            if (m_attackTimer <= 0)
            {
                GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));
                projectile.GetComponent<BulletMovment_SebastianMol>().m_direction = (m_playerTransform.position - transform.position).normalized;
                m_attackTimer = m_shootDeley;
            }
            else
            {
                m_attackTimer -= Time.deltaTime;
            }
        }
    }

}
