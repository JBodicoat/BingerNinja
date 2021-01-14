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
// Alanna 10/12/20 added sound effects for Melee hit, ranged hit and eating


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
     WeaponUI_LouieWilliamson q;
     PlayerAnimation_LouieWilliamson w;
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
    PlayerHealthHunger_MarioFernandes h;

     AudioManager_LouieWilliamson e;

     PlayerController_JamieG r;

     EffectManager_MarioFernandes t;

     int y = 0;


    public void DropWeapon(WeaponType u)
    {
        if(m_currentWeapon[(int) u])
        {
            q.removeWeapon(m_currentWeapon[m_weaponsIndex].IsRanged());
            Transform o = transform.Find(m_currentWeapon[(int) u].name);

        o.gameObject.SetActive(true);
        o.position = transform.position + Vector3.down*0.75f;
        o.parent = null;

        m_currentWeapon[(int)u].enabled = false;
        m_currentWeapon[(int)u] = null;
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

    void Attack(InputAction p, float a = 1)
    {
        w.TriggerAttackAnim();        
            
        if (EventSystem.current.currentSelectedGameObject != null && Application.isMobilePlatform)
        {
        return;
        }else
        if(m_currentWeapon[m_weaponsIndex].IsRanged())
        {
            Vector3 s;

            if(Application.isMobilePlatform)
            {
                s = Camera.main.ScreenToWorldPoint(r.m_aim.ReadValue<Vector2>());
                
                s = s - transform.position;

            }else          
            if(Gamepad.current != null)
            {
            if(r.m_aim.ReadValue<Vector2>() != Vector2.zero)
            {
                s = r.m_aim.ReadValue<Vector2>();
            }else
            return;

            }else
            {
            
            s = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
            s = s - transform.position;            
            }
            s.z = 0; 

            s.Normalize();
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_PlayerThrow); //added by alanna 10/12/20
            //TODO undo this comment
            //m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
            GameObject projectile = Instantiate(m_projectile, transform.position, transform.rotation);
            projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght * a);
            projectile.GetComponent<Projectile_MarioFernandes>().m_distractTime = m_currentWeapon[m_weaponsIndex].m_distractTime;
            projectile.GetComponent<SpriteRenderer>().sprite = m_currentWeapon[m_weaponsIndex].m_mySprite;
            projectile.GetComponent<Projectile_MarioFernandes>().m_direction = s;
            --m_currentWeapon[m_weaponsIndex].m_ammunition;
            q.setAmmo(m_currentWeapon[m_weaponsIndex].m_ammunition);

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
                q.removeWeapon(m_currentWeapon[m_weaponsIndex].IsRanged());
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
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_PlayerAttack); //added by alanna 10/12/20

            float d = Mathf.Infinity;
                GameObject f = null;
                GameObject[] g = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject h in g)
                {
                    float j = (h.transform.position - this.transform.position).sqrMagnitude;
                    if(j < d)
                    {
                        d = j;
                        f = h;
                    }
                }

                if(f && d <= m_meleeAttackRadius)
                {
                    f.GetComponentInParent<BaseEnemy_SebastianMol>().RW( WZ.WX , (int)(m_currentWeapon[m_weaponsIndex].dmg * m_strenght * a));
                }                
            }

            //EnemyDetection.enabled = false;        
        
    }

    public void Eat()       
    {

     
        if (m_currentWeapon[m_weaponsIndex] && (!m_currentWeapon[m_weaponsIndex].IsRanged() || m_currentWeapon[m_weaponsIndex].IsRanged() && m_currentWeapon[m_weaponsIndex].m_ammunition >= y))
        {            
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_Eating); //aaded by alanna 10/12/20

            

            GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(m_currentWeapon[m_weaponsIndex].m_instaHeal);

            switch (m_currentWeapon[m_weaponsIndex].m_foodType)
            {
                case FoodType.FUGU:                   
                    if (Random.Range(0, 101) >= 50)
                        t.AddEffect(new PoisionDefuff_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_poisonDmg));                        
                    break;
                case FoodType.SQUID:                    
                    break;
                case FoodType.RICEBALL:                    
                    break;
                case FoodType.KOBEBEEF:                   
                    t.AddEffect(new SpeedEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_speedModifier));
                    break;
                case FoodType.SASHIMI:                    
                    t.AddEffect(new StrengthEffect_MarioFernandes(5, m_currentWeapon[m_weaponsIndex].m_strengthModifier));
                    break;
                case FoodType.TEMPURA:                   
                    break;
                case FoodType.DANGO:
                    break;
                case FoodType.SAKE:                    
                    break;
                case FoodType.NOODLES:
                    t.AddEffect(new StrengthEffect_MarioFernandes(30, m_currentWeapon[m_weaponsIndex].m_strengthModifier));
                    break;
                default:
                    break;
            }

            
 
            h.Eat(m_currentWeapon[m_weaponsIndex].m_hungerRestoreAmount);

            if(m_currentWeapon[m_weaponsIndex].IsRanged())
            {
            m_currentWeapon[m_weaponsIndex].m_ammunition -= y;
            q.setAmmo(m_currentWeapon[m_weaponsIndex].m_ammunition);
            }


            if(!m_currentWeapon[m_weaponsIndex].IsRanged() || m_currentWeapon[m_weaponsIndex].m_ammunition <= 0)
            {
                q.removeWeapon(m_currentWeapon[m_weaponsIndex].IsRanged());
            
            Destroy(m_currentWeapon[m_weaponsIndex].gameObject);
            m_currentWeapon[m_weaponsIndex] = null;
            }

        }
    }

    public void ChangeWeapon()
    {
        if (m_weaponsIndex == 1)
        {
            m_weaponsIndex = 0;
            q.SetActiveWeapon(true);
        }
        else
        {
            m_weaponsIndex = 1;
            q.SetActiveWeapon(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentWeapon = new WeaponsTemplate_MarioFernandes[2];
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        w = GetComponent<PlayerAnimation_LouieWilliamson>();
        h = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        e = FindObjectOfType<AudioManager_LouieWilliamson>();
        r = GetComponent<PlayerController_JamieG>();
        q = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
        t = gameObject.GetComponent<EffectManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        if(r.m_dropWeapons.triggered)
        {
        DropWeapon((WeaponType) m_weaponsIndex); 
        }

        // 0 - Melee weapon
        // 1 - Ranged weapon
         if(r.m_switchWeapons.triggered)
         {
             ChangeWeapon();             

         }

        if(m_timeSinceLastAttack < 0)
        {


            if (!m_playerStealthScript.B && m_currentWeapon[m_weaponsIndex])
            {         
                if( r.m_attackTap.triggered)              
                {    
                m_timeSinceLastAttack = m_attackDelay;
                
                Attack( r.m_attackTap);   
                } else
                if( r.m_attackSlowTap.triggered)              
                {    
                m_timeSinceLastAttack = m_attackDelay;
                
                Attack(r.m_attackSlowTap, m_chargedModifier);   
                } 
            }          
        }
        else
        m_timeSinceLastAttack -= Time.deltaTime;

        if(r.m_eat.triggered)
        {
            Eat();
        }

       
    }

     void OnTriggerEnter2D(Collider2D j)
	{
		if(!m_currentWeapon[0] && j.GetComponent<WeaponsTemplate_MarioFernandes>() && !j.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            m_currentWeapon[0] = j.GetComponent<WeaponsTemplate_MarioFernandes>();
            j.gameObject.SetActive(false);
            j.transform.parent = transform;
            q.WeaponChange(m_currentWeapon[0].m_foodType, false, 0);
            q.SetWeaponsUIAnimation(true);
		}
        else if(j.GetComponent<WeaponsTemplate_MarioFernandes>() && j.GetComponent<WeaponsTemplate_MarioFernandes>().IsRanged())
        {
            if(!m_currentWeapon[1] )
            {                 
                m_currentWeapon[1] = j.GetComponent<WeaponsTemplate_MarioFernandes>();
                y = m_currentWeapon[1].m_ammunition;
                j.gameObject.SetActive(false);
                j.transform.parent = transform;
                q.WeaponChange(m_currentWeapon[1].m_foodType, true, m_currentWeapon[1].m_ammunition);
                q.SetWeaponsUIAnimation(true);
            }
            else{                
                
                if(j.gameObject.activeSelf && m_currentWeapon[1].m_foodType == j.GetComponent<WeaponsTemplate_MarioFernandes>().m_foodType)
                {
                                       
                    j.gameObject.SetActive(false);
                    m_currentWeapon[1].m_ammunition += j.GetComponent<WeaponsTemplate_MarioFernandes>().m_ammunition;
                    Destroy(j.gameObject);

                    
                    q.WeaponChange(m_currentWeapon[1].m_foodType, true, m_currentWeapon[1].m_ammunition);
                    q.SetWeaponsUIAnimation(true);
                }
            }
        }
    }    
}





