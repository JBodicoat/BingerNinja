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
    internal Transform m_playerTransform; //used to get playe position can be null if undedteceted
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

    private Pathfinder_SebastianMol m_pathfinder;
    internal List<Vector2Int> m_currentPath = new List<Vector2Int>();

    public Tilemap floortilemap;

    abstract internal void EnemyBehaviour();
    internal void PlayerDetection(GameObject collision) //detect player in vision cone the establishes line of sight
    {
        if(collision.CompareTag("Player"))
        {
            m_detectionCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(m_rayCastStart.position, collision.transform.position);
            Debug.DrawLine(m_rayCastStart.position, collision.transform.position, Color.red);

            if(hit.collider.gameObject.name == "Player")
            {
                Debug.Log("hit player");
                m_playerDetected = true;
                m_playerTransform = hit.transform;
                Debug.Log("state.attck");
                m_currentState = state.ATTACK;
            }
            else
            {
                m_detectionCollider.enabled = true;
                Debug.Log("hit " + hit.collider.gameObject.name);
            }
        }
    } 
    internal void Death() //actions that happen befor enemy death
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
    internal void IsPlayerDetected()
    {
        if (m_playerDetected == true)
        {
            //currentState = state.CHASE;
            //detectionCollider.enabled = false;
        }
    }

    internal void MoveToWorldPos(Vector3 destentaion)
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

    internal void FollowPath()
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

    internal void ClearPath() // use this to stop walking
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

    public void TakeDamage(float damage)
    {
        //m_health -= m_DamageToTake;
        m_health -= damage;
    }

    private void Start()
    {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
    }
    private void Update()
    {
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
        FollowPath();
        Death();//checks to see if enemy is dead 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_playerDetected == false)
        {
            PlayerDetection(collision.gameObject);
        }
       
    }
}
