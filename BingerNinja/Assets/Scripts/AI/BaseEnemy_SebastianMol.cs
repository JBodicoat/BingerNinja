//sebastian mol
//sebastian mol 30/10/2020 patrol now handles multiple patrol points

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;


/// <summary>
///base class for enemies to inherit from with some functionality.
/// </summary>
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform m_rayCastStart; //start position of the ray cast
    public PolygonCollider2D m_detectionCollider; // the collder cone used for player detection
    public bool m_playerDetected = false; //has the player been detected
    public enum state { WONDER, CHASE, ATTACK, RETREAT};
    public state m_currentState = state.WONDER;//current state of teh enemy

    [Header("designers Section")]
    [Tooltip("the item the enemy drops on death")]
    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    [Tooltip("health of the enemy")]
    public float m_health; //enemy health with getter and setter
    [Tooltip("speed of the enemy")]
    public float m_speed; //movment speed
    [Tooltip("amaount of damage an enemy takes")]
    public float m_DamageToTake;
    [Tooltip("shows the path the enemy is taking")]
    public bool showPath = false;
    [Tooltip("teh tile map u want to show the path onto")]
    public Tilemap floortilemap;
    [Tooltip("the positions in world space where the enemy patroles")]
    public Transform[] m_patrolPoints;

    private Pathfinder_SebastianMol m_pathfinder;
    protected List<Vector2Int> m_currentPath = new List<Vector2Int>();
    protected Transform m_playerTransform; //used to get player position can be null if undedteceted
    protected float m_scale; //player scale at start
    private Vector3 m_lastPos; //the last position the enemy was at
    protected Vector3 m_startPos;//the starting position of the enemy
    protected float m_attackTimer; //timer for attack deley
    protected float m_outOfSightTimer; //timer for line of sight check
    protected int m_patrolIterator = 0;
    private int m_patrolIteratorMax;
    public float m_deleyBetweenPatrol;
    private float m_patroleTimer;
    private Transform m_currentPatrolePos;

    /// <summary>
    /// abtract class for each enemys behaviour
    /// </summary>
    abstract internal void EnemyBehaviour();
    /// <summary>
    /// detect player in vision cone the establishes line of sight
    /// </summary>
    /// <param name="collision"> the collion date from the onTrigger functions</param>
    protected void PlayerDetection(GameObject collision)
    {
        if(collision.CompareTag("Player")) // is it a the player 
        {
            if(collision.GetComponent<PlayerStealth_JoaoBeijinho>().IsStealthed() == false) //is the player in stealth/
            {
                //cast the ray
                m_detectionCollider.enabled = false;
                RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, collision.transform.position);
                Debug.DrawLine(m_rayCastStart.position, collision.transform.position, Color.red);

                if (hit.collider.gameObject.CompareTag("Player")) //did it hit the play first
                {
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

        }
    }

    /// <summary>
    /// patroles in given points
    /// </summary>
    protected void Patrol()
    {
        if (m_currentPatrolePos == null) return;
        //not sure what this dose might be a bit late at night, maybe initilaziation????
        if (m_currentPatrolePos.position.x == 0 && m_currentPatrolePos.position.y == 0) m_currentPatrolePos = m_patrolPoints[0];
        //if theres no were to go go to the first patrole point
        if (m_currentPath.Count == 0) MoveToWorldPos(m_patrolPoints[m_patrolIterator].position);
        //if close neough to the next patrole point start to swap patrole points
        if (Vector2.Distance(transform.position, m_patrolPoints[m_patrolIterator].position) <= 0.4f) SwapPatrolPoints();
        //start the patrole
        FollowPath();
    }

    /// <summary>
    /// swaps the current patrole destination
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
    /// actions that happen before enemy death.
    /// </summary>
    protected void Death() 
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
    /// functionality for when the player is detected
    /// </summary>
    protected void IsPlayerDetected()
    {
        if (m_playerDetected == true)
        {
            //currentState = state.CHASE;
            m_detectionCollider.enabled = false;
        }
    }

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
    }

    /// <summary>
    /// can you see the player
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
            m_detectionCollider.enabled = true;
            return false;
        }
        
    }

    /// <summary>
    /// controls teh scale of the enemy based on player or direction
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

    /// <summary>
    /// used to make the enemy take damage
    /// </summary>
    /// <param name="damage">amount of damage to take</param>
    public void TakeDamage(float damage)
    {
        //m_health -= m_DamageToTake;
        m_health -= damage;
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
        FollowPath();
    }

    private void Start()
    {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        m_scale = transform.localScale.x;
        m_lastPos = transform.position;
        m_startPos = transform.position;
        m_patrolIteratorMax = m_patrolPoints.Length-1;
        m_patroleTimer = m_deleyBetweenPatrol;
        //if (m_patrolPoints[0] != null) m_currentPatrolePos = m_patrolPoints[0];
    }

    private void Update()
    {
        IsPlayerDetected(); // has the playeer been detected
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
        FollowPath(); //walk the path that the enemy currently has
        SwapDirections(); //chnge the scale of the player
        Death();//checks to see if enemy is dead 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_playerDetected == false)
        {
            PlayerDetection(collision.gameObject);
        }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_playerDetected == false)
        {
            PlayerDetection(collision.gameObject);
        }
    }
}
