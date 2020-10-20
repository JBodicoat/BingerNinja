//sebastian mol
//base class for enemies to inherit from with some functionality.

// Jack 20/10 - Added check for if player is stealthed.
//              Added tempAIScript which is just for demonstrating the prototype and can be removed afterwards
//              Reformated code & changed cone detection & LOS. Execute function now called when enemy has LOS

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform RayCastStart; //start position of the ray cast
    public PolygonCollider2D DetectionCollider; // the collder cone used for player detection
    public GameObject DropItem; // itme that i sdropped when enemie dies
    public float Health; //enemy health with getter and setter


    private PlayerStealth_JoaoBeijinho playerStealthScript;
    public EnemyAi tempAIScript;
    public Transform playerTransform;
    private bool playerInCone = false;

	private void Start()
	{
        playerStealthScript = FindObjectOfType<PlayerStealth_JoaoBeijinho>();
	}

	private void Update()
	{
        print("Update");
		if(playerInCone && !playerStealthScript.IsStealthed())
        {
            print("testing los");
            if(HasLineOfSight())
            {
                print("player seen");
                tempAIScript.Execute();
			}
		}
	}

	public abstract void MovementBehaviour(); //abstract function to have a range of different behavours while moving

    private bool HasLineOfSight() //detect player in vision cone the establishes line of sight
    {
        RaycastHit2D hit = Physics2D.Linecast(RayCastStart.position, playerTransform.position);
        Debug.DrawLine(RayCastStart.position, playerTransform.position, Color.red);

        if (hit)
        {
            return hit.collider.gameObject.name == "Player";
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
        if (collision.tag == "Player")
        {
            print("Player entered cone");
            playerInCone = true;
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            print("Player exited cone");
            playerInCone = false;
        }
	}
}
