//sebastian mol
//sebastian mol 30/10/2020 patrol now handles multiple patrol points
//sebastian mol 02/11/2020 revisions to comments
//sebastian mol 02/11/20 changed enemy behaviour funstion int AILogic function and created more abstract functions.
//sebastian mol 02/11/20 now path gets recalculated when player moves away from original position 
//sebastian mol 02/11/20 improved player detection with second raycast
//sebastian mol 06/11/20 new damage sysetm
//sebastian mol 11/11/2020 enemy can now be stunned
//sebastian mol 11/11/2020 tiger enemy cant see player in vent now

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;


/// <summary>
///base class for enemies to inherit from with logic for detection, patrole, movment, stats managment
/// </summary>
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform m_rayCastStart; //start position of the ray cast
    public Transform m_rayCastStartBackup; //secondary rey cast for better detection neer walls
    public PolygonCollider2D m_detectionCollider; // the collder cone used for player detection
    public bool m_playerDetected = false; //has the player been detected
    public enum state { WONDER, CHASE, ATTACK, RETREAT};
    public state m_currentState = state.WONDER;//current state of teh enemy
    public enum m_enemyType { NORMAL, CHEF, BARISTA, INTERN, NINJA, BUSSINESMAN, PETTIGER};
    public m_enemyType m_currentEnemyType;
    public enum m_damageType { MELEE, RANGE, SNEAK, STUN };

    [Header("designers Section")]
    [Tooltip("the item the enemy drops on death")]
    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    [Tooltip("health of the enemy")]
    public float m_health; //enemy health with getter and setter
    [Tooltip("speed of the enemy")]
    public float m_speed; //movment speed
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
    [Tooltip("the multiply for how much damage to take when enemy cant see player")]
    public float m_sneakDamageMultiplier;
    [Tooltip("the multiply for how much damage to take on enemie sthat take more sneka damage then normal thsi stacks additivley with the sneakDamageMultiplier")]
    public float m_sneakDamageMultiplierStack;

    private Pathfinder_SebastianMol m_pathfinder;
    protected List<Vector2Int> m_currentPath = new List<Vector2Int>();
    protected Transform m_playerTransform; //used to get player position can be null if undedteceted
    protected float m_scale; //player scale at start
    private Vector3 m_lastPos; //the last position the enemy was at
    protected Vector3 m_startPos;//the starting position of the enemy
    protected float m_attackTimer; //timer for attack deley
    protected float m_outOfSightTimer; //timer for line of sight check
    protected int m_patrolIterator = 0; //iterated through patrole points
    private int m_patrolIteratorMax; //the max for teh iterator so it dosent go out of range  
    private float m_patroleTimer; // timer for waiting at each patrole pos
    private Transform m_currentPatrolePos; //the current patrole pos were haeding to / are at 
    private Vector3 m_lastPathFinfToPos; //last given to the path finder to find a path e.g. player position
    private bool m_isStuned = false; //used to stunn the enemy

    #region behaviour tree
    /// <summary>
    /// abstract class used to provied the logic for the wonder state
    /// </summary>
    abstract internal void WonderState();

    /// <summary>
    /// abstract class used to provied the logic for the chase state
    /// </summary>
    abstract internal void ChaseState();

    /// <summary>
    /// abstract class used to provied the logic for the attack state
    /// </summary>
    abstract internal void AttackState();

    /// <summary>
    /// abstract class used to provied the logic for the retreat state
    /// </summary>
    abstract internal void RetreatState();

    /// <summary>
    /// contaisn the switch that stores the dofferent behavoiurs the enemy dose in each state
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
            case state.RETREAT:
                RetreatState();
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
        RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, col.transform.position);
        Debug.DrawLine(m_rayCastStart.position, col.transform.position, Color.red);

        if (hit.collider.gameObject.CompareTag("Player")) //did it hit the play first
        {
            //  m_audioManager.PlaySFX(AudioManager_LouieWilliamson.SFX.Detection);
            m_playerDetected = true;
            m_playerTransform = hit.transform;
            m_currentState = state.ATTACK;
            ClearPath();
        }
        else
        {
            m_detectionCollider.enabled = true;
        }
    }

    /// <summary>
    /// returns waeether the player is in line of sight of the enemy
    /// </summary>
    /// <returns>weather the player is in line of sight</returns>
    protected bool IsPlayerInLineOfSight()
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
                    m_patrolIterator++;
                    m_currentPatrolePos = m_patrolPoints[m_patrolIterator];
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
            if (m_dropItem != null)
            {
                Instantiate(m_dropItem, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }    
    }

    /// <summary>
    /// controls the scale of the enemy based on player or direction
    /// </summary>
    protected void SwapDirections()
    {
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

        }
    }

    /// <summary>
    /// stop the enemys movment and attack, makes it look liek enemy ios tsunned but can stil be damaged
    /// </summary>
    public void StunEnemy()
    {
        if(m_isStuned)
        {
            m_isStuned = false;
        }
        else
        {
            m_isStuned = true;
        }
        
    }

    /// <summary>
    /// stund the enemy for an amount of time in seconds
    /// </summary>
    /// <param name="amountOfTime"> the amaount of time the enemy is stunend for</param>
    /// <returns></returns>
    public IEnumerator StunEnemyWithDeley(float amountOfTime)
    {
        StunEnemy();
        yield return new WaitForSeconds(amountOfTime);
        StunEnemy();
    }

    private void NormalTakeDamage( float damage )
    {
        if (m_playerDetected == false) //if sneak damage
        {
            m_health -= damage * m_sneakDamageMultiplier;
        }
        else
        {
            m_health -= damage;
        }
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
    }

    private void Update()
    {
        if(!m_isStuned)
        {
            AILogic(); // behaviour of the enemy what stste it is in and what it dose
            FollowPath(); //walk the path that the enemy currently has
            SwapDirections(); //chnge the scale of the player
        }    
        OnDeath();//checks to see if enemy is dead 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_isStuned)
            if(m_playerDetected == false)
            {
                PlayerDetection(collision.gameObject);
            }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_isStuned)
            if (m_playerDetected == false)
            {
                PlayerDetection(collision.gameObject);
            }
    }
}
