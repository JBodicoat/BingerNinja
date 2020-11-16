//sebastian mol 14/11/2020 class created

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

    private int m_randomChanceOfRangedAttack;
    private bool m_generateRandomNumberOnce = false;
    private int m_rand;

    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {             
        if(m_rand == m_RangedAttackRandomChance-1)
        {
            if(EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer, ref m_attackTimer, m_projectile, m_shootDeley))
            {
                m_generateRandomNumberOnce = false;
                Debug.Log("ranged");

            }

        }
        else
        {
           if( EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, QuickAttack, ChargeAttack, StunIfTiger, m_petTigerDeley, m_currentEnemyType, m_hitSpeed))
           {
                m_generateRandomNumberOnce = false;
                Debug.Log("melle");
           }
        }
    }

    private void LateUpdate()
    {
        if (!m_generateRandomNumberOnce)
        {
            m_rand = Random.Range(0, m_RangedAttackRandomChance);
            m_generateRandomNumberOnce = true;
        }

        if (m_rand == m_RangedAttackRandomChance - 1)
        {
            m_attackRange = 3;
        }
        else
        {
            m_attackRange = 0.6f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
