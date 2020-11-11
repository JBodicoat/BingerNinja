//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions
//sebastian mol 09/11/20 chrage attack fixed 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// melee enemy class esed by any enemy that has melle attack
/// </summary>
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{
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
    [Tooltip("should the nemey patrole")]
    public bool m_dosePatrole;
    [Tooltip("dose the enemy have a harge attack")]
    public bool m_hasChargeAttack = false;
    [Tooltip("the deley befor chareg attack is carried out")]
    public float m_chargeAttackDeley;
    [Tooltip("possiility of a charge attack (1/m_chargAttackPosibility)")]
    public int m_chargAttackPosibility;
    [Tooltip("the amaount the charge attack is multiplied by")]
    public float m_chargeAttackMultiplier = 3;
    [Tooltip("the amount of time teh pet tigre is frozen for after it dose its attack")]
    public float m_petTigerDeley;


    /// <summary>
    /// funtionality for the melee attack
    /// </summary>
    private void MeleeAttack()
    {
        if (m_attackTimer <= 0)
        {
            if(m_hasChargeAttack)
            {
                int rand = Random.Range(0, m_chargAttackPosibility);
                switch (rand)
                {
                    case 0:
                        StartCoroutine(QuickAttack());
                        break;

                    case 1:
                        StartCoroutine(ChargeAttack());
                        break;
                }
            }
            else
            {
                StartCoroutine(QuickAttack());
            }            

            if(m_currentEnemyType == m_enemyType.PETTIGER)
            {
                StunEnemyWithDeley(m_petTigerDeley);
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
        yield return new WaitForSeconds(m_chargeAttackDeley);
        EnemyDamager_SebastianMol dameger = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();
        dameger.m_damage = dameger.m_baseDamage * m_chargeAttackMultiplier;
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

    internal override void WonderState()
    {
        if(m_dosePatrole)
        {
            if (m_playerDetected) m_currentState = state.CHASE;
            Patrol();
        }
        else
        {
            m_detectionCollider.enabled = true;
            if (transform.position != m_startPos)
            {
                PathfindTo(m_startPos);
            }
            if (transform.localScale.x != m_scale) transform.localScale
                    = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);
        }

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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_meleeRange);
    }
}
