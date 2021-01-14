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

public enum q { w, e, r, t };
public enum y { u, i, o, p, a, s, d, f, g, h, j, k };
public enum l { z, x, c, v };
/// <summary>
///base class for enemies to inherit from with logic for detection, patrole, movment, stats managment
/// </summary>
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform b; //start position of the ray cast
    public Transform n; //secondary rey cast for better detection neer walls
    public PolygonCollider2D m; // the collder cone used for player detection
    public bool Q = false; //has the player been detected   
    public q W = q.w;//current state of teh enemy   
    public y E;
    public GameObject R;
    public GameObject T;
    public Inventory_JoaoBeijinho Y; 
    public float U;

    public GameObject I; // itme that i sdropped when enemie dies
    
    public float O; //enemy health with getter and setter
   
    public float P; //movment speed


    
    public bool A = false;
  
    public Tilemap S;
 
    public Transform[] D;
   
    public float F;
    
    public float G;
   
    public bool H;
    
    public float J;
    
    private float K = 0.5f;

    
    public float L;
    
    public float Z;
   
    public float X;
    public float C = 1f;
    
    public float V = 2;

    
    public float B = 0.3f;
   
    public float N = 3;
   
    public bool M = true;
    
    public float qq = 1.5f;

    internal float qw; //max amount of health an enemy has

    private Pathfinder_SebastianMol qe;
    protected List<Vector2Int> qr = new List<Vector2Int>();
    protected Transform qt; //used to get player position can be null if undedteceted
    protected float qy; //player scale at start
    private Vector3 qu; //the last position the enemy was at
    protected Vector3 qi;//the starting position of the enemy
    protected float qo; //timer for attack deley
    protected float qp; //timer for line of sight check
    protected int qa = 0; //iterated through patrole points
    protected float qs;
    private int qd; //the max for teh iterator so it dosent go out of range  
    private float qf; // timer for waiting at each patrole pos
    private Transform qg; //the current patrole pos were haeding to / are at 
    private Vector3 qh; //last given to the path finder to find a path e.g. player position
    protected bool qj = false; //used to stunn the enemy
    private float qk; //used to remeber m_lookLeftAndRightTimer varaibale at the start for later resents
    private bool ql = false; // if the enemy serching for player
    private Vector3 qz;  //the point of curiosity for an enemy to cheak
    protected int qx = 1; //the phase tadashi is on


    private HitEffectElliott qc;
    private CameraShakeElliott qv;
    protected bool qb = false;
    private PlayerSpoted_Elliott qn;
    

    protected PlayerStealth_JoaoBeijinho qm;
    private int QQ = 1 << 8;
    /// <summary>
    /// abstract class used to provied the logic for the wonder state
    /// </summary>
    private void QW()
    {
        if (H)
        {
            QE();
        }
        else
        {
            m.enabled = true;
            if (Vector2.Distance(transform.position, qi) > 0.5f)
            {
                QR(qi);
            }

            if(Vector2.Distance(transform.position, qi) <= 0.5f) transform.localScale = new Vector3(qy, transform.localScale.y, transform.localScale.z);

        }
        qb = false;
    }

    /// <summary>
    /// abstract class used to provied the logic for the chase state
    /// </summary>
    private void QT()
    {
        if (QY()) // if you can see player
        {
            if (Vector2.Distance(transform.position, qt.position) < X / C) //if the player is in range
            {
                QU(false);
                W = q.r;
            }
            else// if the player is out fo range
            {
                QR(qt.position);
            }
        }
        else
        {
            QI();
            ql = true;
            if (qr.Count == 0)
            {
                if (qp <= 0)
                {
                    ql = false;
                    W = q.w;
                    Q = false;
                }
                else
                {
                    QO();
                    qp -= Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// contains logic for when the enemy is serching for the player it looks left and right every half second
    /// </summary>
    private void QO()
    {
        if(K <= 0)
        {
            if(transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-qy, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(qy, transform.localScale.y, transform.localScale.z);
            }
            K = qk;
        }
        else
        {
            K -= Time.deltaTime;
        }
    }

    /// <summary>
    /// abstract class used to provied the logic for the attack state
    /// </summary>
    abstract internal void QP();

    private void QA()
    {
        if (QY())
        {
            if (Vector2.Distance(transform.position, qt.position) < X)
            {
                QP();

            }
            else
            {
                W = q.e;
            }

            qp = J;
            Q = true;

        }
        else
        {
            Q = false;
            if (qp <= 0)
            {
                W = q.w;
            }
            else
            {
                qp -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// a state that occurs when enemy is forced to look at a specific location
    /// </summary>
    private void QS()
    {
        if (Vector2.Distance( transform.position, qz) < 1 ) 
        if (qp <= 0)
        {
            W = q.w;
            qp = J;
        }
        else
        {
            qp -= Time.deltaTime;
            QI();
        }
    }

    /// <summary>
    /// contains the switch that stores the dofferent behavoiurs the enemy dose in each state
    /// </summary>
    private void QD()
    {
        switch (W)
        {
            case q.w:
                QW();
                break;
            case q.e:
                QT();
                break;
            case q.r:
                QA();
                break;
            case q.t:
                QS();
                break;
        }
    }


    /// <summary>
    /// detect player in vision cone the establishes line of sight
    /// </summary>
    /// <param name="QH"> the collion date from the onTrigger functions</param>
    protected void QF(GameObject QH)
    {
        if(QH.CompareTag("Player")) // is it a the player 
        {

            PlayerStealth_JoaoBeijinho QG = QH.GetComponent<PlayerStealth_JoaoBeijinho>();

            if (E == y.d)
            {
                if(!QG.QJ())
                {
                    QK(QH);
                }          
            }
            else if(!QG.QL()) //is the player in stealth/
            {
                QK(QH);
            }
        }
    }

    /// <summary>
    /// holds the logic for casting a ray when the player is first detected
    /// </summary>
    /// <param name="QL"> a collsion that is checked to see if it is the player</param>
    private void QK(GameObject QL)
    {
        m.enabled = false;
        RaycastHit2D QZ = Physics2D.Linecast(b.position, QL.transform.position);

        //RaycastHit2D crouchedHit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position);
        //Debug.DrawLine(m_rayCastStart.position, col.transform.position, Color.green);

        if (!qm.QX() && QZ.collider.gameObject.CompareTag(Tags_JoaoBeijinho.QC)) //player is not crouched and it hits him
        {
            //  m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Detection);
            Q = true;
            qt = QZ.transform;
            W = q.r;
            QV();
            QU();
        }
        else
        {
            m.enabled = true;
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
    protected bool QY()
    {
        if (qt)
        {
            m.enabled = false;
            RaycastHit2D QB = Physics2D.Linecast(b.position, qt.position);

            if (QB.collider.gameObject.CompareTag("Player"))
            {
                m.enabled = false;
                return true;
            }
            else
            {

                RaycastHit2D QN = Physics2D.Linecast(n.position, qt.position);


                if (QN.collider.gameObject.CompareTag("Player"))
                {
                    m.enabled = false;
                    return true;
                }
                else
                {
                    m.enabled = true;
                    return false;
                }
            }
        }
        return false;
    }



    /// <summary>
    /// set the position to move to in world coords
    /// </summary>
    /// <param name="WQ">the destination of the enemy</param>
    protected void QM(Vector3 WQ)
    {
        Vector2Int WW = (Vector2Int)qe.WE.WorldToCell(transform.position);
        Vector2Int WR = (Vector2Int)qe.WE.WorldToCell(WQ);
        qr = qe.k(WW, WR);

        if(A)
        {
            for (int i = 0; i < qr.Count; i++)
            {
                S.SetTileFlags(new Vector3Int(qr[i].x, qr[i].y, 0), TileFlags.None);
                S.SetColor(new Vector3Int(qr[i].x, qr[i].y, 0), Color.green);
                //Debug.Log(floortilemap.GetCellCenterWorld(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0)));
            }
        }
    }

    /// <summary>
    /// make the enemy follow the path it has created
    /// </summary>
    protected void WT()
    {
        if (qr.Count == 0) return;//skip check

        float WY = P * Time.deltaTime; //movment allowed this frame

        while (WY > 0)
        {
            //gets the center of the target tile
            Vector2 WU = (Vector2)qe.WE.CellToWorld(new Vector3Int( qr[0].x, qr[0].y, 0)) + ((Vector2)qe.WE.cellSize / 2);
            WU += new Vector2(-0.5f, 0);
            float WI = Vector2.Distance(transform.position, WU); //distance to the target tile center               
               
            if (WI > WY)//if all movment is used up in one move
            {
                transform.position = Vector2.MoveTowards(transform.position, WU, WY);//move
                WY = 0;
            }
            else //if theres movment left over after the next tile
            {
                transform.position = Vector2.MoveTowards(transform.position, WU, WI);//move
                WY -= WI;
                if (A) S.SetColor(new Vector3Int(qr[0].x, qr[0].y, 0), Color.white);
                qr.RemoveAt(0);
                if (qr.Count == 0) break;
            }            
        }
    }

    /// <summary>
    /// clears the path and optionaly adds one last step to center the enemy
    /// </summary>
    /// <param name="WO">desides weather to center the enemy at the end of the path</param>
    protected void QU(bool WO = true) // use this to stop walking
    {
        if (A)
        {
            for (int i = 0; i < qr.Count; i++)
            {
                S.SetColor(new Vector3Int(qr[i].x, qr[i].y, 0), Color.white);
            }
        }
        qr.Clear();
        if(WO) qr.Add((Vector2Int)qe.WE.WorldToCell(transform.position));
        qh = Vector3.zero;
    }

    /// <summary>
    /// holds logic for patrole functtionality.
    /// </summary>
    protected void QE()
    {
        if (qg == null) return;
        //not sure what this dose might be a bit late at night, maybe initilaziation???? it just works
        if (qg.position.x == 0 && qg.position.y == 0) qg = D[0];
        //if theres no were to go go to the first patrole point
        if (qr.Count == 0) QM(D[qa].position);
        //if close neough to the next patrole point start to swap patrole points
        if (Vector2.Distance(transform.position, D[qa].position) <= 0.4f) WP();
        //start the patrole
        WT();
    }

    /// <summary>
    /// loops through a list of patrole points to set the enemies next destination
    /// </summary>
    protected void WP()
    {
        if(D.Length > 0) //are there any patrole points
        {
            if (qf <= 0) //if the timer has reached 0 start the swap
            {
                if (qa == qd) // see if the itterator is at its limit
                {
                    qa = 0; //set to first iteration
                    qg = D[qa]; //move to destination
                }
                else// else go through the list 
                {
                    qg = D[++qa];
                }
                qf = G; //reset the timer
            }
            else
            {
                qf -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// used to move the player
    /// </summary>
    /// <param name="pos">where you wnaty the player to be moved</param>
    protected void QR(Vector3 pos)
    {
        if (qr.Count == 0)
        {
            QM(pos);
        }

        if (qh == Vector3.zero) // if there hasent been an old pos 
        {
            qh = pos;
        }

        if (Vector2.Distance(qh, pos) > F) //if players old pos is a distance away to his new pos go to new pos
        {
            QU();
            QM(pos);
        } 
        
        WT();
    }


    /// <summary>
    /// actions that happen before enemy death.
    /// </summary>
    public void WA() 
    {
        if(O <= 0)
        {
            PlayTrack_Jann.Instance.WS(AudioFiles.c);
            if (I)
            {
                Instantiate(I, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
            Y.WD();
        }    
    }

    /// <summary>
    /// controls the scale of the enemy based on player or direction
    /// </summary>
    protected void WF()
    {
        if(!ql)
        if(Q)
        {
            if(qt.position.x > transform.position.x)
            {
                transform.localScale = new Vector3( -qy, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(qy, transform.localScale.y, transform.localScale.z);
            }

        }
        else
        {
            if(qu.x > transform.position.x)
            {
                transform.localScale = new Vector3(qy, transform.localScale.y, transform.localScale.z);
            }
            else if(qu.x < transform.position.x)
            {
                transform.localScale = new Vector3(-qy, transform.localScale.y, transform.localScale.z);
            }
            qu = transform.position;
        }
    }

    /// <summary>
    /// used to apply correct damage amaount and type based on enemy type
    /// </summary>
    /// <param name="WH">what type of damage e.g. melee</param>
    /// <param name="WJ">amount of damage</param>
    public void WG(l WH, float WJ)
    {
        
        switch (E)
        {
            case y.u:
                WK(WJ);
                break;

            case y.i:
                if (WH == l.z)
                {
                    WK(WJ * 1.5f);
                }
                else
                {
                    WK(WJ);
                }   
                break;

            case y.k:

                break;

            case y.o:
                if(qt.position.x < transform.position.x) //player on the left
                {
                    if(transform.localScale.x < 0) WK(WJ); //looking left
                }
                else if(qt.position.x > transform.position.x) //player on the right
                {
                    if (transform.localScale.x > 0) WK(WJ); //looking right
                }
                else
                {
                    WK(WJ*0.25f);
                }
                break;

            case y.a:
                //half damage melle when not sneaked
                if (WH == l.z)
                {
                    if(Q == false) // in stealth
                    {
                        WK(WJ); // normal melle stealth attack
                    }
                    else // not stealth 
                    {
                        WK(WJ * 0.5f); //half damage stealth atatck
                    }                  
                }
                else
                {
                    WK(WJ); // normal
                }
                break;

            case y.s:
            case y.p:
                //sneak multiplier
                if (Q == false)
                {
                    O -= WJ * (L + Z);
                    qc.WL(true);

                }
                else
                {
                    O -= WJ;
                    qc.WL(false);

                }
                break;

            case y.h:             
                if ((O / qw) <= B) //second fase
                {
                    WK(WJ);
                    qo *= qq;
                    M = false;
                    Z = 0;
                }
                else
                {
                     //sneak multiplier
                    if (Q == false)
                    {
                        O -= WJ * (L + Z);
                        qc.WL(true);
                    }
                    else
                    {
                        O -= WJ;
                        qc.WL(false);
                    }
                }
                break;

            case y.j:
                float WZ = O / qw;
                if (WZ > 0.6f)
                {
                    qx = 1;
                    WK(WJ);
                }
                else if( WZ > 0.3f)
                {
                    qx = 2;
                    O -= WJ;
                }
                else//health below 30
                {
                    qx = 3;
                    O -= WJ;
                }

                break;

            default:
                WK(WJ);
                break;

        }

        W = q.e;

        WA();//checks to see if enemy is dead 
    }

    /// <summary>
    /// stop the enemys movment and attack, makes it look liek enemy ios tsunned but can stil be damaged
    /// </summary>
    private void WX()
    {
        qj = !qj;
    }

    /// <summary>
    /// stund the enemy for an amount of time in seconds
    /// </summary>
    /// <param name="WV"> the amaount of time the enemy is stunend for</param>
    /// <returns></returns>
    public IEnumerator WC(float WV)
    {
        WX();
        yield return new WaitForSeconds(WV);
        WX();
    }

    /// <summary>
    /// for use with distraction projectile to stun enemies
    /// </summary>
    /// <param name="WN">amaount of time to stun in seconds</param>
    public void WB(float WN)
    {
        if(E != y.f) StartCoroutine(WC(WN));
    }

    /// <summary>
    /// for use with ligths to stun enemies
    /// </summary>
    /// <param name="EQ">amaount of time to stun in seconds</param>
    public void WM(float EQ)
    {
        StartCoroutine(WC(EQ));
    }

    /// <summary>
    /// normal way of enemy takign damage
    /// </summary>
    /// <param name="EW">amount of damage</param>
    private void WK( float EW )
    {
        if (Q == false) //if sneak damage
        {
            O -= EW * L;
            qv.EE();
            qc.WL(true);
            Y.GiveItem(ItemType.NinjaPoints, 1);
        }
        else
        {
            O -= EW;
            qc.WL(false);
        }
    }

    /// <summary>
    /// forse enemy to look at a certain position and serch for the player
    /// </summary>
    /// <param name="ET">the postion that you want the enemy to search</param>
    public void ER(Vector3 ET)
    {
        QU(false);
        W = q.t;
        qz = ET;
        QR(qz);
    }

    /// <summary>
    /// effect that happens when enemy noticese the player
    /// </summary>
    public void QV()
    {
       GameObject EY = Instantiate(T, new Vector3(transform.position.x,transform.position.y + U,transform.position.z), Quaternion.identity);
        EY.transform.parent = transform;
        
    }

    public void QI()
    {
        if (!qb)
        {
            GameObject A = Instantiate(R, new Vector3(transform.position.x, transform.position.y + U, transform.position.z), Quaternion.identity);
            A.transform.parent = transform;
            qb = true;
           
        }
    }

    public void EU()
    {
        transform.position = qi;
        ql = false;
        W = q.w;
        Q = false;
        qp = J;
        QI();
    }


    private void Start()
    {
        qe = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        qy = transform.localScale.x;
        qu = transform.position;
        qi = transform.position;
        qd = D.Length-1;
        qf = G;
        if (D.Length > 0) qg = D[0];
        qk = K;
        qw = O;
        qp = J;
        qs = X;

        Y = GameObject.Find("Player").GetComponent<Inventory_JoaoBeijinho>();
        qm = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        qc = GetComponent<HitEffectElliott>();
        qv = Camera.main.GetComponent<CameraShakeElliott>();
    }

    private void Update()
    {

        if(!qj)
        {
            QD(); // behaviour of the enemy what stste it is in and what it dose
            WT(); //walk the path that the enemy currently has  
            WF(); //chnge the scale of the player
        }

    }

    private void OnTriggerEnter2D(Collider2D EI)
    {
        if(!qj)
            if(!Q)
            {
                QF(EI.gameObject);
            }      
    }

    private void OnTriggerStay2D(Collider2D EO)
    {
        if (!qj)
            if (!Q)
            {
                QF(EO.gameObject);
            }
    }
}
