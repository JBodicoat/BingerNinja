using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//sebastian mol
//base class for enemies to inherit from with some functionality.
abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform RayCastStart; //start position of the ray cast
    public PolygonCollider2D DetectionCollider; // the collder cone used for player detection
    public GameObject DropItem; // itme that i sdropped when enemie dies
    public float Health; //enemy health with getter and setter

    public abstract void MovementBehaviour(); //abstract function to have a range of different behavours while moving
    private bool PlayerDetection(GameObject collision) //detect player in vision cone the establishes line of sight
    {
        if(collision.CompareTag("Player"))
        {
            //if stealth is not active

            DetectionCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(RayCastStart.position, collision.transform.position);
            Debug.DrawLine(RayCastStart.position, collision.transform.position, Color.red);

            if(hit.collider.gameObject.name == "Player")
            {
                DetectionCollider.enabled = true;
                Debug.Log("hit player");
                return true;  
            }
            else
            {
                DetectionCollider.enabled = true;
                Debug.Log("hit " + hit.collider.gameObject.name);
                return false;
            }
        }

        return false;
    } 
    private void OnDeath() //actions that happen befor enemy death
    {
        if(DropItem != null)
        {
            Instantiate(DropItem, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDetection(collision.gameObject);
    }
}
