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
// Mario 20/11/2020 - Subtration of ammunition and added chargedattack modifier
// Mario 28/11/2020 - item drop using "Q", now it stores the prefabs with him and childs
// Mario 29/11/2020 - contorller implementation
// Mario 05/12/2020 - full contorller detection and range suport
// Mario 06/12/2020 - Touchscreen suport
// Jann  08/12/2020 - Added projectile colour change
// Mário 16/12/2020 - playing the sound after the food check, 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    NULL
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
    public float m_chargedModifier = 1.3f;
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


    public void DropWeapon(WeaponType index)
    {
        if(m_currentWeapon[(int) index])
        {
        
        Transform dropW = transform.Find(m_currentWeapon[(int) index].name);

        dropW.gameObject.SetActive(true);
        dropW.position = transform.position + Vector3.down*0.75f;
        dropW.parent = null;

        m_currentWeapon[(int)index].enabled = false;
        m_currentWeapon[(int)index] = null;
        }
    }
  public  bool IsHoldingFood()
        {
       
        //Temp, should go the current weapon
        return false;
        }

    public void ResetStrength()
    {
        m_strenght = 1;
    }

    void Attack(InputAction cx, float chargedModifier = 1)
    {
        //m_animationScript.TriggerAttackAnim();        
            
        if (EventSystem.current.currentSelectedGameObject != null && Application.isMobilePlatform)
        {
        return;
        }else
        if(m_currentWeapon[m_weaponsIndex].IsRanged())
        {
            Vector3 m_direction;

            if(Application.isMobilePlatform)
            {
                m_direction = Camera.main.ScreenToWorldPoint(Controller.m_aim.ReadValue<Vector2>());
                
                m_direction = m_direction - transform.position;

            }else          
            if(Gamepad.current != null)
            {
            if(Controller.m_aim.ReadValue<Vector2>() != Vector2.zero)
            {
                m_direction = Controller.m_aim.ReadValue<Vector2>();
            }else
            return;

            }else
            {
            
            m_direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
            m_direction = m_direction - transform.position;

            
            }
            m_direction.z = 0; 

            m_direction.Normalize();
            //TODO undo this comment
            //m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
            GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
            projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght * chargedModifier);
            projectile.GetComponent<Projectile_MarioFernandes>().m_distractTime = m_currentWeapon[m_weaponsIndex].m_distractTime;
            projectile.GetComponent<SpriteRenderer>().sprite = m_currentWeapon[m_weaponsIndex].m_mySprite;
            projectile.GetComponent<Projectile_MarioFernandes>().m_direction = m_direction;
            --m_currentWeapon[m_weaponsIndex].m_ammunition;
            m_WeaponUI.setAmmo(-1);

            if(m_currentWeapon[m_weaponsIndex].m_foodType == FoodType.DANGO)
            {
            GameObject projectile2 = Instantiate(m_projectile, transform.position, transform.rotation);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_distractTime = m_currentWeapon[m_weaponsIndex].m_distractTime;
            projectile2.GetComponent<Projectile_MarioFernandes>().m_speed /= 2;
            projectile.GetComponent<SpriteRenderer>().sprite = m_currentWeapon[m_weaponsIndex].m_mySprite;
            }
            
            if(m_currentWeapon[m_weaponsIndex].m_ammunition <= 0)
            {
                print("Destroy usege");
                Destroy(m_currentWeapon[m_weaponsIndex].gameObject);
                m_currentWeapon[m_weaponsIndex] = null;
            }
            
            ColorChanger_Jann.Instance.UpdateColor(projectile.GetComponent<SpriteRenderer>());
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
                    CloseEnemy.GetComponentInParent<BaseEnemy_SebastianMol>().TakeDamage( m_damageType.MELEE , (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght * chargedModifier));
                }                
            }

            //EnemyDetection.enabled = false;        
        
    }

    public void Eat()
    {       

        if (m_currentWeapon[m_weaponsIndex])
        {
            m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Eating);

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

            m_WeaponUI.removeWeapon(m_currentWeapon[m_weaponsIndex].IsRanged());
 
            m_playerHealthHungerScript.Eat(m_currentWeapon[m_weaponsIndex].m_hungerRestoreAmount);

            print("Destroy Eating");
            Destroy(m_currentWeapon[m_weaponsIndex].gameObject);
            m_currentWeapon[m_weaponsIndex] = null;

        }
    }

    public void ChangeWeapon()
    {
        if (m_weaponsIndex == 1)
        {
            m_weaponsIndex = 0;
            m_WeaponUI.SetActiveWeapon(true);
        }
        else
        {
            m_weaponsIndex = 1;
            m_WeaponUI.SetActiveWeapon(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentWeapon = new WeaponsTemplate_MarioFernandes[2];
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        m_animationScript = GetComponentInChildren<PlayerAnimation_LouieWilliamson>();
        m_playerHealthHungerScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        m_audioManager = FindObjectOfType<AudioManager_LouieWilliamson>();
        Controller = GetComponent<PlayerController_JamieG>();
        m_WeaponUI = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
        m_effectManager = gameObject.GetComponent<EffectManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller.m_dropWeapons.triggered)
        {
        DropWeapon((WeaponType) m_weaponsIndex); 
        }

        // 0 - Melee weapon
        // 1 - Ranged weapon
         if(Controller.m_switchWeapons.triggered)
         {
             ChangeWeapon();             

             print(m_currentWeapon[m_weaponsIndex]);
         }

        if(m_timeSinceLastAttack < 0)
        {


              if (!m_playerStealthScript.m_crouched && m_currentWeapon[m_weaponsIndex])
            {         
                if( Controller.m_attackTap.triggered)              
                {    
                m_timeSinceLastAttack = m_attackDelay;
                
                Attack( Controller.m_attackTap);   
                } else
                if( Controller.m_attackSlowTap.triggered)              
                {    
                m_timeSinceLastAttack = m_attackDelay;
                
                Attack(Controller.m_attackSlowTap, m_chargedModifier);   
               } 
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
            collision.transform.parent = transform;
            m_WeaponUI.WeaponChange(m_currentWeapon[0].m_foodType, false, 0);
            m_WeaponUI.SetWeaponsUIAnimation(true);
		}
        else if(collision.GetComponent<WeaponsTemplate_MarioFernandes>() && collision.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            if(!m_currentWeapon[1])
            { 
            m_currentWeapon[1] = collision.GetComponent<WeaponsTemplate_MarioFernandes>();
            collision.gameObject.SetActive(false);
            collision.transform.parent = transform;
            m_WeaponUI.WeaponChange(m_currentWeapon[1].m_foodType, true, m_currentWeapon[1].m_ammunition);
            m_WeaponUI.SetWeaponsUIAnimation(true);
            }else{
                if(m_currentWeapon[1].m_foodType == collision.GetComponent<WeaponsTemplate_MarioFernandes>().m_foodType)
                {
                m_currentWeapon[1].m_ammunition += collision.GetComponent<WeaponsTemplate_MarioFernandes>().m_ammunition;
                //Destroy(collision.gameObject);
                print("Destroy ammunition");
                m_WeaponUI.WeaponChange(m_currentWeapon[1].m_foodType, true, m_currentWeapon[1].m_ammunition);
                m_WeaponUI.SetWeaponsUIAnimation(true);
                }
            }
        }
    }
}


