//sebastian mol
//sebastian mol 30/10/20 removed patrol function

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
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

    internal override void EnemyBehaviour()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                //move form one postion to another
                if (m_playerDetected) m_currentState = state.CHASE;
                Patrol();
                break;

            case state.CHASE:
                //move towards the player if he has been detected
                if (IsPlayerInLineOfSight())
                {
                    if (Vector2.Distance(transform.position, m_playerTransform.position) < m_shootingRange / 1.5f)
                    {
                        ClearPath();
                        m_currentState = state.ATTACK;
                    }
                    else
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
                break;

            case state.ATTACK:
                //personalized attack
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
                break;
        }     
    }
   
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
                    projectile.GetComponent<BulletMovment_SebastianMol>().direction = (m_playerTransform.position - transform.position).normalized;
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
}
