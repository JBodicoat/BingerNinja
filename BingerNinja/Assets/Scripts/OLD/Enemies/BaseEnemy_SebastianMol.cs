using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseEnemy_SebastianMol : MonoBehaviour
{
    public Transform RayCastStart;
    public PolygonCollider2D DetectionCollider;
    bool PlayerDetection(GameObject collision)
    {
        if(collision.CompareTag("Player"))
        {
            DetectionCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(RayCastStart.position, collision.transform.position);
            Debug.DrawLine(RayCastStart.position, collision.transform.position, Color.red);

            if(hit.collider.gameObject.name == "Player")
            {
                DetectionCollider.enabled = true;
                return true;  
            }
            else
            {
                DetectionCollider.enabled = true;
                return false;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Debug.Log( PlayerDetection(collision.gameObject));
    }


}
