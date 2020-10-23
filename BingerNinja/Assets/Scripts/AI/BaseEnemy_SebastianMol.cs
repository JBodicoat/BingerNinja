using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//sebastian mol
//base class for enemies to inherit from with some functionality.
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform rayCastStart; //start position of the ray cast
    public PolygonCollider2D detectionCollider; // the collder cone used for player detection
    public GameObject DropItem; // itme that i sdropped when enemie dies
    public float Health; ///enemy health with getter and setter
    public Transform playerTransform; //used to get playe position can be null if undedteceted
    public bool PlayerDetected = false;
    public enum state { WONDER, CHASE, ATTACK, RETREAT};
    public state currentState = state.WONDER;

    internal void PlayerDetection(GameObject collision) //detect player in vision cone the establishes line of sight
    {
        if(collision.CompareTag("Player"))
        {
            detectionCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(rayCastStart.position, collision.transform.position);
            Debug.DrawLine(rayCastStart.position, collision.transform.position, Color.red);

            if(hit.collider.gameObject.name == "Player")
            {
                Debug.Log("hit player");
                PlayerDetected = true;
                playerTransform = hit.transform;
                Debug.Log("state.attck");
                currentState = state.ATTACK;
            }
            else
            {
                detectionCollider.enabled = true;
                Debug.Log("hit " + hit.collider.gameObject.name);
            }
        }
    } 
    internal void Death() //actions that happen befor enemy death
    {
        if(Health <= 0)
        {
            if (DropItem != null)
            {
                Instantiate(DropItem, transform.position, Quaternion.identity);
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
        if (PlayerDetected == true)
        {
            //currentState = state.CHASE;
            //detectionCollider.enabled = false;
        }
    }

    abstract internal void EnemyBehaviour();

    private void Update()
    {
        EnemyBehaviour(); // behaviour of the enemy what stste it is in and what it dose
        Death();//checks to see if enemy is dead 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerDetected == false)
        {
            PlayerDetection(collision.gameObject);
        }
       
    }
}
