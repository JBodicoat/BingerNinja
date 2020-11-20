//sebastian mol 14/11/2020 class created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for enemies with ranegd and melee attcks
/// </summary>
 class RangedAndMeleeEnemy_SebastianMol :  MeleeEnemy_SebastianMol
 {
    [Header("projectile prefabs")]
    public GameObject m_aimer;
    public GameObject m_projectile;
    [Header("projectile Variables")]
    [Tooltip("how fast the projectile moves")]
    public float m_shootDeley;
    [Tooltip("speed of the projectile")]
    public float m_projectileSpeed;
    [Tooltip("random chance of the enemy doing a ranged attack its is 1/ m_RangedAttackRandomChance")]
    public int m_RangedAttackRandomChance;
    [Header("special variablees for ranged/melee class")]
    [Tooltip("the attack distance on ranged attack")]
    public float m_rangedAttackRange = 3;
    [Tooltip("the attack distance on melee attack")]
    public float m_meleeAttackRange = 1;


    private int m_randomChanceOfRangedAttack;
    private bool m_generateRandomNumberOnce = false;
    private int m_randAttackChance;

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {             
        if(m_randAttackChance == m_RangedAttackRandomChance-1)
        {
            if(EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer, ref m_attackTimer, m_projectile, m_shootDeley))  m_generateRandomNumberOnce = false;

        }
        else
        {
           if( EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack, ChargeAttack, StunIfTiger, m_petTigerDeley, m_currentEnemyType, m_hitSpeed)) m_generateRandomNumberOnce = false;
        }
    }

    /// <summary>
    /// updates teh attack ranged based on what attack is coming up e.g. more range for ranged attacks
    /// </summary>
    private void UpdateAttack()
    {
        if (!m_generateRandomNumberOnce)
        {
            m_randAttackChance = Random.Range(0, m_RangedAttackRandomChance);
            m_generateRandomNumberOnce = true;
        }

        if (m_randAttackChance == m_RangedAttackRandomChance - 1)
        {
            m_attackRange = m_rangedAttackRange;
        }
        else
        {
            m_attackRange = m_meleeAttackRange;
        }
    }

    private void LateUpdate()
    {
        UpdateAttack();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
