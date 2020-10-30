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
    private float m_timer;
    private float m_timer2;

    [Tooltip("point of patrole")]
    public Vector2 m_patrolPos1;
    [Tooltip("second point of patrole")]
    public Vector2 m_patrolPos2;

    public float m_deleyBetweenPatrol;
    private float m_timer3;
    private Vector2 m_currentPatrolePos;

    internal override void EnemyBehaviour()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                //move form one postion to another
                if (m_playerDetected) m_currentState = state.CHASE;
                Patrole();
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
                        if (m_timer2 <= 0)
                        {
                            m_currentState = state.WONDER;
                            m_playerDetected = false;
                        }
                        else
                        {
                            m_timer2 -= Time.deltaTime;
                        }
                    }

                }
                break;

            case state.ATTACK:
                //personalized attack
                if (IsPlayerInLineOfSight())
                {
                    RangedAttack();
                    m_timer2 = m_outOfSightDeley;
                    m_playerDetected = true;
                }
                else
                {
                    m_playerDetected = false;
                    if (m_timer2 <= 0)
                    {
                        m_currentState = state.WONDER;
                    }
                    else
                    {
                        m_timer2 -= Time.deltaTime;
                    }
                }
                break;
        }     
    }
    private void Patrole()
    {
        if (m_currentPatrolePos.x == 0 && m_currentPatrolePos.y == 0) m_currentPatrolePos = m_patrolPos1;
        if (m_currentPath.Count == 0) MoveToWorldPos(m_currentPatrolePos);
        if (Vector2.Distance(transform.position, m_currentPatrolePos) <= 0.4f) SwapPatrolePoints();
        FollowPath();
    }

    private void SwapPatrolePoints()
    {
        if (m_timer3 <= 0)
        {
            if (m_currentPatrolePos == m_patrolPos1)
            {
                m_currentPatrolePos = m_patrolPos2;
            }
            else
            {
                m_currentPatrolePos = m_patrolPos1;
            }
            m_timer3 = m_deleyBetweenPatrol;
        }
        else
        {
            m_timer3 -= Time.deltaTime;
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

                if (m_timer <= 0)
                {
                    GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.Euler(new Vector3(dir.x, dir.y, 0)));
                    projectile.GetComponent<BulletMovment_SebastianMol>().direction = (m_playerTransform.position - transform.position).normalized;
                    m_timer = m_shootDeley;
                }
                else
                {
                    m_timer -= Time.deltaTime;
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
