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
public enum Q
{
    W,
    E,
    R,
    T,
    Y,
    U,
    I,
    O,
    P,
    A
} 

public enum S
{
    D,
    F
}

///<summary>
/// This class stores the current weapon on the player and make im abel to use it 
///<summary>
public class PlayerCombat_MarioFernandes : MonoBehaviour
{ 
     WeaponUI_LouieWilliamson q;
     PlayerAnimation_LouieWilliamson w;
    public GameObject G = null;
    public float H = 1;
    public float J = 0;
    //public Collider2D EnemyDetection = null;    
    public float K = 3;
    public float L = 1;
    public float Z = 1.3f;
    protected PlayerStealth_JoaoBeijinho X;

    //This weapons are represented by an array of weapons of size 2
    // 0 - Melee weapon
    // 1 - Ranged weapon
    [SerializeField]
    protected WeaponsTemplate_MarioFernandes[] C;

    public int V = 0; 
    PlayerHealthHunger_MarioFernandes h;

     AudioManager_LouieWilliamson e;

     PlayerController_JamieG r;

     EffectManager_MarioFernandes t;

     int y = 0;


    public void B(S u)
    {
        if(C[(int) u])
        {
            q.N(C[V].QR());
            Transform o = transform.Find(C[(int) u].name);

        o.gameObject.SetActive(true);
        o.position = transform.position + Vector3.down*0.75f;
        o.parent = null;

        C[(int)u].enabled = false;
        C[(int)u] = null;
        }
    }
  public  bool M()
        {
       
        //Temp, should go the current weapon
        return false;
        }

    public void QQ()
    {
        L = 1;
    }

    void QW(InputAction p, float a = 1)
    {
        w.QE();        
            
        if (EventSystem.current.currentSelectedGameObject != null && Application.isMobilePlatform)
        {
        return;
        }else
        if(C[V].QR())
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
            PlayTrack_Jann.Instance.EM(AudioFiles.QL); //added by alanna 10/12/20
            //TODO undo this comment
            //m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.PlayerAttack);
            GameObject projectile = Instantiate(G, transform.position, transform.rotation);
            projectile.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(C[V].dmg * L * a);
            projectile.GetComponent<Projectile_MarioFernandes>().m_distractTime = C[V].m_distractTime;
            projectile.GetComponent<SpriteRenderer>().sprite = C[V].m_mySprite;
            projectile.GetComponent<Projectile_MarioFernandes>().m_direction = s;
            --C[V].m_ammunition;
            q.QO(C[V].m_ammunition);

            if(C[V].m_foodType == Q.I)
            {
            GameObject projectile2 = Instantiate(G, transform.position, transform.rotation);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_dmg = (int)(C[V].dmg * L);
            projectile2.GetComponent<Projectile_MarioFernandes>().m_distractTime = C[V].m_distractTime;
            projectile2.GetComponent<Projectile_MarioFernandes>().m_speed /= 2;
            projectile.GetComponent<SpriteRenderer>().sprite = C[V].m_mySprite;
            }
            
            if(C[V].m_ammunition <= 0)
            {
                q.N(C[V].QR());
                print("Destroy usege");
                Destroy(C[V].gameObject);
                C[V] = null;
            }
            
            ColorChanger_Jann.Instance.QT(projectile.GetComponent<SpriteRenderer>());
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

                if(f && d <= K)
                {
                    f.GetComponentInParent<BaseEnemy_SebastianMol>().RW( WZ.WX , (int)(C[V].dmg * L * a));
                }                
            }

            //EnemyDetection.enabled = false;        
        
    }

    public void QY()       
    {

     
        if (C[V] && (!C[V].QR() || C[V].QR() && C[V].m_ammunition >= y))
        {            
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_Eating); //aaded by alanna 10/12/20

            

            GetComponent<PlayerHealthHunger_MarioFernandes>().Heal(C[V].m_instaHeal);

            switch (C[V].m_foodType)
            {
                case Q.W:                   
                    if (Random.Range(0, 101) >= 50)
                        t.h(new PoisionDefuff_MarioFernandes(5, C[V].m_poisonDmg));                        
                    break;
                case Q.E:                    
                    break;
                case Q.R:                    
                    break;
                case Q.T:                   
                    t.h(new SpeedEffect_MarioFernandes(5, C[V].m_speedModifier));
                    break;
                case Q.Y:                    
                    t.h(new StrengthEffect_MarioFernandes(5, C[V].m_strengthModifier));
                    break;
                case Q.U:                   
                    break;
                case Q.I:
                    break;
                case Q.O:                    
                    break;
                case Q.P:
                    t.h(new StrengthEffect_MarioFernandes(30, C[V].m_strengthModifier));
                    break;
                default:
                    break;
            }

            
 
            h.QI(C[V].QU);

            if(C[V].QR())
            {
            C[V].m_ammunition -= y;
            q.QO(C[V].m_ammunition);
            }


            if(!C[V].QR() || C[V].m_ammunition <= 0)
            {
                q.N(C[V].QR());
            
            Destroy(C[V].gameObject);
            C[V] = null;
            }

        }
    }

    public void QP()
    {
        if (V == 1)
        {
            V = 0;
            q.SetActiveWeapon(true);
        }
        else
        {
            V = 1;
            q.SetActiveWeapon(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        C = new WeaponsTemplate_MarioFernandes[2];
        X = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
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
        if(r.QA.triggered)
        {
        B((S) V); 
        }

        // 0 - Melee weapon
        // 1 - Ranged weapon
         if(r.QS.triggered)
         {
             QP();             

         }

        if(J < 0)
        {


            if (!X.B && C[V])
            {         
                if( r.m_attackTap.triggered)              
                {    
                J = H;
                
                QW( r.m_attackTap);   
                } else
                if( r.m_attackSlowTap.triggered)              
                {    
                J = H;
                
                QW(r.m_attackSlowTap, Z);   
                } 
            }          
        }
        else
        J -= Time.deltaTime;

        if(r.m_eat.triggered)
        {
            QY();
        }

       
    }

     void OnTriggerEnter2D(Collider2D j)
	{
		if(!C[0] && j.GetComponent<WeaponsTemplate_MarioFernandes>() && !j.GetComponent<WeaponsTemplate_MarioFernandes>().QR())
        {
            C[0] = j.GetComponent<WeaponsTemplate_MarioFernandes>();
            j.gameObject.SetActive(false);
            j.transform.parent = transform;
            q.QD(C[0].m_foodType, false, 0);
            q.QF(true);
		}
        else if(j.GetComponent<WeaponsTemplate_MarioFernandes>() && j.GetComponent<WeaponsTemplate_MarioFernandes>().QR())
        {
            if(!C[1] )
            {                 
                C[1] = j.GetComponent<WeaponsTemplate_MarioFernandes>();
                y = C[1].m_ammunition;
                j.gameObject.SetActive(false);
                j.transform.parent = transform;
                q.QD(C[1].m_foodType, true, C[1].m_ammunition);
                q.QF(true);
            }
            else{                
                
                if(j.gameObject.activeSelf && C[1].m_foodType == j.GetComponent<WeaponsTemplate_MarioFernandes>().m_foodType)
                {
                                       
                    j.gameObject.SetActive(false);
                    C[1].m_ammunition += j.GetComponent<WeaponsTemplate_MarioFernandes>().m_ammunition;
                    Destroy(j.gameObject);

                    
                    q.QD(C[1].m_foodType, true, C[1].m_ammunition);
                    q.QF(true);
                }
            }
        }
    }    
}





