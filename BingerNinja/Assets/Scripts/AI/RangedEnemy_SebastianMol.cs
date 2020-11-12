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
    [Tooltip("how far the player can be befor enemy shoots")]
    public float m_shootingRange;
    [Tooltip("how long the player has to be out of sight befor enemy losses intrest")]
    public float m_outOfSightDeley;  
    private void RangedAttack()
    {
        if (Vector2.Distance(transform.position, m_playerTransform.position) < m_shootingRange)
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
        else 
        {
            m_currentState = state.CHASE;
        }
    }

    void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_shootingRange);
   }

    internal override void ChaseState()
    {
        if (IsPlayerInLineOfSight()) // if you can see player
        {
            if (Vector2.Distance(transform.position, m_playerTransform.position) < m_shootingRange / 1.5f) //if the player is in range
            {
                ClearPath();
                m_currentState = state.ATTACK;
            }
            else// if the [layer is out fo range
            {
                PathfindTo(m_playerTransform.position);
            }
        }
        else
        {
            if (m_currentPath.Count == 0)
            {
                if (m_outOfSightTimer <= 0)
                {
                    m_currentState = state.WONDER;
                    m_playerDetected = false;
                }
                else
                {
                    m_outOfSightTimer -= Time.deltaTime;
                }
            }
        }
    }

    internal override void AttackState()
    {
        if (IsPlayerInLineOfSight())
        {
            RangedAttack();
            m_outOfSightTimer = m_outOfSightDeley;
            m_playerDetected = true;
        }
        else
        {
            m_playerDetected = false;
            if (m_outOfSightTimer <= 0)
            {
                m_currentState = state.WONDER;
            }
            else
            {
                m_outOfSightTimer -= Time.deltaTime;
            }
        }
    }

    internal override void RetreatState()
    {
    }
}
