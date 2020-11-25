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

    public GameObject m_normalAttackColider;
    public GameObject m_chargeAttackColider;

    public GameObject[] m_tadashiProjectiles;

    public SpriteRenderer m_ProjectileDisplay;


    private int RandChanceAttackTadashi;
    private float RandChanceAttackTadashiFloat;

    private bool m_generateRandomNumberOnceTadashi = false;
    private GameObject m_currentProjectile = null;


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
                if (RandChanceAttackTadashi == 0)
                {
                    if (EnemyAttacks_SebastianMol.ChargeAttack(m_playerTransform, ref m_attackTimer,
                    m_attackCollider, m_hitSpeed, gameObject, m_chargeAttackSpeed)) m_generateRandomNumberOnceTadashi = false; //make this ibnto a public variable
                    Debug.Log("charge attack");
                }
                else
                {
                    if (EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility,
                    QuickAttack, ChargeAttack, StunAfterAttack,
                    m_currentEnemyType, m_hitSpeed)) m_generateRandomNumberOnceTadashi = false;
                    Debug.Log("melee attack");
                }  
            }
            else if(healthPercentage < 0.6f)//health above 30 change this later
            {
                if (EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer,
                        ref m_attackTimer, m_currentProjectile, m_shootDeley))
                {
                    m_currentProjectile = null;
                    m_generateRandomNumberOnceTadashi = false;
                    m_attackTimer = m_shootDeley;
                    m_ProjectileDisplay.sprite = null;
                }
                Debug.Log("raneg attack");

                //break all light here
                //if(RandChanceAttackTadashiFloat > 0.3f)
                //{
                //    if (EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer,
                //        ref m_attackTimer, m_currentProjectile, m_shootDeley, 4, 4))
                //    {
                //        m_currentProjectile = null;
                //        m_generateRandomNumberOnceTadashi = false;
                //        m_attackTimer = m_shootDeley;
                //    }
                //    Debug.Log("raneg attack");


                //}
                //else
                //{
                //   
                //}

                //implement the raneg mechinct SOME NEXT LVL SHIT
                //tripple shot range mechanic
                //70 30 split
            }
            else//health below 30
            {
                //destroy all plants on this lvl
                GameObject[] allPlants =  GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_plant);
                foreach (var plant in allPlants)
                {
                    plant.SetActive(false);
                    //do an effect for plants to dissapoear
                }
                //crazy raneg attack from fase two
                //quick hit no cooldown
                //change attack no stun
                //30/30/30 split
            }
            
            
        }


    }

    private void UpdateTadashi()
    {
        //phase one ========================================

        switch (m_tadashiPhase)
        {
            case 1:
                if (!m_generateRandomNumberOnceTadashi)
                {
                    RandChanceAttackTadashi = Random.Range(0, 2);
                    Debug.Log(RandChanceAttackTadashi);
                    m_generateRandomNumberOnceTadashi = true;
                }

                if (RandChanceAttackTadashi == 0)
                {
                    m_attackRange = m_chargeAttackRangeTadashi;
                    m_attackCollider = m_chargeAttackColider;
                    m_normalAttackColider.SetActive(false);
                }
                else
                {
                    m_attackRange = m_meleeAttackRangeTadashi;
                    m_attackCollider = m_normalAttackColider;
                    m_chargeAttackColider.SetActive(false);
                }
                break;

            case 2:
                m_attackRange = m_rangedAttackRange;
                m_attackCollider = m_normalAttackColider;

                if (!m_generateRandomNumberOnceTadashi)
                {
                    RandChanceAttackTadashiFloat = Random.Range(0.0f, 1.1f);
                    Debug.Log(RandChanceAttackTadashi);
                    m_generateRandomNumberOnceTadashi = true;
                    if (RandChanceAttackTadashiFloat <= 0.25f)
                    {
                        m_projectile = m_tadashiProjectiles[0];
                        Debug.Log("first");
                    }
                    else if (RandChanceAttackTadashiFloat > 0.25f)
                    {
                        m_projectile = m_tadashiProjectiles[1];
                        Debug.Log("second");
                    }
                    else if (RandChanceAttackTadashiFloat > 0.5f)
                    {
                        m_projectile = m_tadashiProjectiles[2];
                        Debug.Log("third");
                    }
                    else if (RandChanceAttackTadashiFloat > 0.75f)
                    {
                        m_projectile = m_tadashiProjectiles[3];
                        Debug.Log("fourth");
                    }
                }

                if (!m_currentProjectile)
                {
                    int rand = Random.Range(0, 4);

                    switch (rand)
                    {
                        case 0:
                            m_projectile = m_tadashiProjectiles[0];
                            break;

                        case 1:
                            m_projectile = m_tadashiProjectiles[1];
                            break;

                        case 2:
                            m_projectile = m_tadashiProjectiles[2];
                            break;

                        case 3:
                            m_projectile = m_tadashiProjectiles[3];
                            break;
                    }

                    m_ProjectileDisplay.sprite = m_projectile.GetComponent<SpriteRenderer>().sprite;
                    m_ProjectileDisplay.color = m_projectile.GetComponent<SpriteRenderer>().color; //delete thsi line when yi get the art for projectiles

                    m_currentProjectile = m_projectile;

                    //TODO make attack ranged differnet
                }

                break;

            case 3:
                break;
        }
        
        //pahse two ===============================================


    }

    private void LateUpdate()
    {
        UpdateAttackAlien();
        UpdateTadashi();


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
