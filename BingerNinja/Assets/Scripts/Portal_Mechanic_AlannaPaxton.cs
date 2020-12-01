using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// alanna 09/11/20, needs to be added onto player object, then create portals and tag them appropriately. Also add animation if wanted.
//alanna 19/11/20 this code just needs to be added onto player. when making portals in hierarchy pair the portals up so they have 1 parent and one child, then this code should work for all of them.

public class Portal_Mechanic_AlannaPaxton : MonoBehaviour
{
    bool canTeleport = false;
    protected int trigger = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            if (canTeleport)
            {
                // Portal();
                if (collision.transform.childCount > 0)
                {
                    GameObject ChildGameObject1 = collision.transform.GetChild(0).gameObject;
                    Vector2 child_pos = ChildGameObject1.transform.position;
                    this.transform.position = child_pos;
                    canTeleport = false;
                } else if (collision.transform.parent != null)
                {
                    Vector2 parent_pos = collision.transform.parent.position;
                    this.transform.position = parent_pos;
                    canTeleport = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Portal")
        {
            if (trigger++ >= 2)
            {
                Debug.Log(trigger);
                canTeleport = true;
                trigger = 0;
            }
        }
        
    }


}
