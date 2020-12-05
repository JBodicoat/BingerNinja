//sebastian mol 14/11/2020 class created
//sebastian mol 29/11/2020 finished and commeneted all logic for final boss

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
    public GameObject m_tadashiNormalPorjectile;

    private int m_RandChanceAttackTadashi;
    private float m_RandChanceAttackTadashiFloat;
    private bool m_generateRandomNumberOnceTadashi = false;
    private GameObject m_currentProjectile = null;
    private int m_tadashiMultiShotCounter = 0;
    private int m_tadashiMultiShotCounterMax = 3;
    private float m_tadashiLastFaseAttackRand;
    private float m_attackRangeeAfterCharge;


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
            if(m_tadashiPhase == 1)
            {
                if (m_RandChanceAttackTadashi == 0)
                {
                    TadashiChargeAttack();
                }
                else
                {
                    TadashiQuickAttack();
                    //TODO cool down
                }
            }
            else if(m_tadashiPhase == 2)//health above 30 change this later
            {
                GameObject[] allLights = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_lightTag);
                foreach (var light in allLights)
                {
                    light.SetActive(false);
                    //add effect for light goign off or on or whatever
                }

                if (m_RandChanceAttackTadashiFloat > 0.3f)
                {
                    TadashiCrazyRangeAttack();
                }
                else
                {
                    TadashiTripleShot();
                }
            }
            else if (m_tadashiPhase == 3)//health below 30
            {
                //destroy all plants on this lvl
                GameObject[] allPlants = GameObject.FindGameObjectsWithTag(Tags_JoaoBeijinho.m_plant);
                foreach (var plant in allPlants)
                {
                    plant.SetActive(false);
                    //do an effect for plants to dissapoear
                }

                if (m_tadashiLastFaseAttackRand < 0.33f)
                {
                   TadashiQuickAttack();
                }
                else if (m_tadashiLastFaseAttackRand < 0.66f)
                {
                    TadashiChargeAttack();
                }
                else if (m_tadashiLastFaseAttackRand >= 0.66f)
                {
                    TadashiCrazyRangeAttack();
                }
            }
            
            
        }
    }

    /// <summary>
    /// logic for tadashi tripple shot attack
    /// </summary>
    private void TadashiTripleShot()
    {
        if (m_tadashiMultiShotCounter < m_tadashiMultiShotCounterMax)
        {
            m_ProjectileDisplay.sprite = null;
            m_currentProjectile = m_tadashiNormalPorjectile;
            if (EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer,
                ref m_attackTimer, m_currentProjectile, 0.3f))
            {
                m_tadashiMultiShotCounter++;
                m_attackTimer = 0.1f;
            }

        }
        else
        {
            m_tadashiMultiShotCounter = 0;
            m_attackTimer = m_shootDeley;
            m_generateRandomNumberOnceTadashi = false;
            m_currentProjectile = null;
        }
    }
    /// <summary>
    /// logic for tadashi normal attack
    /// </summary>
    private void TadashiQuickAttack()
    {
        if (EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility,
                   QuickAttack, ChargeAttack, StunAfterAttack,
                   m_currentEnemyType, m_hitSpeed))
        {
            m_generateRandomNumberOnceTadashi = false;
        }
    }
    /// <summary>
    /// logic for tadashi charg attack
    /// </summary>       
    private void TadashiChargeAttack()
    {
        if (EnemyAttacks_SebastianMol.ChargeAttack(m_playerTransform, ref m_attackTimer,
                   m_attackCollider, m_hitSpeed, gameObject, m_chargeAttackSpeed))
            m_generateRandomNumberOnceTadashi = false; //make this ibnto a public variable
    }
    /// <summary>
    /// logic for tadashi ranged attack that uses multiple projectiles
    /// </summary>
    private void TadashiCrazyRangeAttack()
    {
        if (m_currentProjectile)
        {
            if (EnemyAttacks_SebastianMol.RangedAttack(m_playerTransform, transform, m_aimer,
                             ref m_attackTimer, m_currentProjectile, m_shootDeley))
            {
                m_generateRandomNumberOnceTadashi = false;
                m_currentProjectile = null;
            }
        }
        
    }

    /// <summary>
    /// set up for tadashi quick attack
    /// </summary>
    private void TadashiQuickAttackSetUp()
    {    
        m_attackRange = m_meleeAttackRangeTadashi;
        m_attackCollider = m_normalAttackColider;
        if(m_chargeAttackColider) m_chargeAttackColider.SetActive(false);
        m_ProjectileDisplay.sprite = null;
        m_generateRandomNumberOnceTadashi = true;
        m_attackTimer = m_hitSpeed;
        m_attackRangeeAfterCharge = m_meleeAttackRangeTadashi;
    }
    /// <summary>
    /// set up for tadashi charged attack
    /// </summary>
    private void TadashiChargeAttackSetUp()
    {
        m_attackRange = m_chargeAttackRangeTadashi;
        m_attackCollider = m_chargeAttackColider;
        if (m_normalAttackColider) m_normalAttackColider.SetActive(false);
        m_ProjectileDisplay.sprite = null;
        m_generateRandomNumberOnceTadashi = true;
        m_attackTimer = m_chargeAttackDeley;
        m_attackRangeeAfterCharge = m_chargeAttackRangeTadashi;
    }
    /// <summary>
    /// set up for tadashi ranged attack with multiple projectiles
    /// </summary>
    private void TadashiCrazyRangeAttackSetUp()
    {
        m_attackRange = m_rangedAttackRange;
        m_attackCollider = m_normalAttackColider;
        m_RandChanceAttackTadashiFloat = Random.Range(0.0f, 1.0f);
        m_generateRandomNumberOnceTadashi = true;
        m_attackTimer = m_shootDeley;
        m_attackRangeeAfterCharge = m_rangedAttackRange;

        if (!m_currentProjectile)
        {
            int rand = Random.Range(0, 4);
            Debug.Log(rand + "shooting type");
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

        }
        
    }

    /// <summary>
    /// handles the set up for each fase of tadashi boss fight
    /// </summary>
    private void UpdateTadashi()
    {    if(m_currentEnemyType == m_enemyType.TADASHI)
        switch (m_tadashiPhase)
        {
            case 1:
                if (!m_generateRandomNumberOnceTadashi)
                {
                    m_RandChanceAttackTadashi = Random.Range(0, 2);
                    m_generateRandomNumberOnceTadashi = true;
                    if (m_RandChanceAttackTadashi == 0)
                    {
                        TadashiChargeAttackSetUp();
                        Debug.Log("C");
                    }
                    else
                    {
                        TadashiQuickAttackSetUp();
                        Debug.Log("Q");
                    }
                }
                break;

            case 2:
                if (!m_generateRandomNumberOnceTadashi)
                {
                    TadashiCrazyRangeAttackSetUp();
                }
                break;

            case 3:
              if(!m_generateRandomNumberOnceTadashi)
              {
                    m_currentProjectile = null;
                    m_ProjectileDisplay.sprite = null;
                    m_tadashiLastFaseAttackRand = Random.Range(0.0f, 1.0f);

                    if (m_tadashiLastFaseAttackRand < 0.33f)
                    {
                        TadashiQuickAttackSetUp();
                        Debug.Log("Q");

                    }
                    else if (m_tadashiLastFaseAttackRand < 0.66f)
                    {
                        TadashiChargeAttackSetUp();
                        Debug.Log("C");
                    }
                    else if (m_tadashiLastFaseAttackRand > 0.66f)
                    {
                        TadashiCrazyRangeAttackSetUp();
                        Debug.Log("R");
                    }

              }
               break;
        }


        WalkAwayFromWallBasedOnRange(m_attackRangeeAfterCharge);

    }

    /// <summary>
    /// used to walk away from all after charge attack so tadashi dosent get stuck
    /// </summary>
    /// <param name="range"></param>
    private void WalkAwayFromWallBasedOnRange(float range)
    {
        if (!m_isStuned)
            if (m_doMoveAwayFromWallOnce)
                StartCoroutine(MoveAwayFromeWall(m_amountOfTimeToMoveAwayFromWall, range));
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

            if (!m_isStuned)
                if (m_doMoveAwayFromWallOnce)
                {
                    StartCoroutine(MoveAwayFromeWall(m_amountOfTimeToMoveAwayFromWall));
                }
        }
        
    }

    private void LateUpdate()
    {
        UpdateAttackAlien();
        UpdateTadashi();      
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }
}
