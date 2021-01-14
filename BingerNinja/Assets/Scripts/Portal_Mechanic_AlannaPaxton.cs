using System.Collections;
using UnityEngine;

// alanna 09/11/20, needs to be added onto player object, then create portals and tag them appropriately. Also add animation if wanted.
//alanna 19/11/20 this code just needs to be added onto player. when making portals in hierarchy pair the portals up so they have 1 parent and one child, then this code should work for all of them.
//Jon did a thing

public class Portal_Mechanic_AlannaPaxton : MonoBehaviour
{
    bool canTeleport = true;
    public float portalDelay = 0.8f;

    void OnTriggerEnter2D(Collider2D a)
    {
        if (a.gameObject.tag == "Portal")
        {
            if (canTeleport)
            {
                // Portal();
                if (a.transform.childCount > 0)
                {
                    GameObject b = a.transform.GetChild(0).gameObject;
                    Vector2 c = b.transform.position;
                    transform.position = c;
                    StartCoroutine(PortalExitDelay());
                } else if (a.transform.parent != null)
                {
                    Vector2 d = a.transform.parent.position;
                    transform.position = d;
                    StartCoroutine(PortalExitDelay());
                }
            }
        }
    }

    IEnumerator PortalExitDelay()
    {
        canTeleport = false;
        SceneManager_JamieG.Instance.FadeBoth();
        yield return new WaitForSeconds(portalDelay);
        canTeleport = true;
    }

}
