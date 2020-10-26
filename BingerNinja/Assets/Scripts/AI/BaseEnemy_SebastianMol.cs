using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//sebastian mol
//base class for enemies to inherit from with some functionality.
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform m_rayCastStart; //start position of the ray cast
    public PolygonCollider2D m_detectionCollider; // the collder cone used for player detection
    internal Transform m_playerTransform; //used to get playe position can be null if undedteceted
    public bool m_playerDetected = false;
    public enum state { WONDER, CHASE, ATTACK, RETREAT};
    internal state m_currentState = state.WONDER;
    [Header("designers Section")]
    [Tooltip("the item the enemy drops on death")]
    public GameObject m_dropItem; // itme that i sdropped when enemie dies
    [Tooltip("health of the enemy")]
    public float m_health; ///enemy health with getter and setter

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


    private void Update()
    {
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
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
