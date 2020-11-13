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
//sebastian mol 05/11/20 changed enemy take damage function call

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

    public float attackSpeed = 1;
    public float currentatktime = 0;
    //public Collider2D EnemyDetection = null;
    
    public float RangeAttribute = 3;
    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;

    [SerializeField]
    protected WeaponsTemplate_MarioFernandes m_currentWeapon;

    PlayerHealthHunger_MarioFernandes m_playerHealthHungerScript;

    private AudioManager_LouieWilliamson m_audioManager;

  public  bool IsHoldingFood()
        {
       
        //Temp, should go the current weapon
        return false;
        }

    void PickUpFood()
    {
        
    }

    void Attack()
    {
        m_animationScript.TriggerAttackAnim();

        if(m_currentWeapon.IsRanged())
        {
            //TODO undo this comment
             //m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
             GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
             projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = m_currentWeapon.dmg;
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


            // --------------


            //print("Activate Detection");
            ////EnemyDetection.enabled = true;
            //print("Detect enemy");
            //Collider2D[] results = new Collider2D[10];
            //ContactFilter2D contact = new ContactFilter2D();   
            //contact.NoFilter();
//
            //GetComponent<CircleCollider2D>().OverlapCollider(contact, results);
            //
            //
            //print("Detected enemys: " + results.Length);            
            // 
            //if(results[0] )
            //{
            //    
            //    GameObject CloseEnemy = null;
            //    float distance = 100;
//
            //    foreach (var enemy in results)
            //    {
            //        if(enemy.tag == "Enemy")
            //        {
            //            if (distance > Vector3.Distance(transform.position, enemy.transform.position))
            //            {
            //            distance = Vector3.Distance(transform.position, enemy.transform.position);
            //            CloseEnemy = enemy.gameObject;
            //            }
            //        }
            //    }

                if(CloseEnemy && distanceToClosestsEnemy <= RangeAttribute)
                {
                    CloseEnemy.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage( m_damageType.MELEE , m_currentWeapon.dmg);
                }                
            }

            //EnemyDetection.enabled = false;
        
    }

    public void eat()
    {
        m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Eating);

        if (m_currentWeapon)
        {
            switch (m_currentWeapon.m_foodType)
            {
                case FoodType.FUGU:
                    GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon.m_instaHeal);
                    if (Random.Range(0, 101) >= 50)
                        gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(m_currentWeapon.m_poisonDmg, 5));
                    break;
                case FoodType.SQUID:
                    GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon.m_instaHeal);
                    break;
                case FoodType.RICEBALL:
                    GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon.m_instaHeal);
                    break;
                case FoodType.KOBEBEEF:
                    GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon.m_instaHeal);
                    gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(new PoisionDefuff_MarioFernandes(0, 5, m_currentWeapon.m_speedModifier));
                    break;
                case FoodType.SASHIMI:
                    GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon.m_instaHeal);
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

            m_playerHealthHungerScript.Eat(m_currentWeapon.m_hungerRestoreAmount);

            m_currentWeapon.enabled = false;
            m_currentWeapon = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        m_animationScript = GetComponent<PlayerAnimation_LouieWilliamson>();
        m_playerHealthHungerScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        m_audioManager = FindObjectOfType<AudioManager_LouieWilliamson>();
    }

    // Update is called once per frame
    void Update()
    {
         

        if(currentatktime < 0)
        {
              if (!m_playerStealthScript.m_crouched && m_currentWeapon &&  GetComponent<PlayerController_JamieG>().m_attack.triggered)
            {                           
                currentatktime = attackSpeed;
                print("Attack");
                Attack();        
            }          
        }
        else
        currentatktime -= Time.deltaTime;

        if(GetComponent<PlayerController_JamieG>().m_eat.triggered)
        {
            eat();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(!m_currentWeapon && collision.GetComponent<WeaponsTemplate_MarioFernandes>())
        {
            m_currentWeapon = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
		}
	}
}


