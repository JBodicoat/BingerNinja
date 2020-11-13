﻿//sebastian mol
//sebastian mol 30/10/20 melee enemy shoudl be completed
//sebastian mol 02/11/20 removed player behaviour switch replaced it with abstract functions
//sebastian mol 09/11/20 chrage attack fixed 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// melee enemy class esed by any enemy that has melle attack
/// </summary>
class MeleeEnemy_SebastianMol : BaseEnemy_SebastianMol
{
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

    private EnemyAttacks_SebastianMol m_attacks;

    /// <summary>
    /// activates the "enemy weapon" object that damages the player uses quick attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator QuickAttackCo()
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
    private IEnumerator ChargeAttackCo()
    {
        yield return new WaitForSeconds(m_chargeAttackDeley);
        EnemyDamager_SebastianMol dameger = m_attackCollider.GetComponent<EnemyDamager_SebastianMol>();
        dameger.m_damage = dameger.m_baseDamage * m_chargeAttackMultiplier;
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(attackDeactivationSpeed);
        m_attackCollider.SetActive(false);
    }

    private void QuickAttack()
    {
        StartCoroutine(QuickAttackCo());
    }

    private void ChargeAttack()
    {
        StartCoroutine(ChargeAttackCo());
    }

    private void StunIfTiger()
    {
        StunEnemyWithDeleyFunc(m_petTigerDeley);
    }

    internal override void AttackBehaviour()
    {
        m_attacks.MelleAttack(m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack, ChargeAttack, StunIfTiger, m_petTigerDeley, m_currentEnemyType, m_hitSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attckRange);
    }
}
