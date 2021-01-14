//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created this scripted, collision and object attachment/detachment to player
//Joao Beijinho 30/10/2020 - Created m_isClose bool so that the player can only grab when its colliding
//Joao Beijinho 02/10/2020 - Replaced m_isClose with m_isGrabbed, removed collider.trigger and put collider.enable
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Joao Beijinho 06/12/2020 - Changed m_collider type to Collider2D
//Joao Beijinho 14/12/2020 - Created two functions for the two statements in the Update()
//                           Reference PlayerHealthHunger Script
//                           In update, if HealthSlider(Using this since the Die() function is ) is bellow 1, UnGrab
//                           Unassign this GameObject Tag while getting the reference of all the other
//                           gameObject with the same tag, then reassign the tag back to this GameObject
//                           In the Grab() function, check if any other crate is grabbed, if it is then can't grab

using UnityEngine;

/// <summary>
/// This script makes it so that the player can grab and move an object, it stops moving after the player stops grabbing
/// </summary>
public class PushableObject_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;
    protected PlayerHealthHunger_MarioFernandes m_playerHealthScript;
     PlayerMovement_MarioFernandes a;
     EffectManager_MarioFernandes b;
     Transform c;
     Collider2D d;

     string e;
     GameObject[] f;
     PushableObject_JoaoBeijinho g;

    public bool m_canGrab = false;
    public bool m_isGrabbed = false;

     void Start()
    {
        c = GameObject.Find("Player").transform;
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
        m_playerHealthScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        d = GetComponent<Collider2D>();
        a = GameObject.Find("Player").GetComponent<PlayerMovement_MarioFernandes>();

        transform.gameObject.tag = "Untagged";//Remove tag from this GameObject
        e = Tags_JoaoBeijinho.m_crateTag;
        f = GameObject.FindGameObjectsWithTag(e);//Object with the crate tag
        gameObject.tag = e;//Reassign tag back to this GameObject
    }

     void OnCollisionEnter2D(Collision2D h)
    {
        if (h.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canGrab = true;
        }
    }

     void OnCollisionExit2D(Collision2D i)
    {
        if (i.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canGrab = false;
        }
    }

     void x(bool j, bool k)
    {
        m_canGrab = false;
        m_isGrabbed = true;
        transform.parent = c;
        Physics2D.IgnoreCollision(gameObject.transform.parent.GetComponent<Collider2D>(), d);
        a.m_baseSpeed = 1;
        a.m_speed = 1;

        foreach (GameObject crate in f)
        {
            g = crate.GetComponent<PushableObject_JoaoBeijinho>();
        
            if (g.m_isGrabbed)
            {
                y(true, false);
            }
        }
    }

     void y(bool l, bool m)
    {
        a.m_baseSpeed = 3;
        a.m_speed = 3;
        m_canGrab = l;
        m_isGrabbed = m;
        Physics2D.IgnoreCollision(gameObject.transform.parent.GetComponent<Collider2D>(), d, false);
        transform.parent = null;
    }

    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canGrab)//Press interact to grab object and move it freely
        {
            x(false, true);
        }
        else if (m_playerControllerScript.m_interact.triggered && m_isGrabbed)//Press interact to let go of object
        {
            y(true, false);
        }

        if (m_playerHealthScript.m_healthSlider.value <= 1)
        {
            y(false, false);
        }
    }
}