// Mário Fernandes


// Mário 17/10/2020 - Create class and Attack, PickUpFood, IIsHoldingFood Funcions
// Joao 25/10/2020 - Stop weapon usage while crouched in update
// Louie 02/11/2020 - added player attack animation code
// Jack 02/11/2020 - changed "CloseEnemy.GetComponent<EnemyAi>().Hit(m_currentWeapon.dmg);" to GetComponent<BaseEnemy_SebastianMol>().TakeDamage(m_currentWeapon.dmg);
//                   in Attack function
//                   added reference to PlayerHealthAndHunger script and extended the eat function so that it actually restores your hunger bar
//                   set m_currentWeapon to null after eating & added check when pickup up weapon so only 1 can be held
//                   changed GetComponent in above to GetComponentInParent to support new EnemyCollider child on enemy prefabs
//                   EnemyCollider child needed because otherwise projectiles collide with enemy view cone triggers
// Louie 03/11/2020 - Added Player Sound Effects
//sebastian mol 05/11/20 changed enemy take damage function call
// Mario 08/11/2020 - Update Effects
// Mario 09/11/2020 - Update Names and Add strength
// Mario 13/11/2020 - Add Distraction time to progectile
// Louie 17/11/2020 - Added Weapon UI integration

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum FoodType
{
    FUGU,
    SQUID,
    RICEBALL,
    KOBEBEEF,
    SASHIMI,
    TEMPURA,
    DANGO,
    SAKE,
    NOODLES,
} 

public enum WeaponType
{
    Melee,
    Ranged
}

///<summary>
/// This class stores the current weapon on the player and make im abel to use it 
///<summary>
public class PlayerCombat_MarioFernandes : MonoBehaviour
{
    private WeaponUI_LouieWilliamson m_WeaponUI;
    private PlayerAnimation_LouieWilliamson m_animationScript;
    public GameObject m_projectile = null;
    public float m_attackDelay = 1;
    public float m_timeSinceLastAttack = 0;
    //public Collider2D EnemyDetection = null;    
    public float m_meleeAttackRadius = 3;
    public float m_strenght = 1;
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    //This weapons are represented by an array of weapons of size 2
    // 0 - Melee weapon
    // 1 - Ranged weapon
    [SerializeField]
    protected WeaponsTemplate_MarioFernandes[] m_currentWeapon;

    public int m_weaponsIndex = 0; 
    PlayerHealthHunger_MarioFernandes m_playerHealthHungerScript;

    private AudioManager_LouieWilliamson m_audioManager;

    private PlayerController_JamieG Controller;

    private EffectManager_MarioFernandes m_effectManager;

  public  bool IsHoldingFood()
        {
       
        //Temp, should go the current weapon
        return false;
        }

    public void ResetStrength()
    {
        m_strenght = 1;
    }

    void Attack()
    {
        m_animationScript.TriggerAttackAnim();

        if(m_currentWeapon[m_weaponsIndex].IsRanged())
        {
            //TODO undo this comment
            //m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
            GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
            projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght);
            projectile.GetComponent<Projectile_MarioFernandes>().m_distractTime = m_currentWeapon[m_weaponsIndex].m_distractTime;

            if(m_currentWeapon[m_weaponsIndex].m_foodType == FoodType.DANGO)
            {
            GameObject projectile2 = Instantiate(m_projectile, transform.position, transform.rotation);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_distractTime = m_currentWeapon[m_weaponsIndex].m_distractTime;
            projectile2.GetComponent<Projectile_MarioFernandes>().m_speed /= 2;
            }
        }
        else
        {
            //TODO uncomment this
           // m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);

            float distanceToClosestsEnemy = Mathf.Infinity;
                GameObject CloseEnemy = null;
                GameObject[] allEnemys = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject CurrentEnemy in allEnemys)
                {
                    float distanceToEnemy = (CurrentEnemy.transform.position - this.transform.position).sqrMagnitude;
                    if(distanceToEnemy < distanceToClosestsEnemy)
                    {
                        distanceToClosestsEnemy = distanceToEnemy;
                        CloseEnemy = CurrentEnemy;
                    }
                }

                if(CloseEnemy && distanceToClosestsEnemy <= m_meleeAttackRadius)
                {
                    CloseEnemy.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage( m_damageType.MELEE , (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght));
                }                
            }

            //EnemyDetection.enabled = false;        
    }

    public void Eat()
    {
        m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Eating);

        if (m_currentWeapon[m_weaponsIndex])
        {
            GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon[m_weaponsIndex].m_instaHeal);

            switch (m_currentWeapon[m_weaponsIndex].m_foodType)
            {
                case FoodType.FUGU:                   
                    if (Random.Range(0, 101) >= 50)
                        m_effectManager.AddEffect(new PoisionDefuff_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_poisonDmg));                        
                    break;
                case FoodType.SQUID:                    
                    break;
                case FoodType.RICEBALL:                    
                    break;
                case FoodType.KOBEBEEF:                   
                    m_effectManager.AddEffect(new SpeedEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_speedModifier));
                    break;
                case FoodType.SASHIMI:                    
                    m_effectManager.AddEffect(new StrengthEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_strengthModifier));
                    break;
                case FoodType.TEMPURA:                   
                    break;
                case FoodType.DANGO:
                    break;
                case FoodType.SAKE:                    
                    break;
                case FoodType.NOODLES:
                    m_effectManager.AddEffect(new StrengthEffect_MarioFernandes(30, m_currentWeapon[m_weaponsIndex].m_strengthModifier));
                    break;
                default:
                    break;
            }

            m_playerHealthHungerScript.Eat(m_currentWeapon[m_weaponsIndex].m_hungerRestoreAmount);

            m_currentWeapon[m_weaponsIndex].enabled = false;
            m_currentWeapon[m_weaponsIndex] = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_currentWeapon = new WeaponsTemplate_MarioFernandes[2];
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        m_animationScript = GetComponent<PlayerAnimation_LouieWilliamson>();
        m_playerHealthHungerScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        m_audioManager = FindObjectOfType<AudioManager_LouieWilliamson>();
        Controller = GetComponent<PlayerController_JamieG>();
        m_WeaponUI = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
        m_effectManager = gameObject.GetComponent<EffectManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        // 0 - Melee weapon
        // 1 - Ranged weapon
         if(Controller.m_switchWeapons.triggered)
         {
             if(m_weaponsIndex == 1)
             m_weaponsIndex = 0;
             else
             m_weaponsIndex = 1;

             print(m_currentWeapon[m_weaponsIndex]);
         }

        if(m_timeSinceLastAttack < 0)
        {
              if (!m_playerStealthScript.m_crouched && m_currentWeapon[m_weaponsIndex] &&  Controller.m_attack.triggered)
            {                           
                m_timeSinceLastAttack = m_attackDelay;
                print("Attack");
                Attack();        
            }          
        }
        else
        m_timeSinceLastAttack -= Time.deltaTime;

        if(Controller.m_eat.triggered)
        {
            Eat();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(!m_currentWeapon[0] && collision.GetComponent<WeaponsTemplate_MarioFernandes>() && !collision.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            m_currentWeapon[0] = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
            m_WeaponUI.WeaponChange(m_currentWeapon[0].m_foodType, false, 0);
		}
        else if(!m_currentWeapon[1] && collision.GetComponent<WeaponsTemplate_MarioFernandes>() && collision.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            m_currentWeapon[1] = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
            m_WeaponUI.WeaponChange(m_currentWeapon[1].m_foodType, true, 5);
        }
    }
}


