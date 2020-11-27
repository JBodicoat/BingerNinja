//sebastian mol 14/11/2020 class created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for enemies with ranegd and melee attcks
/// </summary>
 class RangedAndMeleeEnemy_SebastianMol :  MeleeEnemy_SebastianMol
 {
    [Header("class based damage variables")]
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

    [Header("special variablees for Alien")]
    [Tooltip("the attack distance on ranged attack")]
    public float m_rangedAttackRange = 3;
    [Tooltip("the attack distance on melee attack")]
    public float m_meleeAttackRange = 1;


    private int m_randomChanceOfRangedAttack;
    private bool m_generateRandomNumberOnce = false;
    private int m_randAttackChance;

    [Header("special variablees for Tadashi")]
    [Tooltip("the attack distance on ranged attack")]
    public float m_chargeAttackRangeTadashi = 10;
    [Tooltip("the attack distance on ranged attack")]
    public float m_rangedAttackRangeTadashi = 3;
    [Tooltip("the attack distance on melee attack")]
    public float m_meleeAttackRangeTadashi = 1;

    private int RandChanceAttackTadashiPhaseOne;
    private bool m_generateRandomNumberOnceTadashi = false;


    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {             
        
        if(m_currentEnemyType == m_enemyType.ALIEN)
        {
            if (m_randAttackChance == m_RangedAttackRandomChance - 1)
            {
                if (EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer, 
                    ref m_attackTimer, m_projectile, m_shootDeley)) m_generateRandomNumberOnce = false;

            }
            else
            {
                if (EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility, 
                    QuickAttack, ChargeAttack, StunAfterAttack,
                    m_currentEnemyType, m_hitSpeed)) m_generateRandomNumberOnce = false;
            }
        }

        if (m_currentEnemyType == m_enemyType.TADASHI)
        {
            //health above 60
            float healthPercentage = m_health / m_maxHealth;
            if(healthPercentage > 0.6f)
            {
                if(RandChanceAttackTadashiPhaseOne == 0)
                {
                    if(EnemyAttacks_SebastianMol.ChargeAttack(m_playerTransform, ref m_attackTimer,
                       m_attackCollider, m_hitSpeed, gameObject, m_chargeAttackSpeed)) m_generateRandomNumberOnceTadashi = false; //make this ibnto a public variable
                }
                else
                {
                    if(EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility,
                    QuickAttack, ChargeAttack, StunAfterAttack,
                    m_currentEnemyType, m_hitSpeed)) m_generateRandomNumberOnceTadashi = false;
                }
            }
            else if(healthPercentage > 0.3f)//health above 30
            {
                //break all light here
                //implement the raneg mechinct SOME NEXT LVL SHIT
                //tripple shot range mechanic
                //70 30 split
            }
            else//health below 30
            {
                //destroy all plants on this lvl
                //crazy raneg attack from fase two
                //quick hit no cooldown
                //change attack no stun
                //30/30/30 split
            }
            
            
        }


    }
    private void LateUpdate()
    {
        UpdateAttackAlien();

       

        if (!m_generateRandomNumberOnceTadashi)
        {
            RandChanceAttackTadashiPhaseOne = Random.Range(0, 2);
            Debug.Log(RandChanceAttackTadashiPhaseOne);
            m_generateRandomNumberOnceTadashi = true;
        }


        if (RandChanceAttackTadashiPhaseOne == 0)
        {
            m_attackRange = m_chargeAttackRangeTadashi;
        }
        else
        {
            m_attackRange = m_meleeAttackRangeTadashi;
        }


        if (!m_isStuned)
            if (m_doMoveAwayFromWallOnce)
            {
                StartCoroutine(MoveAwayFromeWall(m_amountOfTimeToMoveAwayFromWall));
            }
    }

    /// <summary>
    /// updates teh attack ranged based on what attack is coming up e.g. more range for ranged attacks
    /// </summary>
    private void UpdateAttackAlien()
    {
        if(m_currentEnemyType == m_enemyType.ALIEN)
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
        
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
