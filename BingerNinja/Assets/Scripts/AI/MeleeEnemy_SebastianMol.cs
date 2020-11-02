//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions

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
    public bool m_hasChargeAttack = false;
    public float m_chargeAttackDeley;

    /// <summary>
    /// funtionality for the melee attack
    /// </summary>
    private void MeleeAttack()
    {
        if (m_attackTimer <= 0)
        {
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    StartCoroutine(QuickAttack());
                    break;
                
                case 1:
                    StartCoroutine(ChargeAttack());
                    break;
            }
           
            m_attackTimer = m_hitSpeed;
        }
        else
        {
            m_attackTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses quick attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator QuickAttack()
    {
        m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_damage
            = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_baseDamage;
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }
    
    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses charge attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChargeAttack()
    {
        new WaitForSeconds(m_chargeAttackDeley);
        m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_damage 
            = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>().m_baseDamage * 3;
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

    internal override void WonderState()
    {
        m_detectionCollider.enabled = true;
        if (transform.position != m_startPos)
        {
            PathfindTo(m_startPos);
        }
        if (transform.localScale.x != m_scale) transform.localScale
                = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);

    }

    internal override void ChaseState()
    {
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
    }

    internal override void AttackState()
    {
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
    }

    internal override void RetreatState()
    {
    }
}
