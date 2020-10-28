using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

//sebastian mol
//base class for enemies to inherit from with some functionality.
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
    public bool showPath = false;
    public Tilemap floortilemap;

    private Pathfinder_SebastianMol m_pathfinder;
    protected List<Vector2Int> m_currentPath = new List<Vector2Int>();
    protected Transform m_playerTransform; //used to get playe position can be null if undedteceted
    private float m_scale;
    private Vector3 m_lastPos;


    abstract internal void EnemyBehaviour();
    protected void PlayerDetection(GameObject collision) //detect player in vision cone the establishes line of sight
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.GetComponent<PlayerStealth_JoaoBeijinho>().IsStealthed() == false)
            {
                m_detectionCollider.enabled = false;
                RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, collision.transform.position);
                Debug.DrawLine(m_rayCastStart.position, collision.transform.position, Color.red);

                if (hit.collider.gameObject.CompareTag("Player"))
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
    protected void Death() //actions that happen befor enemy death
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
    protected void IsPlayerDetected()
    {
        if (m_playerDetected == true)
        {
            //currentState = state.CHASE;
            //detectionCollider.enabled = false;
        }
    }

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

    protected void ClearPath() // use this to stop walking
    {
        if (showPath)
        {
            for (int i = 0; i < m_currentPath.Count; i++)
            {
                floortilemap.SetColor(new Vector3Int(m_currentPath[i].x, m_currentPath[i].y, 0), Color.white);
            }
        }
        m_currentPath.Clear();
        m_currentPath.Add((Vector2Int)m_pathfinder.m_tileMap.WorldToCell(transform.position));
    }

    protected bool IsPlayerInLineOfSight() // can you see the player
    {
        m_detectionCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, m_playerTransform.position);
        Debug.DrawLine(m_rayCastStart.position, m_playerTransform.position, Color.red);

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            m_detectionCollider.enabled = true;
            return false;
        }
        
    }

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

    public void TakeDamage(float damage)
    {
        //m_health -= m_DamageToTake;
        m_health -= damage;
    }

    private void Start()
    {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
        m_scale = transform.localScale.x;
        m_lastPos = transform.position;
    }

    private void Update()
    {
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
        FollowPath();
        SwapDirections();
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
