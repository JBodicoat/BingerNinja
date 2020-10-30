using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sebastian mol
//melee enemy class can aslo be used for brute enemy
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    public float m_meleeRange;
    public float m_hitSpeed;
    public GameObject m_attackCollider;
    public float attackDeactivationSpeed;
    public float m_outOfSightDeley;
    private float m_timer;
    private float m_timer2;
    internal override void EnemyBehaviour()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                //if (m_playerDetected) m_currentState = state.CHASE;
                m_detectionCollider.enabled = true;
                if(transform.position != m_startPos)
                {
                    PathfindTo(m_startPos);
                }
                if (transform.localScale.x != m_scale) transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);

                break;

            case state.CHASE:
                //move towards the player if he has been detected
                if (IsPlayerInLineOfSight())
                {
                    if (Vector2.Distance(transform.position, m_playerTransform.position) < m_meleeRange)
                    {
                        ClearPath(false);
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
                if (IsPlayerInLineOfSight())
                {
                    if (Vector2.Distance(transform.position, m_playerTransform.position) < m_meleeRange)
                    {
                        MeleeAttack();
                    }
                    else
                    {
                        m_currentState = state.CHASE;
                    }
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

    private void MeleeAttack()
    {
        if (m_timer <= 0)
        {

            StartCoroutine(QuickAttack());
            m_timer = m_hitSpeed;
        }
        else
        {
            m_timer -= Time.deltaTime;
        }
    }

    private IEnumerator QuickAttack()
    {
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

}
