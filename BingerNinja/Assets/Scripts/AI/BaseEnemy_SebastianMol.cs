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

public enum state { WONDER, CHASE, ATTACK, CURIOUS };
public enum m_enemyType { NORMAL, CHEF, BARISTA, INTERN, NINJA, BUSSINESMAN, PETTIGER, ALIEN, TIGERBOSS, SPACENINJABOSS, TADASHI, CHEFBOSS };
public enum m_damageType { MELEE, RANGE, SNEAK, STUN };
/// <summary>
///base class for enemies to inherit from with logic for detection, patrole, movment, stats managment
/// </summary>
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform m_rayCastStart; //start position of the ray cast
    public Transform m_rayCastStartBackup; //secondary rey cast for better detection neer walls
    public PolygonCollider2D m_detectionCollider; // the collder cone used for player detection
    public bool m_playerDetected = false; //has the player been detected   
    public state m_currentState = state.WONDER;//current state of teh enemy   
    public m_enemyType m_currentEnemyType;
    public GameObject m_questionmark;
    public GameObject m_excalmationmark;
    public Inventory_JoaoBeijinho m_inventory; 
    public float m_notifcaionOffset;

    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    
    public float m_health; //enemy health with getter and setter
   
    public float m_speed; //movment speed


    
    public bool showPath = false;
  
    public Tilemap floortilemap;
 
    public Transform[] m_patrolPoints;
   
    public float m_playerMoveAllowance;
    
    public float m_deleyBetweenPatrol;
   
    public bool m_dosePatrole;
    
    public float m_outOfSightDeley;
    
     float m_lookLeftAndRightTimer = 0.5f;

    
    public float m_sneakDamageMultiplier;
    
    public float m_sneakDamageMultiplierStack;
   
    public float m_attackRange;
    public float m_attckRangeDevider = 1f;
    
    public float m_tiggerBossLooseTargetDistance = 2;

    
    public float m_secondPhaseStartPercentage = 0.3f;
   
    public float m_amountOfStunWhenPlayerStealthed = 3;
   
    public bool m_doseAffect = true;
    
    public float m_attackSpeedIncrease = 1.5f;

    internal float m_maxHealth; //max amount of health an enemy has

     Pathfinder_SebastianMol q;
    protected List<Vector2Int> m_currentPath = new List<Vector2Int>();
    protected Transform m_playerTransform; //used to get player position can be null if undedteceted
    protected float m_scale; //player scale at start
     Vector3 w; //the last position the enemy was at
    protected Vector3 m_startPos;//the starting position of the enemy
    protected float m_attackTimer; //timer for attack deley
    protected float m_outOfSightTimer; //timer for line of sight check
    protected int m_patrolIterator = 0; //iterated through patrole points
    protected float m_maxAttackRange;
     int e; //the max for teh iterator so it dosent go out of range  
     float r; // timer for waiting at each patrole pos
     Transform t; //the current patrole pos were haeding to / are at 
     Vector3 y; //last given to the path finder to find a path e.g. player position
    protected bool m_isStuned = false; //used to stunn the enemy
     float u; //used to remeber m_lookLeftAndRightTimer varaibale at the start for later resents
     bool i = false; // if the enemy serching for player
     Vector3 o;  //the point of curiosity for an enemy to cheak
    protected int m_tadashiPhase = 1; //the phase tadashi is on


     HitEffectElliott a;
     CameraShakeElliott s;
    protected bool m_showquestionMarkonce = false;
     PlayerSpoted_Elliott d;
    

    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;
     int f = 1 << 8;
    /// <summary>
    /// abstract class used to provied the logic for the wonder state
    /// </summary>
     void g()
    {
        if (m_dosePatrole)
        {
            Patrol();
        }
        else
        {
            m_detectionCollider.enabled = true;
            if (Vector2.Distance(transform.position, m_startPos) > 0.5f)
            {
                PathfindTo(m_startPos);
            }

            if(Vector2.Distance(transform.position, m_startPos) <= 0.5f) transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);

        }
        m_showquestionMarkonce = false;
    }

    /// <summary>
    /// abstract class used to provied the logic for the chase state
    /// </summary>
     void h()
    {
        if (IsPlayerInLineOfSight()) // if you can see player
        {
            if (Vector2.Distance(transform.position, m_playerTransform.position) < m_attackRange / m_attckRangeDevider) //if the player is in range
            {
                ClearPath(false);
                m_currentState = state.ATTACK;
            }
            else// if the player is out fo range
            {
                PathfindTo(m_playerTransform.position);
            }
        }
        else
        {
            OnceLostContactEffect();
            i = true;
            if (m_currentPath.Count == 0)
            {
                if (m_outOfSightTimer <= 0)
                {
                    i = false;
                    m_currentState = state.WONDER;
                    m_playerDetected = false;
                }
                else
                {
                    j();
                    m_outOfSightTimer -= Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// contains logic for when the enemy is serching for the player it looks left and right every half second
    /// </summary>
    void j()
    {
        if(m_lookLeftAndRightTimer <= 0)
        {
            if(transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-m_scale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);
            }
            m_lookLeftAndRightTimer = u;
        }
        else
        {
            m_lookLeftAndRightTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// abstract class used to provied the logic for the attack state
    /// </summary>
    abstract internal void AttackBehaviour();

     void k()
    {
        if (IsPlayerInLineOfSight())
        {
            if (Vector2.Distance(transform.position, m_playerTransform.position) < m_attackRange)
            {
                AttackBehaviour();

            }
            else
            {
                m_currentState = state.CHASE;
            }

            m_outOfSightTimer = m_outOfSightDeley;
            m_playerDetected = true;

        }
        else
        {
            m_playerDetected = false;
            if (m_outOfSightTimer <= 0)
            {
                m_currentState = state.WONDER;
            }
            else
            {
                m_outOfSightTimer -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// a state that occurs when enemy is forced to look at a specific location
    /// </summary>
     void l()
    {
        if (Vector2.Distance( transform.position, o) < 1 ) 
        if (m_outOfSightTimer <= 0)
        {
            m_currentState = state.WONDER;
            m_outOfSightTimer = m_outOfSightDeley;
        }
        else
        {
            m_outOfSightTimer -= Time.deltaTime;
            OnceLostContactEffect();
        }
    }

    /// <summary>
    /// contains the switch that stores the dofferent behavoiurs the enemy dose in each state
    /// </summary>
     void z()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                g();
                break;
            case state.CHASE:
                h();
                break;
            case state.ATTACK:
                k();
                break;
            case state.CURIOUS:
                l();
                break;
        }
    }


    /// <summary>
    /// detect player in vision cone the establishes line of sight
    /// </summary>
    /// <param name="collision"> the collion date from the onTrigger functions</param>
    protected void PlayerDetection(GameObject collision)
    {
        if(collision.CompareTag("Player")) // is it a the player 
        {

            PlayerStealth_JoaoBeijinho playerStealth = collision.GetComponent<PlayerStealth_JoaoBeijinho>();

            if (m_currentEnemyType == m_enemyType.PETTIGER)
            {
                if(!playerStealth.IsinVent())
                {
                    x(collision);
                }          
            }
            else if(!playerStealth.IsStealthed()) //is the player in stealth/
            {
                x(collision);
            }
        }
    }

    /// <summary>
    /// holds the logic for casting a ray when the player is first detected
    /// </summary>
    /// <param name="col"> a collsion that is checked to see if it is the player</param>
     void x(GameObject col)
    {
        m_detectionCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position);
        Debug.DrawLine(m_rayCastStart.position, col.transform.position, Color.red);

        //RaycastHit2D crouchedHit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position);
        //Debug.DrawLine(m_rayCastStart.position, col.transform.position, Color.green);

        if (!m_playerStealthScript.IsCrouched() && hit.collider.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)) //player is not crouched and it hits him
        {
            //  m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Detection);
            m_playerDetected = true;
            m_playerTransform = hit.transform;
            m_currentState = state.ATTACK;
            NoticePlayerEffect();
            ClearPath();
        }
        else
        {
            m_detectionCollider.enabled = true;
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
    protected bool IsPlayerInLineOfSight()
    {
        if (m_playerTransform)
        {
            m_detectionCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, m_playerTransform.position);
            Debug.DrawLine(m_rayCastStart.position, m_playerTransform.position, Color.red);

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                m_detectionCollider.enabled = false;
                return true;
            }
            else
            {

                RaycastHit2D hitTwo = Physics2D.Linecast(m_rayCastStartBackup.position, m_playerTransform.position);
                Debug.DrawLine(m_rayCastStartBackup.position, m_playerTransform.position, Color.red);

                if (hitTwo.collider.gameObject.CompareTag("Player"))
                {
                    m_detectionCollider.enabled = false;
                    return true;
                }
                else
                {
                    m_detectionCollider.enabled = true;
                    return false;
                }
            }
        }
        return false;
    }



    /// <summary>
    /// set the position to move to in world coords
    /// </summary>
    /// <param name="destentaion">the destination of the enemy</param>
    protected void MoveToWorldPos(Vector3 destentaion)
    {
        Vector2Int tileMapSpaceStart = (Vector2Int)q.m_tileMap.WorldToCell(transform.position);
        Vector2Int tileMapSpaceTarget = (Vector2Int)q.m_tileMap.WorldToCell(destentaion);
        m_currentPath = q.PathFind(tileMapSpaceStart, tileMapSpaceTarget);

        if(showPath)
        {
            for (int i = 0; i < m_currentPath.Count; i++)
            {
                floortilemap.SetTileFlags(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0), TileFlags.None);
                floortilemap.SetColor(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0), Color.green);
                //Debug.Log(floortilemap.GetCellCenterWorld(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0)));
            }
        }
    }

    /// <summary>
    /// make the enemy follow the path it has created
    /// </summary>
    protected void FollowPath()
    {
        if (m_currentPath.Count == 0) return;//skip check

        float movementLeftOver = m_speed * Time.deltaTime; //movment allowed this frame

        while (movementLeftOver > 0)
        {
            //gets the center of the target tile
            Vector2 targetTileCenter = (Vector2)q.m_tileMap.CellToWorld(new Vector3Int( m_currentPath[0].x, m_currentPath[0].y, 0)) + ((Vector2)q.m_tileMap.cellSize / 2);
            targetTileCenter += new Vector2(-0.5f, 0);
            float distance = Vector2.Distance(transform.position, targetTileCenter); //distance to the target tile center               
               
            if (distance > movementLeftOver)//if all movment is used up in one move
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTileCenter, movementLeftOver);//move
                movementLeftOver = 0;
            }
            else //if theres movment left over after the next tile
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTileCenter, distance);//move
                movementLeftOver -= distance;
                if (showPath) floortilemap.SetColor(new Vector3Int(m_currentPath[0].x, m_currentPath[0].y, 0), Color.white);
                m_currentPath.RemoveAt(0);
                if (m_currentPath.Count == 0) break;
            }            
        }
    }

    /// <summary>
    /// clears the path and optionaly adds one last step to center the enemy
    /// </summary>
    /// <param name="goBackToCenter">desides weather to center the enemy at the end of the path</param>
    protected void ClearPath(bool goBackToCenter = true) // use this to stop walking
    {
        if (showPath)
        {
            for (int i = 0; i < m_currentPath.Count; i++)
            {
                floortilemap.SetColor(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0), Color.white);
            }
        }
        m_currentPath.Clear();
        if(goBackToCenter) m_currentPath.Add((Vector2Int)q.m_tileMap.WorldToCell(transform.position));
        y = Vector3.zero;
    }

    /// <summary>
    /// holds logic for patrole functtionality.
    /// </summary>
    protected void Patrol()
    {
        if (t == null) return;
        //not sure what this dose might be a bit late at night, maybe initilaziation???? it just works
        if (t.position.x == 0 && t.position.y == 0) t = m_patrolPoints[0];
        //if theres no were to go go to the first patrole point
        if (m_currentPath.Count == 0) MoveToWorldPos(m_patrolPoints[m_patrolIterator].position);
        //if close neough to the next patrole point start to swap patrole points
        if (Vector2.Distance(transform.position, m_patrolPoints[m_patrolIterator].position) <= 0.4f) SwapPatrolPoints();
        //start the patrole
        FollowPath();
    }

    /// <summary>
    /// loops through a list of patrole points to set the enemies next destination
    /// </summary>
    protected void SwapPatrolPoints()
    {
        if(m_patrolPoints.Length > 0) //are there any patrole points
        {
            if (r <= 0) //if the timer has reached 0 start the swap
            {
                if (m_patrolIterator == e) // see if the itterator is at its limit
                {
                    m_patrolIterator = 0; //set to first iteration
                    t = m_patrolPoints[m_patrolIterator]; //move to destination
                }
                else// else go through the list 
                {
                    t = m_patrolPoints[++m_patrolIterator];
                }
                r = m_deleyBetweenPatrol; //reset the timer
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
    protected void PathfindTo(Vector3 pos)
    {
        if (m_currentPath.Count == 0)
        {
            MoveToWorldPos(pos);
        }

        if (y == Vector3.zero) // if there hasent been an old pos 
        {
            y = pos;
        }

        if (Vector2.Distance(y, pos) > m_playerMoveAllowance) //if players old pos is a distance away to his new pos go to new pos
        {
            ClearPath();
            MoveToWorldPos(pos);
        } 
        
        FollowPath();
    }


    /// <summary>
    /// actions that happen before enemy death.
    /// </summary>
    public void OnDeath() 
    {
        if(m_health <= 0)
        {
            PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_Damage);
            if (m_dropItem)
            {
                Instantiate(m_dropItem, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
            m_inventory.CheckDeadEnemies();
        }    
    }

    /// <summary>
    /// controls the scale of the enemy based on player or direction
    /// </summary>
    protected void SwapDirections()
    {
        if(!i)
        if(m_playerDetected)
        {
            if(m_playerTransform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3( -m_scale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);
            }

        }
        else
        {
            if(w.x > transform.position.x)
            {
                transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);
            }
            else if(w.x < transform.position.x)
            {
                transform.localScale = new Vector3(-m_scale, transform.localScale.y, transform.localScale.z);
            }
            w = transform.position;
        }
    }

    /// <summary>
    /// used to apply correct damage amaount and type based on enemy type
    /// </summary>
    /// <param name="damageType">what type of damage e.g. melee</param>
    /// <param name="damage">amount of damage</param>
    public void TakeDamage(m_damageType damageType, float damage)
    {
        
        switch (m_currentEnemyType)
        {
            case m_enemyType.NORMAL:
                v(damage);
                break;

            case m_enemyType.CHEF:
                if (damageType == m_damageType.MELEE)
                {
                    v(damage * 1.5f);
                }
                else
                {
                    v(damage);
                }   
                break;

            case m_enemyType.CHEFBOSS:

                break;

            case m_enemyType.BARISTA:
                if(m_playerTransform.position.x < transform.position.x) //player on the left
                {
                    if(transform.localScale.x < 0) v(damage); //looking left
                }
                else if(m_playerTransform.position.x > transform.position.x) //player on the right
                {
                    if (transform.localScale.x > 0) v(damage); //looking right
                }
                else
                {
                    v(damage*0.25f);
                }
                break;

            case m_enemyType.NINJA:
                //half damage melle when not sneaked
                if (damageType == m_damageType.MELEE)
                {
                    if(m_playerDetected == false) // in stealth
                    {
                        v(damage); // normal melle stealth attack
                    }
                    else // not stealth 
                    {
                        v(damage * 0.5f); //half damage stealth atatck
                    }                  
                }
                else
                {
                    v(damage); // normal
                }
                break;

            case m_enemyType.BUSSINESMAN:
            case m_enemyType.INTERN:
                //sneak multiplier
                Debug.Log(m_playerDetected);
                if (m_playerDetected == false)
                {
                    m_health -= damage * (m_sneakDamageMultiplier + m_sneakDamageMultiplierStack);
                    a.StartHitEffect(true);

                }
                else
                {
                    m_health -= damage;
                    a.StartHitEffect(false);

                }
                break;

            case m_enemyType.SPACENINJABOSS:             
                if ((m_health / m_maxHealth) <= m_secondPhaseStartPercentage) //second fase
                {
                    v(damage);
                    m_attackTimer *= m_attackSpeedIncrease;
                    m_doseAffect = false;
                    m_sneakDamageMultiplierStack = 0;
                }
                else
                {
                     //sneak multiplier
                    if (m_playerDetected == false)
                    {
                        m_health -= damage * (m_sneakDamageMultiplier + m_sneakDamageMultiplierStack);
                        a.StartHitEffect(true);
                    }
                    else
                    {
                        m_health -= damage;
                        a.StartHitEffect(false);
                    }
                }
                break;

            case m_enemyType.TADASHI:
                float healthPercentage = m_health / m_maxHealth;
                if (healthPercentage > 0.6f)
                {
                    m_tadashiPhase = 1;
                    v(damage);
                }
                else if( healthPercentage > 0.3f)
                {
                    m_tadashiPhase = 2;
                    m_health -= damage;
                }
                else//health below 30
                {
                    m_tadashiPhase = 3;
                    m_health -= damage;
                }

                break;

            default:
                v(damage);
                break;

        }

        m_currentState = state.CHASE;

        OnDeath();//checks to see if enemy is dead 
    }

    /// <summary>
    /// stop the enemys movment and attack, makes it look liek enemy ios tsunned but can stil be damaged
    /// </summary>
     void c()
    {
        m_isStuned = !m_isStuned;
    }

    /// <summary>
    /// stund the enemy for an amount of time in seconds
    /// </summary>
    /// <param name="amountOfTime"> the amaount of time the enemy is stunend for</param>
    /// <returns></returns>
    public IEnumerator StunEnemyWithDeley(float amountOfTime)
    {
        c();
        yield return new WaitForSeconds(amountOfTime);
        c();
    }

    /// <summary>
    /// for use with distraction projectile to stun enemies
    /// </summary>
    /// <param name="amaountOfTime">amaount of time to stun in seconds</param>
    public void StunEnemyWithDeleyFunc(float amaountOfTime)
    {
        if(m_currentEnemyType != m_enemyType.ALIEN) StartCoroutine(StunEnemyWithDeley(amaountOfTime));
    }

    /// <summary>
    /// for use with ligths to stun enemies
    /// </summary>
    /// <param name="amaountOfTime">amaount of time to stun in seconds</param>
    public void StunEnemyWithLightsDeleyFunc(float amaountOfTime)
    {
        StartCoroutine(StunEnemyWithDeley(amaountOfTime));
    }

    /// <summary>
    /// normal way of enemy takign damage
    /// </summary>
    /// <param name="damage">amount of damage</param>
     void v( float damage )
    {
        if (m_playerDetected == false) //if sneak damage
        {
            m_health -= damage * m_sneakDamageMultiplier;
            s.StartShake();
            a.StartHitEffect(true);
            m_inventory.GiveItem(ItemType.NinjaPoints, 1);
        }
        else
        {
            m_health -= damage;
            a.StartHitEffect(false);
        }
    }

    /// <summary>
    /// forse enemy to look at a certain position and serch for the player
    /// </summary>
    /// <param name="pos">the postion that you want the enemy to search</param>
    public void ForceCuriosity(Vector3 pos)
    {
        ClearPath(false);
        m_currentState = state.CURIOUS;
        o = pos;
        PathfindTo(o);
    }

    /// <summary>
    /// effect that happens when enemy noticese the player
    /// </summary>
    public void NoticePlayerEffect()
    {
       GameObject excalm = Instantiate(m_excalmationmark, new Vector3(transform.position.x,transform.position.y + m_notifcaionOffset,transform.position.z), Quaternion.identity);
        excalm.transform.parent = transform;
        
    }

    public void OnceLostContactEffect()
    {
        if (!m_showquestionMarkonce)
        {
            GameObject A = Instantiate(m_questionmark, new Vector3(transform.position.x, transform.position.y + m_notifcaionOffset, transform.position.z), Quaternion.identity);
            A.transform.parent = transform;
            m_showquestionMarkonce = true;
           
        }
    }

    public void ForceLooseIntrest()
    {
        transform.position = m_startPos;
        i = false;
        m_currentState = state.WONDER;
        m_playerDetected = false;
        m_outOfSightTimer = m_outOfSightDeley;
        OnceLostContactEffect();
    }


     void Start()
    {
        q = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        m_scale = transform.localScale.x;
        w = transform.position;
        m_startPos = transform.position;
        e = m_patrolPoints.Length-1;
        r = m_deleyBetweenPatrol;
        if (m_patrolPoints.Length > 0) t = m_patrolPoints[0];
        u = m_lookLeftAndRightTimer;
        m_maxHealth = m_health;
        m_outOfSightTimer = m_outOfSightDeley;
        m_maxAttackRange = m_attackRange;

        m_inventory = GameObject.Find("Player").GetComponent<Inventory_JoaoBeijinho>();
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        a = GetComponent<HitEffectElliott>();
        s = Camera.main.GetComponent<CameraShakeElliott>();
    }

     void Update()
    {

        if(!m_isStuned)
        {
            z(); // behaviour of the enemy what stste it is in and what it dose
            FollowPath(); //walk the path that the enemy currently has  
            SwapDirections(); //chnge the scale of the player
        }

    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_isStuned)
            if(!m_playerDetected)
            {
                PlayerDetection(collision.gameObject);
            }      
    }

     void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_isStuned)
            if (!m_playerDetected)
            {
                PlayerDetection(collision.gameObject);
            }
    }
}
