// Mário Fernandes
/// This class stores the current weapon on the player and make im abel to use it 

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
// Mario 08/11/2020 - Update Effects
// Mario 09/11/2020 - Update Names and Add strength

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
    PIZZA,
    SAKE,
    NOODLES,
} 
public class PlayerCombat_MarioFernandes : MonoBehaviour
{
    private PlayerAnimation_LouieWilliamson m_animationScript;
    public GameObject m_projectile = null;
    public float m_attackSpeed = 1;
    public float m_currentAttackime = 0;
    //public Collider2D EnemyDetection = null;    
    public float m_angeAttribute = 3;
    public float m_strenght = 1;
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    [SerializeField]
    protected WeaponsTemplate_MarioFernandes[] m_currentWeapon;

    public int m_weaponsIndex = 0; 
    PlayerHealthHunger_MarioFernandes m_playerHealthHungerScript;

    private AudioManager_LouieWilliamson m_audioManager;

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
             m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
             GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
             projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght);
        }
        else
        {
            m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);

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

                if(CloseEnemy && distanceToClosestsEnemy <= m_angeAttribute)
                {
                    CloseEnemy.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage((int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght));
                }                
            }

            //EnemyDetection.enabled = false;        
    }

    public void eat()
    {
        m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Eating);

        if (m_currentWeapon[m_weaponsIndex])
        {
            GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon[m_weaponsIndex].m_instaHeal);

            switch (m_currentWeapon[m_weaponsIndex].m_foodType)
            {
                case FoodType.FUGU:                   
                    if (Random.Range(0, 101) >= 50)
                        gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_poisonDmg));                        
                    break;
                case FoodType.SQUID:                    
                    break;
                case FoodType.RICEBALL:                    
                    break;
                case FoodType.KOBEBEEF:                   
                    gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(new SpeedEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_speedModifier));
                    break;
                case FoodType.SASHIMI:                    
                    gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(new StrengthEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_strengthModifier));
                    break;
                case FoodType.PIZZA:
                    break;
                case FoodType.SAKE:
                    break;
                case FoodType.NOODLES:
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
    }

    // Update is called once per frame
    void Update()
    {
         if(GetComponent<PlayerController_JamieG>().m_switchWeapons.triggered)
         {
             if(m_weaponsIndex == 1)
             m_weaponsIndex = 0;
             else
             m_weaponsIndex = 1;

             print(m_currentWeapon[m_weaponsIndex]);
         }

        if(m_currentAttackime < 0)
        {
              if (!m_playerStealthScript.m_crouched && m_currentWeapon[m_weaponsIndex] &&  GetComponent<PlayerController_JamieG>().m_attack.triggered)
            {                           
                m_currentAttackime = m_attackSpeed;
                print("Attack");
                Attack();        
            }          
        }
        else
        m_currentAttackime -= Time.deltaTime;

        if(GetComponent<PlayerController_JamieG>().m_eat.triggered)
        {
            eat();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(!m_currentWeapon[0] && collision.GetComponent<WeaponsTemplate_MarioFernandes>() && !collision.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            m_currentWeapon[0] = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
		}
        else if(!m_currentWeapon[1] && collision.GetComponent<WeaponsTemplate_MarioFernandes>() && collision.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            m_currentWeapon[1] = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
		}
	}
}


