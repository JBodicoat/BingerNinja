﻿//sebastian mol 14/11/2020 class created
//sebastian mol 29/11/2020 finished and commeneted all logic for final boss
// louie        11/12/2020 Attack animation
using UnityEngine;

/// <summary>
/// class for enemies with ranegd and melee attcks
/// </summary>
class RangedAndMeleeEnemy_SebastianMol :  MeleeEnemy_SebastianMol
 {

    public GameObject m_aimer;
    public GameObject m_projectile;

    public float m_shootDeley;

    public float m_projectileSpeed;

    public int m_RangedAttackRandomChance;

    public float m_rangedAttackRange = 3;

    public float m_meleeAttackRange = 1;
    private int m_randomChanceOfRangedAttack;
    private bool m_generateRandomNumberOnce = false;
    private int m_randAttackChance;

    public float m_chargeAttackRangeTadashi = 10;
    public float m_rangedAttackRangeTadashi = 3;
    public float m_meleeAttackRangeTadashi = 1;

    public GameObject m_normalAttackColider;
    public GameObject m_chargeAttackColider;
    public GameObject[] m_tadashiProjectiles;
    public SpriteRenderer m_ProjectileDisplay;
    public GameObject m_tadashiNormalPorjectile;

    private int q;
    private float w;
    private bool e = false;
    private GameObject r = null;
    private int t = 0;
    private int y = 3;
    private float u;
    private float i;


    //i have to put this here i cba to find a cleaner way to do this future seb get your shit togther


    /// <summary>
    /// ovveride class that holds logic for what the enemy shoudl do when in the attack state
    /// </summary>
    internal override void AttackBehaviour()
    {                    
        if(m_currentEnemyType == m_enemyType.ALIEN || m_currentEnemyType == m_enemyType.NORMAL)
        {
            if (m_randAttackChance == m_RangedAttackRandomChance - 1)
            {
                if (EnemyAttacks_SebastianMol.RangedAttack(GetComponent<Enemy_Animation_LouieWilliamson>(), m_playerTransform, transform, m_aimer,
                    ref m_attackTimer, m_projectile, m_shootDeley))
                {
                    m_generateRandomNumberOnce = false;
                }
            }
            else
            {
                if (EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility,
                    QuickAttack, ChargeAttack, StunAfterAttack,
                    m_currentEnemyType, m_hitSpeed, GetComponent<Enemy_Animation_LouieWilliamson>()))
                {
                    m_generateRandomNumberOnce = false;
                }
            }
        }

        if (m_currentEnemyType == m_enemyType.TADASHI)
        {
            //health above 60
            if(m_tadashiPhase == 1)
            {
                if (q == 0)
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

                if (w > 0.3f)
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

                if (u < 0.33f)
                {
                   TadashiQuickAttack();
                }
                else if (u < 0.66f)
                {
                    TadashiChargeAttack();
                }
                else if (u >= 0.66f)
                {
                    TadashiCrazyRangeAttack();
                }
            }
            
            
        }
    }
    /// <summary>
    /// logic for tadashi tripple shot attack
    /// </summary>
    void TadashiTripleShot()
    {
        if (t < y)
        {
            m_ProjectileDisplay.sprite = null;
            r = m_tadashiNormalPorjectile;
            if (EnemyAttacks_SebastianMol.RangedAttack(GetComponent<Enemy_Animation_LouieWilliamson>(), m_playerTransform, transform, m_aimer,
                ref m_attackTimer, r, 0.3f))
            {
                t++;
                m_attackTimer = 0.1f;
            }

        }
        else
        {
            t = 0;
            m_attackTimer = m_shootDeley;
            e = false;
            r = null;
        }
    }
    /// <summary>
    /// logic for tadashi normal attack
    /// </summary>
    void TadashiQuickAttack()
    {
        if (EnemyAttacks_SebastianMol.MelleAttack(ref m_attackTimer, m_hasChargeAttack, m_chargAttackPosibility,
                   QuickAttack, ChargeAttack, StunAfterAttack,
                   m_currentEnemyType, m_hitSpeed, GetComponent<Enemy_Animation_LouieWilliamson>()))
        {
            e = false;
        }
    }
    /// <summary>
    /// logic for tadashi charg attack
    /// </summary>       
    void TadashiChargeAttack()
    {
        if (EnemyAttacks_SebastianMol.ChargeAttack(m_playerTransform, ref m_attackTimer,
                   m_attackCollider, m_hitSpeed, gameObject, m_chargeAttackSpeed))
            e = false; //make this ibnto a public variable
    }
    /// <summary>
    /// logic for tadashi ranged attack that uses multiple projectiles
    /// </summary>
    void TadashiCrazyRangeAttack()
    {
        if (r)
        {
            if (EnemyAttacks_SebastianMol.RangedAttack(GetComponent<Enemy_Animation_LouieWilliamson>(), m_playerTransform, transform, m_aimer,
                             ref m_attackTimer, r, m_shootDeley))
            {
                e = false;
                r = null;
            }
        }
        
    }

    /// <summary>
    /// set up for tadashi quick attack
    /// </summary>
    void TadashiQuickAttackSetUp()
    {    
        m_attackRange = m_meleeAttackRangeTadashi;
        m_attackCollider = m_normalAttackColider;
        if(m_chargeAttackColider) m_chargeAttackColider.SetActive(false);
        m_ProjectileDisplay.sprite = null;
        e = true;
        m_attackTimer = m_hitSpeed;
        i = m_meleeAttackRangeTadashi;
    }
    /// <summary>
    /// set up for tadashi charged attack
    /// </summary>
    void TadashiChargeAttackSetUp()
    {
        m_attackRange = m_chargeAttackRangeTadashi;
        m_attackCollider = m_chargeAttackColider;
        if (m_normalAttackColider) m_normalAttackColider.SetActive(false);
        m_ProjectileDisplay.sprite = null;
        e = true;
        m_attackTimer = m_chargeAttackDeley;
        i = m_chargeAttackRangeTadashi;
    }
    /// <summary>
    /// set up for tadashi ranged attack with multiple projectiles
    /// </summary>
    void TadashiCrazyRangeAttackSetUp()
    {
        m_attackRange = m_rangedAttackRange;
        m_attackCollider = m_normalAttackColider;
        w = Random.Range(0.0f, 1.0f);
        e = true;
        m_attackTimer = m_shootDeley;
        i = m_rangedAttackRange;

        if (!r)
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

            r = m_projectile;

        }
        
    }

    /// <summary>
    /// handles the set up for each fase of tadashi boss fight
    /// </summary>
   void UpdateTadashi()
    {    if(m_currentEnemyType == m_enemyType.TADASHI)
        switch (m_tadashiPhase)
        {
            case 1:
                if (!e)
                {
                    q = Random.Range(0, 2);
                    e = true;
                    if (q == 0)
                    {
                        TadashiChargeAttackSetUp();
                  
                    }
                    else
                    {
                        TadashiQuickAttackSetUp();
              
                    }
                }
                break;

            case 2:
                if (!e)
                {
                    TadashiCrazyRangeAttackSetUp();
                }
                break;

            case 3:
              if(!e)
              {
                    r = null;
                    m_ProjectileDisplay.sprite = null;
                    u = Random.Range(0.0f, 1.0f);

                    if (u < 0.33f)
                    {
                        TadashiQuickAttackSetUp();
           

                    }
                    else if (u < 0.66f)
                    {
                        TadashiChargeAttackSetUp();
                
                    }
                    else if (u > 0.66f)
                    {
                        TadashiCrazyRangeAttackSetUp();
                   
                    }

              }
               break;
        }


        WalkAwayFromWallBasedOnRange(i);

    }

    /// <summary>
    /// used to walk away from all after charge attack so tadashi dosent get stuck
    /// </summary>
    /// <param name="a"></param>
     void WalkAwayFromWallBasedOnRange(float a)
    {
        if (!m_isStuned)
            if (m_doMoveAwayFromWallOnce)
                StartCoroutine(MoveAwayFromeWall(m_amountOfTimeToMoveAwayFromWall, a));
    }


    /// <summary>
    /// updates teh attack ranged based on what attack is coming up e.g. more range for ranged attacks
    /// </summary>
    void UpdateAttackAlien()
    {
        if(m_currentEnemyType == m_enemyType.ALIEN || m_currentEnemyType == m_enemyType.NORMAL)
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

    void LateUpdate()
    {
        UpdateAttackAlien();
        UpdateTadashi();      
    }
}
