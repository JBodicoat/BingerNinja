//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created this scripted, collision and object attachment/detachment to player
//Joao Beijinho 30/10/2020 - Created m_isClose bool so that the player can only grab when its colliding
//Joao Beijinho 02/10/2020 - Replaced m_isClose with m_isGrabbed, removed collider.trigger and put collider.enable
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Joao Beijinho 06/12/2020 - Changed m_collider type to Collider2D
//Joao Beijinho 14/12/2020 - Created two functions for the two statements in the Update()
//                           Reference PlayerHealthHunger Script
//                           In update, if HealthSlider(Using this since the Die() function is private) is bellow 1, UnGrab
//                           Unassign this GameObject Tag while getting the reference of all the other
//                           gameObject with the same tag, then reassign the tag back to this GameObject
//                           In the Grab() function, check if any other crate is grabbed, if it is then can't grab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes it so that the player can grab and move an object, it stops moving after the player stops grabbing
/// </summary>
public class PushableObject_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;
    protected PlayerHealthHunger_MarioFernandes m_playerHealthScript;
    private PlayerMovement_MarioFernandes m_playerSpeed;
    private EffectManager_MarioFernandes m_effectManager;
    private Transform m_playerTransform;
    private Collider2D m_collider;

    private string m_normalTag;
    private GameObject[] m_crates;
    private PushableObject_JoaoBeijinho m_pushableObjectScript;

    public bool m_canGrab = false;
    public bool m_isGrabbed = false;

    private void Start()
    {
        m_playerTransform = GameObject.Find("Player").transform;
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
        m_playerHealthScript = FindObjectOfType<PlayerHealthHunger_MarioFernandes>();
        m_collider = GetComponent<Collider2D>();
        m_playerSpeed = GameObject.Find("Player").GetComponent<PlayerMovement_MarioFernandes>();

        this.transform.gameObject.tag = "Untagged";//Remove tag from this GameObject
        m_normalTag = Tags_JoaoBeijinho.m_crateTag;
        m_crates = GameObject.FindGameObjectsWithTag(m_normalTag);//Object with the crate tag
        this.gameObject.tag = m_normalTag;//Reassign tag back to this GameObject
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canGrab = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canGrab = false;
        }
    }

    private void Grab(bool canGrab, bool isGrabbed)
    {
        m_canGrab = false;
        m_isGrabbed = true;
        transform.parent = m_playerTransform;
        Physics2D.IgnoreCollision(gameObject.transform.parent.GetComponent<Collider2D>(), m_collider);
        m_playerSpeed.m_baseSpeed = 1;
        m_playerSpeed.m_speed = 1;

        foreach (GameObject crate in m_crates)
        {
            m_pushableObjectScript = crate.GetComponent<PushableObject_JoaoBeijinho>();
        
            if (m_pushableObjectScript.m_isGrabbed == true)
            {
                UnGrab(true, false);
            }
        }
    }

    private void UnGrab(bool canGrab, bool isGrabbed)
    {
        m_playerSpeed.m_baseSpeed = 3;
        m_playerSpeed.m_speed = 3;
        m_canGrab = canGrab;
        m_isGrabbed = isGrabbed;
        Physics2D.IgnoreCollision(gameObject.transform.parent.GetComponent<Collider2D>(), m_collider, false);
        transform.parent = null;
    }

    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canGrab == true)//Press interact to grab object and move it freely
        {
            Grab(false, true);
        }
        else if (m_playerControllerScript.m_interact.triggered && m_isGrabbed == true)//Press interact to let go of object
        {
            UnGrab(true, false);
        }

        if (m_playerHealthScript.m_healthSlider.value <= 1)
        {
            UnGrab(false, false);
        }
    }
}