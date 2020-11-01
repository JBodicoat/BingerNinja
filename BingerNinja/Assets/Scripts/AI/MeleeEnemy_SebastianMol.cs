//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// melee enemy class esed by any enemy that has melle attack
/// </summary>
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    [Header("desighner variables")]
    [Tooltip("how fast the how far away can the enemy be befor attacking")]
    public float m_meleeRange;
    [Tooltip("speed of the enemies attack")]
    public float m_hitSpeed;
    [Tooltip("object used to damage the enemy coudl be called the enemy weapon")]
    public GameObject m_attackCollider;
    [Tooltip("time it takess for the attck gameobject to be turneed off this shoudl be realy short")]
    public float attackDeactivationSpeed;
    [Tooltip("deley between line of sight checks")]
    public float m_outOfSightDeley;
    internal override void EnemyBehaviour()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                m_detectionCollider.enabled = true;
                if(transform.position != m_startPos)
                {
                    PathfindTo(m_startPos);
                }
                if (transform.localScale.x != m_scale) transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);

                break;

            case state.CHASE:
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

    /// <summary>
    /// funtionality for the melee attack
    /// </summary>
    private void MeleeAttack()
    {
        if (m_attackTimer <= 0)
        {

            StartCoroutine(QuickAttack());
            m_attackTimer = m_hitSpeed;
        }
        else
        {
            m_attackTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// activates the "enemy weapon" object that damages the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator QuickAttack()
    {
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

}
