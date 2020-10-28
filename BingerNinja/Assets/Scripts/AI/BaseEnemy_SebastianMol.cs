using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

//sebastian mol
//base class for enemies to inherit from with some functionality.
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform m_rayCastStart; //start position of the ray cast
    public PolygonCollider2D m_detectionCollider; // the collder cone used for player detection
    internal Transform m_playerTransform; //used to get playe position can be null if undedteceted
    public bool m_playerDetected = false;
    public enum state { WONDER, CHASE, ATTACK, RETREAT};
    public state m_currentState = state.WONDER;
    [Header("designers Section")]
    [Tooltip("the item the enemy drops on death")]
    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    [Tooltip("health of the enemy")]
    public float m_health; //enemy health with getter and setter
    public float m_speed; //movment speed

    private Pathfinder_SebastianMol m_pathfinder;
    private List<Vector2Int> m_currentPath;

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
        Debug.Log(m_currentPath[1]);
    }

    internal void FollowPath()
    {
        float movmentLeftOver = m_speed * Time.deltaTime; //movment allowed this frame

        while (movmentLeftOver >= 0)
        {
            //gets the center of the target tile
           Vector2 targetTileCenter = (Vector2)(Vector3)m_pathfinder.m_tileMap.WorldToCell(transform.position) + ((Vector2)m_pathfinder.m_tileMap.cellSize / 2);
           float distance = Vector2.Distance(m_currentPath[0],  targetTileCenter); //distance to the target tile center
           if ( distance > movmentLeftOver)//if all movment is used up in one move
           {
                movmentLeftOver = 0;
                m_currentPath.RemoveAt(0);
                break;
           }
           else //if theres movment left over after the next tile
           {
                movmentLeftOver -= distance;
           }
           //move
            Vector2.MoveTowards(gameObject.transform.position, targetTileCenter, distance);
        }
    }

    internal void ClearPath()
    {
        m_currentPath.Clear();
    }

    private void Start()
    {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder_SebastianMol>();
    }

    private void Update()
    {
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
        if (m_currentPath != null) FollowPath();
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
