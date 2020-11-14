using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    internal override void AttackBehaviour()
    {
        int rand = Random.Range(0, m_RangedAttackRandomChance+1);
        if(rand == m_RangedAttackRandomChance)
        {
            EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer, ref m_attackTimer, m_projectile, m_shootDeley);
        }
        else
        {
            EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack, 
                                                    ChargeAttack, StunIfTiger, m_petTigerDeley, m_currentEnemyType, m_hitSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
