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
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

public enum state { WONDER, CHASE, ATTACK, CURIOUS };
public enum m_enemyType { NORMAL, CHEF, BARISTA, INTERN, NINJA, BUSSINESMAN, PETTIGER, ALIEN, TIGERBOSS, SPACENINJABOSS, TADASHI };
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


    [Header("designers Section")]
    [Header("stats variables")]
    [Tooltip("the item the enemy drops on death")]
    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    [Tooltip("health of the enemy")]
    public float m_health; //enemy health with getter and setter
    [Tooltip("speed of the enemy")]
    public float m_speed; //movment speed

    [Header("enemie movment variables")]
    [Tooltip("shows the path the enemy is taking")]
    public bool showPath = false;
    [Tooltip("teh tile map u want to show the path onto")]
    public Tilemap floortilemap;
    [Tooltip("the positions in world space where the enemy patroles")]
    public Transform[] m_patrolPoints;
    [Tooltip("the amaout the player can move from his previous position befor new pathfinding is started")]
    public float m_playerMoveAllowance;
    [Tooltip("the deley in second whan at a patrol pos and waiting to go to the next")]
    public float m_deleyBetweenPatrol;
    [Tooltip("should the nemey patrole")]
    public bool m_dosePatrole;
    [Tooltip("deley between line of sight checks")]
    public float m_outOfSightDeley;
    [Tooltip("how fast the enemy looks left and right when serching for player")]
    private float m_lookLeftAndRightTimer = 0.5f;

    [Header("damage variables")]
    [Tooltip("the multiply for how much damage to take when enemy cant see player")]
    public float m_sneakDamageMultiplier;
    [Tooltip("the multiply for how much damage to take on enemie sthat take more sneka damage then normal thsi stacks additivley with the sneakDamageMultiplier")]
    public float m_sneakDamageMultiplierStack;
    [Tooltip("the disteance between th enemy and the player befor he starts attack")]
    public float m_attackRange;
    [Tooltip("for ranged enemies only how much to devide the attack range by befor starts attack")]
    [Range(1.0f, 1.5f)]
    public float m_attckRangeDevider = 1f;
    [Header("specific enemy variables")]
    [Tooltip("distance tiger bosss has to be away from player befor it dosen change direction to chase while charrging - ask seb if you ever need to change this")]
    public float m_tiggerBossLooseTargetDistance = 2;

    [Tooltip("with how much health left in a percentage, dose the enemy start second phase ")]
    [Range(0.0f, 1.0f)]
    public float m_secondPhaseStartPercentage = 0.3f;
    [Tooltip("amound of stun space ninja has when player gose stealth mode")]
    public float m_amountOfStunWhenPlayerStealthed = 3;
    [Tooltip("dose the enemy cuse affect on player hwen attacking")]
    public bool m_doseAffect = true;
    [Tooltip("multiplies how much the attack speed increases by in space ninja boss second fase")]
    public float m_attackSpeedIncrease = 1.5f;

    internal float m_maxHealth; //max amount of health an enemy has

    private Pathfinder_SebastianMol m_pathfinder;
    protected List<Vector2Int> m_currentPath = new List<Vector2Int>();
    protected Transform m_playerTransform; //used to get player position can be null if undedteceted
    protected float m_scale; //player scale at start
    private Vector3 m_lastPos; //the last position the enemy was at
    protected Vector3 m_startPos;//the starting position of the enemy
    protected float m_attackTimer; //timer for attack deley
    protected float m_outOfSightTimer; //timer for line of sight check
    protected int m_patrolIterator = 0; //iterated through patrole points
    protected float m_maxAttackRange;
    private int m_patrolIteratorMax; //the max for teh iterator so it dosent go out of range  
    private float m_patroleTimer; // timer for waiting at each patrole pos
    private Transform m_currentPatrolePos; //the current patrole pos were haeding to / are at 
    private Vector3 m_lastPathFinfToPos; //last given to the path finder to find a path e.g. player position
    protected bool m_isStuned = false; //used to stunn the enemy
    private float m_lookLeftAndRightTimerMax; //used to remeber m_lookLeftAndRightTimer varaibale at the start for later resents
    private bool m_isSerching = false; // if the enemy serching for player
    private Vector3 m_curiousTarget;  //the point of curiosity for an enemy to cheak
    protected int m_tadashiPhase = 1; //the phase tadashi is on


    private HitEffectElliott m_HitEffectElliott;
    private CameraShakeElliott m_cameraShake;
    protected bool m_showquestionMarkonce = false;
    private PlayerSpoted_Elliott playerSpoted_Elliott;
    

    protected PlayerStealth_JoaoBeijinho m_playerStealthScript;
    private int m_crouchObjectLayer = 1 << 8;

    private EnemyHealthBar healthBar;

    #region finite state machine
    /// <summary>
    /// abstract class used to provied the logic for the wonder state
    /// </summary>
    private void WonderState()
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
    private void ChaseState()
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
            m_isSerching = true;
            if (m_currentPath.Count == 0)
            {
                if (m_outOfSightTimer <= 0)
                {
                    m_isSerching = false;
                    m_currentState = state.WONDER;
                    m_playerDetected = false;
                }
                else
                {
                    LookLeftAndRight();
                    m_outOfSightTimer -= Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// contains logic for when the enemy is serching for the player it looks left and right every half second
    /// </summary>
    private void LookLeftAndRight()
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
            m_lookLeftAndRightTimer = m_lookLeftAndRightTimerMax;
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

    private void AttackState()
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
    private void CuriousState()
    {
        if (Vector2.Distance( transform.position, m_curiousTarget) < 1 ) 
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
    private void AILogic()
    {
        switch (m_currentState)
        {
            case state.WONDER:
                WonderState();
                break;
            case state.CHASE:
                ChaseState();
                break;
            case state.ATTACK:
                AttackState();
                break;
            case state.CURIOUS:
                CuriousState();
                break;
        }
    }
    #endregion

    #region player detection

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
                    PlayerDetectionRaycasLogic(collision);
                }          
            }
            else if(!playerStealth.IsStealthed()) //is the player in stealth/
            {
                PlayerDetectionRaycasLogic(collision);
            }
        }
    }

    /// <summary>
    /// holds the logic for casting a ray when the player is first detected
    /// </summary>
    /// <param name="col"> a collsion that is checked to see if it is the player</param>
    private void PlayerDetectionRaycasLogic(GameObject col)
    {
        m_detectionCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position, m_crouchObjectLayer);
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
        m_detectionCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, m_playerTransform.position, m_crouchObjectLayer);
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

    #endregion

    #region player movment
    /// <summary>
    /// set the position to move to in world coords
    /// </summary>
    /// <param name="destentaion">the destination of the enemy</param>
    protected void MoveToWorldPos(Vector3 destentaion)
    {
        Vector2Int tileMapSpaceStart = (Vector2Int)m_pathfinder.m_tileMap.WorldToCell(transform.position);
        Vector2Int tileMapSpaceTarget = (Vector2Int)m_pathfinder.m_tileMap.WorldToCell(destentaion);
        m_currentPath = m_pathfinder.PathFind(tileMapSpaceStart, tileMapSpaceTarget);

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
            Vector2 targetTileCenter = (Vector2)m_pathfinder.m_tileMap.CellToWorld(new Vector3Int( m_currentPath[0].x, m_currentPath[0].y, 0)) + ((Vector2)m_pathfinder.m_tileMap.cellSize / 2);
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
        if(goBackToCenter) m_currentPath.Add((Vector2Int)m_pathfinder.m_tileMap.WorldToCell(transform.position));
        m_lastPathFinfToPos = Vector3.zero;
    }

    /// <summary>
    /// holds logic for patrole functtionality.
    /// </summary>
    protected void Patrol()
    {
        if (m_currentPatrolePos == null) return;
        //not sure what this dose might be a bit late at night, maybe initilaziation???? it just works
        if (m_currentPatrolePos.position.x == 0 && m_currentPatrolePos.position.y == 0) m_currentPatrolePos = m_patrolPoints[0];
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
            if (m_patroleTimer <= 0) //if the timer has reached 0 start the swap
            {
                if (m_patrolIterator == m_patrolIteratorMax) // see if the itterator is at its limit
                {
                    m_patrolIterator = 0; //set to first iteration
                    m_currentPatrolePos = m_patrolPoints[m_patrolIterator]; //move to destination
                }
                else// else go through the list 
                {
                    m_currentPatrolePos = m_patrolPoints[++m_patrolIterator];
                }
                m_patroleTimer = m_deleyBetweenPatrol; //reset the timer
            }
            else
            {
                m_patroleTimer -= Time.deltaTime;
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

        if (m_lastPathFinfToPos == Vector3.zero) // if there hasent been an old pos 
        {
            m_lastPathFinfToPos = pos;
        }

        if (Vector2.Distance(m_lastPathFinfToPos, pos) > m_playerMoveAllowance) //if players old pos is a distance away to his new pos go to new pos
        {
            ClearPath();
            MoveToWorldPos(pos);
        } 
        
        FollowPath();
    }
    #endregion

    /// <summary>
    /// actions that happen before enemy death.
    /// </summary>
    protected void OnDeath() 
    {
        if(m_health <= 0)
        {
            PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_Death);
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
        if(!m_isSerching)
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
            if(m_lastPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(m_scale, transform.localScale.y, transform.localScale.z);
            }
            else if(m_lastPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(-m_scale, transform.localScale.y, transform.localScale.z);
            }
            m_lastPos = transform.position;
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
                NormalTakeDamage(damage);
                break;

            case m_enemyType.CHEF:
                if (damageType == m_damageType.MELEE)
                {
                    NormalTakeDamage(damage * 1.5f);
                }
                else
                {
                    NormalTakeDamage(damage);
                }   
                break;

            case m_enemyType.BARISTA:
                if(m_playerTransform.position.x < transform.position.x) //player on the left
                {
                    if(transform.localScale.x < 0) NormalTakeDamage(damage); //looking left
                }
                else if(m_playerTransform.position.x > transform.position.x) //player on the right
                {
                    if (transform.localScale.x > 0) NormalTakeDamage(damage); //looking right
                }
                else
                {
                    NormalTakeDamage(damage*0.25f);
                }
                break;

            case m_enemyType.NINJA:
                //half damage melle when not sneaked
                if (damageType == m_damageType.MELEE)
                {
                    if(m_playerDetected == false) // in stealth
                    {
                        NormalTakeDamage(damage); // normal melle stealth attack
                    }
                    else // not stealth 
                    {
                        NormalTakeDamage(damage * 0.5f); //half damage stealth atatck
                    }                  
                }
                else
                {
                    NormalTakeDamage(damage); // normal
                }
                break;

            case m_enemyType.BUSSINESMAN:
            case m_enemyType.INTERN:
                //sneak multiplier
                if (m_playerDetected == false)
                {
                    m_health -= damage * (m_sneakDamageMultiplier + m_sneakDamageMultiplierStack);
                   
                }
                else
                {
                    m_health -= damage;
                    
                }
                break;

            case m_enemyType.SPACENINJABOSS:             
                if ((m_health / m_maxHealth) <= m_secondPhaseStartPercentage) //second fase
                {
                    NormalTakeDamage(damage);
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
                    }
                    else
                    {
                        m_health -= damage;
                    }
                }
                break;

            case m_enemyType.TADASHI:
                float healthPercentage = m_health / m_maxHealth;
                if (healthPercentage > 0.6f)
                {
                    m_tadashiPhase = 1;
                    NormalTakeDamage(damage);
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
                NormalTakeDamage(damage);
                break;

        }

        OnDeath();//checks to see if enemy is dead 

        healthBar.UpdateHealth(m_health);
    }

    /// <summary>
    /// stop the enemys movment and attack, makes it look liek enemy ios tsunned but can stil be damaged
    /// </summary>
    private void StunEnemyToggle()
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
        StunEnemyToggle();
        yield return new WaitForSeconds(amountOfTime);
        StunEnemyToggle();
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
    private void NormalTakeDamage( float damage )
    {
        if (m_playerDetected == false) //if sneak damage
        {
            m_health -= damage * m_sneakDamageMultiplier;
            m_cameraShake.StartShake();
            m_HitEffectElliott.StartHitEffect(true);
            m_inventory.GiveItem(ItemType.NinjaPoints, 1);
        }
        else
        {
            m_health -= damage;
            m_HitEffectElliott.StartHitEffect(false);
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
        m_curiousTarget = pos;
        PathfindTo(m_curiousTarget);
    }

    /// <summary>
    /// effect that happens when enemy noticese the player
    /// </summary>
    public void NoticePlayerEffect()
    {
       Debug.Log("!");
       GameObject excalm = Instantiate(m_excalmationmark, new Vector3(transform.position.x,transform.position.y + m_notifcaionOffset,transform.position.z), Quaternion.identity);
        excalm.transform.parent = transform;
        
    }

    public void OnceLostContactEffect()
    {
        if (!m_showquestionMarkonce)
        {
            Debug.Log("?");
            GameObject A = Instantiate(m_questionmark, new Vector3(transform.position.x, transform.position.y + m_notifcaionOffset, transform.position.z), Quaternion.identity);
            A.transform.parent = transform;
            m_showquestionMarkonce = true;
           
        }
    }

    public void ForceLooseIntrest()
    {
        transform.position = m_startPos;
        m_isSerching = false;
        m_currentState = state.WONDER;
        m_playerDetected = false;
        m_outOfSightTimer = m_outOfSightDeley;
        OnceLostContactEffect();
    }


    private void Start()
    {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        m_scale = transform.localScale.x;
        m_lastPos = transform.position;
        m_startPos = transform.position;
        m_patrolIteratorMax = m_patrolPoints.Length-1;
        m_patroleTimer = m_deleyBetweenPatrol;
        if (m_patrolPoints.Length > 0) m_currentPatrolePos = m_patrolPoints[0];
        m_lookLeftAndRightTimerMax = m_lookLeftAndRightTimer;
        m_maxHealth = m_health;
        m_outOfSightTimer = m_outOfSightDeley;
        m_maxAttackRange = m_attackRange;

        m_inventory = GameObject.Find("Player").GetComponent<Inventory_JoaoBeijinho>();
        m_playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
        m_crouchObjectLayer = ~m_crouchObjectLayer;
        m_HitEffectElliott = GetComponent<HitEffectElliott>();
        m_cameraShake = Camera.main.GetComponent<CameraShakeElliott>();

        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Update()
    {

        if(!m_isStuned)
        {
            AILogic(); // behaviour of the enemy what stste it is in and what it dose
            FollowPath(); //walk the path that the enemy currently has  
            SwapDirections(); //chnge the scale of the player
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_isStuned)
            if(!m_playerDetected)
            {
                PlayerDetection(collision.gameObject);
            }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_isStuned)
            if (!m_playerDetected)
            {
                PlayerDetection(collision.gameObject);
            }
    }
}
