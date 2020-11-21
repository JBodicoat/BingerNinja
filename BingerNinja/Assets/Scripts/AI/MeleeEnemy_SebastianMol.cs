//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions
//sebastian mol 09/11/20 chrage attack fixed 
//sebastian mol 20/11/2020 spce ninja enemy logic done

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// melee enemy class esed by any enemy that has melle attack
/// </summary>
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{
    [Header("class based damage variables")]
    [Tooltip("speed of the enemies attack")]
    public float m_hitSpeed;
    [Tooltip("object used to damage the enemy coudl be called the enemy weapon")]
    public GameObject m_attackCollider;
    [Tooltip("time it takess for the attck gameobject to be turneed off this shoudl be realy short")]
    public float attackDeactivationSpeed;
    [Tooltip("dose the enemy have a harge attack")]
    public bool m_hasChargeAttack = false;
    [Tooltip("the deley befor chareg attack is carried out")]
    public float m_chargeAttackDeley;
    [Tooltip("possiility of a charge attack (1/m_chargAttackPosibility)")]
    public int m_chargAttackPosibility;
    [Tooltip("the amaount the charge attack is multiplied by")]
    public float m_chargeAttackMultiplier = 3;
    [Tooltip("the amount of time the pet tigre is frozen for after it dose its attack")]
    public float m_petTigerDeley;
    [Tooltip("speed of the enemies attack")]
    public float m_maxChargTimeBeforDamege = 0.2f;


    private bool m_doStunOnce = false;


    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses quick attack
    /// </summary>
    /// <returns></returns>
    protected IEnumerator QuickAttackCo()
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
    protected IEnumerator ChargeAttackCo()
    {
        yield return new WaitForSeconds(m_chargeAttackDeley);
        EnemyDamager_SebastianMol dameger = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();
        dameger.m_damage = dameger.m_baseDamage * m_chargeAttackMultiplier;
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

    protected void QuickAttack()
    {
        StartCoroutine(QuickAttackCo());
    }

    protected void ChargeAttack()
    {
        StartCoroutine(ChargeAttackCo());
    }

    protected void StunIfTiger()
    {
        StunEnemyWithDeleyFunc(m_petTigerDeley);
    }

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {
        if(m_currentEnemyType == m_enemyType.PETTIGER)
        {
            EnemyAttacks_SebastianMol.ChargeAttack(m_playerTransform, ref m_attackTimer, 
                m_attackCollider, m_hitSpeed, gameObject, 500); //make this ibnto a public variable
        }
        else
        {
            EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack,
                                               ChargeAttack, StunIfTiger, m_petTigerDeley, m_currentEnemyType, m_hitSpeed);
        }
       
    }

    private void LateUpdate()
    {
        if(m_currentEnemyType == m_enemyType.SPACENINJABOSS)
        {
            if ((m_health / m_maxHealth) > m_secondPhaseStartPercentage)
                if (GameObject.FindObjectOfType<PlayerStealth_JoaoBeijinho>().IsStealthed()) //confusuion when player stelths
                {
                    if (!m_doStunOnce)
                    {
                        StunEnemyWithDeleyFunc(m_amountOfStunWhenPlayerStealthed);
                        m_doStunOnce = true;
                    }
                }
                else
                {
                    m_doStunOnce = false;
                }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_currentEnemyType == m_enemyType.PETTIGER)
        {

            Rigidbody2D rijy = GetComponent<Rigidbody2D>();
            if (rijy.bodyType == RigidbodyType2D.Dynamic)
            {

                rijy.bodyType = RigidbodyType2D.Kinematic;
                rijy.velocity = Vector2.zero;
                m_attackCollider.SetActive(false);
                //if hit wall walk away one tile 
                //if hit wall stunn
                if (collision.gameObject.name == "Walls1_map")
                {
                    
                    StunEnemyWithDeleyFunc(m_petTigerDeley);
                    Debug.Log("stun tigers boom");
                }
            }

            
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
