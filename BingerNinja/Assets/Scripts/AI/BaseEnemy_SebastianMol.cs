//sebastian mol
//sebastian mol 30/10/2020 patrol now handles multiple patrol points
//sebastian mol 02/11/2020 revisions to comments
//sebastian mol 02/11/20 changed enemy behaviour funstion int AILogic function and created more abstract functions.
//sebastian mol 02/11/20 now path gets recalculated when player moves away from original position 
//sebastian mol 02/11/20 improved player detection with second raycast
//sebastian mol 06/11/20 new damage sysetm
//sebastian mol 11/11/2020 enemy can now be stunned
//sebastian mol 11/11/2020 tiger enemy cant see player in vent now
//Joao Beijinho 12/11/2020  Added layerMask for crouchObjectLayer and reference to playerStealth()
//                          Added layerMask to raycast in PlayerDetectionRaycasLogic() and IsPlayerInLineOfSight()
//                          Added m_playerStealthScript.IsCrouched() to PlayerDetectionRaycasLogic() and two else if inside
//                          Changed tags in PlayerDetectionRaycasLogic() to use the Tags_JoaoBeijinho() tags
//sebastian mol 14/11/2020 moved logic out of child classes and moved into here
//Elliott Desouza 20/11/2020 added hit effect and camera shake when taken damage
//sebastian mol 18/11/2020 alien now dosent get stunned
//sebastian mol 20/11/2020 spce ninja enemy logic done
//sebastian mol 29/11/2020 creaeted spece for exlemation mark adn completed damage take for last boss
//Elliott Desouza 30/11/2020 added a funtion (OnceLostContactEffect) which instanshates the Question mark prefab.
// Alanna & Elliott 07/12/20 Added Ninja points when getting a critical hit (sneak attack on enemy) 
// Alanna 10/12/20 added death sound effect for enemies 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum WW { WE, WR, WT, WY };
public enum WU { WI, WO, WP, WA, WS, WD, WF, WG, WH, WJ, WK, WL };
public enum WZ { WX, WC, WV, WB };
/// <summary>
///base class for enemies to inherit from with logic for detection, patrole, movment, stats managment
/// </summary>
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform a; //start position of the ray cast
    public Transform b; //secondary rey cast for better detection neer walls
    public PolygonCollider2D CE; // the collder cone used for player detection
    public bool CW = false; //has the player been detected   
    public WW CQ = WW.WE;//current state of teh enemy   
    public WU CA;
    public GameObject CR;
    public GameObject CT;
    public Inventory_JoaoBeijinho CY; 
    public float CU;

    public GameObject CI; // itme that i sdropped when enemie dies
    
    public float CO; //enemy health with getter and setter
   
    public float CP; //movment speed


    
    public bool CS = false;
  
    public Tilemap CD;
 
    public Transform[] CF;
   
    public float CG;
    
    public float CH;
   
    public bool CJ;
    
    public float CK;
    
     float m = 0.5f;

    
    public float X;
    
    public float C;
   
    public float V;
    public float B = 1f;
    
    public float N = 2;

    
    public float M = 0.3f;
   
    public float QQ = 3;
   
    public bool QW = true;
    
    public float QE = 1.5f;

    internal float QR; //max amount of health an enemy has

     Pathfinder_SebastianMol q;
    protected List<Vector2Int> QT = new List<Vector2Int>();
    protected Transform QY; //used to get player position can be null if undedteceted
    protected float QU; //player scale at start
     Vector3 w; //the last position the enemy was at
    protected Vector3 QI;//the starting position of the enemy
    protected float QO; //timer for attack deley
    protected float QP; //timer for line of sight check
    protected int QA = 0; //iterated through patrole points
    protected float QS;
     int e; //the max for teh iterator so it dosent go out of range  
     float r; // timer for waiting at each patrole pos
     Transform t; //the current patrole pos were haeding to / are at 
     Vector3 y; //last given to the path finder to find a path e.g. player position
    protected bool QD = false; //used to stunn the enemy
     float u; //used to remeber m_lookLeftAndRightTimer varaibale at the start for later resents
     bool i = false; // if the enemy serching for player
     Vector3 o;  //the point of curiosity for an enemy to cheak
    protected int QF = 1; //the phase tadashi is on


     CameraShakeElliott s;
    protected bool QG = false;
     PlayerSpoted_Elliott d;
    

    protected PlayerStealth_JoaoBeijinho QH;
     int f = 1 << 8;
    /// <summary>
    /// abstract class used to provied the logic for the wonder state
    /// </summary>
     void g()
    {
        if (CJ)
        {
            WM();
        }
        else
        {
            CE.enabled = true;
            if (Vector2.Distance(transform.position, QI) > 0.5f)
            {
                EQ(QI);
            }

            if(Vector2.Distance(transform.position, QI) <= 0.5f) transform.localScale = new Vector3(QU, transform.localScale.y, transform.localScale.z);

        }
        QG = false;
    }

    /// <summary>
    /// abstract class used to provied the logic for the chase state
    /// </summary>
     void h()
    {
        if (EW()) // if you can see player
        {
            if (Vector2.Distance(transform.position, QY.position) < V / B) //if the player is in range
            {
                EE(false);
                CQ = WW.WT;
            }
            else// if the player is out fo range
            {
                EQ(QY.position);
            }
        }
        else
        {
            ER();
            i = true;
            if (QT.Count == 0)
            {
                if (QP <= 0)
                {
                    i = false;
                    CQ = WW.WE;
                    CW = false;
                }
                else
                {
                    j();
                    QP -= Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// contains logic for when the enemy is serching for the player it looks left and right every half second
    /// </summary>
    void j()
    {
        if(m <= 0)
        {
            if(transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-QU, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(QU, transform.localScale.y, transform.localScale.z);
            }
            m = u;
        }
        else
        {
            m -= Time.deltaTime;
        }
    }

    /// <summary>
    /// abstract class used to provied the logic for the attack state
    /// </summary>
    abstract internal void ET();

     void k()
    {
        if (EW())
        {
            if (Vector2.Distance(transform.position, QY.position) < V)
            {
                ET();

            }
            else
            {
                CQ = WW.WR;
            }

            QP = CK;
            CW = true;

        }
        else
        {
            CW = false;
            if (QP <= 0)
            {
                CQ = WW.WE;
            }
            else
            {
                QP -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// a state that occurs when enemy is forced to look at a specific location
    /// </summary>
     void l()
    {
        if (Vector2.Distance( transform.position, o) < 1 ) 
        if (QP <= 0)
        {
            CQ = WW.WE;
            QP = CK;
        }
        else
        {
            QP -= Time.deltaTime;
            ER();
        }
    }

    /// <summary>
    /// contains the switch that stores the dofferent behavoiurs the enemy dose in each state
    /// </summary>
     void z()
    {
        switch (CQ)
        {
            case WW.WE:
                g();
                break;
            case WW.WR:
                h();
                break;
            case WW.WT:
                k();
                break;
            case WW.WY:
                l();
                break;
        }
    }


    /// <summary>
    /// detect player in vision cone the establishes line of sight
    /// </summary>
    /// <param name="EI"> the collion date from the onTrigger functions</param>
    protected void EY(GameObject EI)
    {
        if(EI.CompareTag("Player")) // is it a the player 
        {

            PlayerStealth_JoaoBeijinho EU = EI.GetComponent<PlayerStealth_JoaoBeijinho>();

            if (CA == WU.WF)
            {
                if(!EU.H())
                {
                    x(EI);
                }          
            }
            else if(!EU.F()) //is the player in stealth/
            {
                x(EI);
            }
        }
    }

    /// <summary>
    /// holds the logic for casting a ray when the player is first detected
    /// </summary>
    /// <param name="EO"> a collsion that is checked to see if it is the player</param>
     void x(GameObject EO)
    {
        CE.enabled = false;
        RaycastHit2D EP = Physics2D.Linecast(a.position, EO.transform.position);

        //RaycastHit2D crouchedHit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position);
        //Debug.DrawLine(m_rayCastStart.position, col.transform.position, Color.green);

        if (!QH.G() && EP.collider.gameObject.CompareTag(Tags_JoaoBeijinho.QK)) //player is not crouched and it hits him
        {
            //  m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Detection);
            CW = true;
            QY = EP.transform;
            CQ = WW.WT;
            EA();
            EE();
        }
        else
        {
            CE.enabled = true;
        }
        //else if (m_playerStealthScript.IsCrouched() && crouchedHit.collider.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)) //player is croucheed and it hits 
        //{
        //    //  m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Detection);
        //    m_playerDetected = true;
        //    m_playerTransform = hit.transform;
        //    m_currentState = state.ATTACK;
        //    NoticePlayerEffect();
        //    ClearPath();
        //}
        //else if (m_playerStealthScript.IsCrouched() && !crouchedHit.collider.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        //{
        //    m_detectionCollider.enabled = true;
        //}

    }

    /// <summary>
    /// returns waeether the player is in line of sight of the enemy
    /// </summary>
    /// <returns>weather the player is in line of sight</returns>
    protected bool EW()
    {
        if (QY)
        {
            CE.enabled = false;
            RaycastHit2D ES = Physics2D.Linecast(a.position, QY.position);
         

            if (ES.collider.gameObject.CompareTag("Player"))
            {
                CE.enabled = false;
                return true;
            }
            else
            {

                RaycastHit2D ED = Physics2D.Linecast(b.position, QY.position);

                if (ED.collider.gameObject.CompareTag("Player"))
                {
                    CE.enabled = false;
                    return true;
                }
                else
                {
                    CE.enabled = true;
                    return false;
                }
            }
        }
        return false;
    }



    /// <summary>
    /// set the position to move to in world coords
    /// </summary>
    /// <param name="EG">the destination of the enemy</param>
    protected void EF(Vector3 EG)
    {
        Vector2Int EH = (Vector2Int)q.QB.WorldToCell(transform.position);
        Vector2Int EJ = (Vector2Int)q.QB.WorldToCell(EG);
        QT = q.WE(EH, EJ);

        if(CS)
        {
            for (int i = 0; i < QT.Count; i++)
            {
                CD.SetTileFlags(new Vector3Int(QT[i].x, QT[i].y, 0), TileFlags.None);
                CD.SetColor(new Vector3Int(QT[i].x, QT[i].y, 0), Color.green);
                //Debug.Log(floortilemap.GetCellCenterWorld(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0)));
            }
        }
    }

    /// <summary>
    /// make the enemy follow the path it has created
    /// </summary>
    protected void EK()
    {
        if (QT.Count == 0) return;//skip check

        float EL = CP * Time.deltaTime; //movment allowed this frame

        while (EL > 0)
        {
            //gets the center of the target tile
            Vector2 EZ = (Vector2)q.QB.CellToWorld(new Vector3Int( QT[0].x, QT[0].y, 0)) + ((Vector2)q.QB.cellSize / 2);
            EZ += new Vector2(-0.5f, 0);
            float EX = Vector2.Distance(transform.position, EZ); //distance to the target tile center               
               
            if (EX > EL)//if all movment is used up in one move
            {
                transform.position = Vector2.MoveTowards(transform.position, EZ, EL);//move
                EL = 0;
            }
            else //if theres movment left over after the next tile
            {
                transform.position = Vector2.MoveTowards(transform.position, EZ, EX);//move
                EL -= EX;
                if (CS) CD.SetColor(new Vector3Int(QT[0].x, QT[0].y, 0), Color.white);
                QT.RemoveAt(0);
                if (QT.Count == 0) break;
            }            
        }
    }

    /// <summary>
    /// clears the path and optionaly adds one last step to center the enemy
    /// </summary>
    /// <param name="EC">desides weather to center the enemy at the end of the path</param>
    protected void EE(bool EC = true) // use this to stop walking
    {
        if (CS)
        {
            for (int i = 0; i < QT.Count; i++)
            {
                CD.SetColor(new Vector3Int(QT[i].x, QT[i].y, 0), Color.white);
            }
        }
        QT.Clear();
        if(EC) QT.Add((Vector2Int)q.QB.WorldToCell(transform.position));
        y = Vector3.zero;
    }

    /// <summary>
    /// holds logic for patrole functtionality.
    /// </summary>
    protected void WM()
    {
        if (t == null) return;
        //not sure what this dose might be a bit late at night, maybe initilaziation???? it just works
        if (t.position.x == 0 && t.position.y == 0) t = CF[0];
        //if theres no were to go go to the first patrole point
        if (QT.Count == 0) EF(CF[QA].position);
        //if close neough to the next patrole point start to swap patrole points
        if (Vector2.Distance(transform.position, CF[QA].position) <= 0.4f) EV();
        //start the patrole
        EK();
    }

    /// <summary>
    /// loops through a list of patrole points to set the enemies next destination
    /// </summary>
    protected void EV()
    {
        if(CF.Length > 0) //are there any patrole points
        {
            if (r <= 0) //if the timer has reached 0 start the swap
            {
                if (QA == e) // see if the itterator is at its limit
                {
                    QA = 0; //set to first iteration
                    t = CF[QA]; //move to destination
                }
                else// else go through the list 
                {
                    t = CF[++QA];
                }
                r = CH; //reset the timer
            }
            else
            {
                r -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// used to move the player
    /// </summary>
    /// <param name="pos">where you wnaty the player to be moved</param>
    protected void EQ(Vector3 pos)
    {
        if (QT.Count == 0)
        {
            EF(pos);
        }

        if (y == Vector3.zero) // if there hasent been an old pos 
        {
            y = pos;
        }

        if (Vector2.Distance(y, pos) > CG) //if players old pos is a distance away to his new pos go to new pos
        {
            EE();
            EF(pos);
        } 
        
        EK();
    }


    /// <summary>
    /// actions that happen before enemy death.
    /// </summary>
    public void EB() 
    {
        if(CO <= 0)
        {
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_Damage);
            if (CI)
            {
                Instantiate(CI, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
            CY.EN();
        }    
    }

    /// <summary>
    /// controls the scale of the enemy based on player or direction
    /// </summary>
    protected void RQ()
    {
        if(!i)
        if(CW)
        {
            if(QY.position.x > transform.position.x)
            {
                transform.localScale = new Vector3( -QU, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(QU, transform.localScale.y, transform.localScale.z);
            }

        }
        else
        {
            if(w.x > transform.position.x)
            {
                transform.localScale = new Vector3(QU, transform.localScale.y, transform.localScale.z);
            }
            else if(w.x < transform.position.x)
            {
                transform.localScale = new Vector3(-QU, transform.localScale.y, transform.localScale.z);
            }
            w = transform.position;
        }
    }

    /// <summary>
    /// used to apply correct damage amaount and type based on enemy type
    /// </summary>
    /// <param name="RE">what type of damage e.g. melee</param>
    /// <param name="RR">amount of damage</param>
    public void RW(WZ RE, float RR)
    {
        
        switch (CA)
        {
            case WU.WI:
                v(RR);
                break;

            case WU.WO:
                if (RE == WZ.WX)
                {
                    v(RR * 1.5f);
                }
                else
                {
                    v(RR);
                }   
                break;

            case WU.WL:

                break;

            case WU.WP:
                if(QY.position.x < transform.position.x) //player on the left
                {
                    if(transform.localScale.x < 0) v(RR); //looking left
                }
                else if(QY.position.x > transform.position.x) //player on the right
                {
                    if (transform.localScale.x > 0) v(RR); //looking right
                }
                else
                {
                    v(RR*0.25f);
                }
                break;

            case WU.WS:
                //half damage melle when not sneaked
                if (RE == WZ.WX)
                {
                    if(CW == false) // in stealth
                    {
                        v(RR); // normal melle stealth attack
                    }
                    else // not stealth 
                    {
                        v(RR * 0.5f); //half damage stealth atatck
                    }                  
                }
                else
                {
                    v(RR); // normal
                }
                break;

            case WU.WD:
            case WU.WA:
                //sneak multiplier
                Debug.Log(CW);
                if (CW == false)
                {
                    CO -= RR * (X + C);

                }
                else
                {
                    CO -= RR;

                }
                break;

            case WU.WJ:             
                if ((CO / QR) <= M) //second fase
                {
                    v(RR);
                    QO *= QE;
                    QW = false;
                    C = 0;
                }
                else
                {
                     //sneak multiplier
                    if (CW == false)
                    {
                        CO -= RR * (X + C);
                    }
                    else
                    {
                        CO -= RR;
                    }
                }
                break;

            case WU.WK:
                float RY = CO / QR;
                if (RY > 0.6f)
                {
                    QF = 1;
                    v(RR);
                }
                else if( RY > 0.3f)
                {
                    QF = 2;
                    CO -= RR;
                }
                else//health below 30
                {
                    QF = 3;
                    CO -= RR;
                }

                break;

            default:
                v(RR);
                break;

        }

        CQ = WW.WR;

        EB();//checks to see if enemy is dead 
    }

    /// <summary>
    /// stop the enemys movment and attack, makes it look liek enemy ios tsunned but can stil be damaged
    /// </summary>
     void c()
    {
        QD = !QD;
    }

    /// <summary>
    /// stund the enemy for an amount of time in seconds
    /// </summary>
    /// <param name="RI"> the amaount of time the enemy is stunend for</param>
    /// <returns></returns>
    public IEnumerator RU(float RI)
    {
        c();
        yield return new WaitForSeconds(RI);
        c();
    }

    /// <summary>
    /// for use with distraction projectile to stun enemies
    /// </summary>
    /// <param name="RP">amaount of time to stun in seconds</param>
    public void RO(float RP)
    {
        if(CA != WU.WG) StartCoroutine(RU(RP));
    }

    /// <summary>
    /// for use with ligths to stun enemies
    /// </summary>
    /// <param name="RS">amaount of time to stun in seconds</param>
    public void RA(float RS)
    {
        StartCoroutine(RU(RS));
    }

    /// <summary>
    /// normal way of enemy takign damage
    /// </summary>
    /// <param name="RD">amount of damage</param>
     void v( float RD )
    {
        if (CW == false) //if sneak damage
        {
            CO -= RD * X;
            s.RF();
            CY.RG(ItemType.NinjaPoints, 1);
        }
        else
        {
            CO -= RD;
        }
    }

    /// <summary>
    /// forse enemy to look at a certain position and serch for the player
    /// </summary>
    /// <param name="pos">the postion that you want the enemy to search</param>
    public void RH(Vector3 pos)
    {
        EE(false);
        CQ = WW.WY;
        o = pos;
        EQ(o);
    }

    /// <summary>
    /// effect that happens when enemy noticese the player
    /// </summary>
    public void EA()
    {
       GameObject RK = Instantiate(CT, new Vector3(transform.position.x,transform.position.y + CU,transform.position.z), Quaternion.identity);
        RK.transform.parent = transform;
        
    }

    public void ER()
    {
        if (!QG)
        {
            GameObject A = Instantiate(CR, new Vector3(transform.position.x, transform.position.y + CU, transform.position.z), Quaternion.identity);
            A.transform.parent = transform;
            QG = true;
           
        }
    }

    public void RJ()
    {
        transform.position = QI;
        i = false;
        CQ = WW.WE;
        CW = false;
        QP = CK;
        ER();
    }


     void Start()
    {
        q = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        QU = transform.localScale.x;
        w = transform.position;
        QI = transform.position;
        e = CF.Length-1;
        r = CH;
        if (CF.Length > 0) t = CF[0];
        u = m;
        QR = CO;
        QP = CK;
        QS = V;

        CY = GameObject.Find("Player").GetComponent<Inventory_JoaoBeijinho>();
        QH = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        s = Camera.main.GetComponent<CameraShakeElliott>();
    }

     void Update()
    {

        if(!QD)
        {
            z(); // behaviour of the enemy what stste it is in and what it dose
            EK(); //walk the path that the enemy currently has  
            RQ(); //chnge the scale of the player
        }

    }

     void OnTriggerEnter2D(Collider2D RL)
    {
        if(!QD)
            if(!CW)
            {
                EY(RL.gameObject);
            }      
    }

     void OnTriggerStay2D(Collider2D RZ)
    {
        if (!QD)
            if (!CW)
            {
                EY(RZ.gameObject);
            }
    }
}
