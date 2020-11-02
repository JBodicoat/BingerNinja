//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created this scripted, collision and object attachment/detachment to player
//Joao Beijinho 30/10/2020 - Created m_isClose bool so that the player can only grab when its colliding
//Joao Beijinho 02/10/2020 - Replaced m_isClose with m_isGrabbed, removed collider.trigger and put collider.enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes it so that the player can grab and move an object, it stops moving after the player stops grabbing
/// </summary>
public class PushableObject_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;

    private GameObject m_player;
    private BoxCollider2D m_collider; 

    private string m_playerTag = "Player";
    public bool m_canGrab = false;
    public bool m_isGrabbed = false;

    private void Start()
    {
        m_player = GameObject.Find("Player");
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
        m_collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == m_playerTag)
        {
            m_canGrab = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == m_playerTag)
        {
            m_canGrab = false;
        }
    }

    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canGrab == true)//Press interact to grab object and move it freely
        {
            m_canGrab = false;
            m_isGrabbed = true;
            gameObject.transform.parent = m_player.transform;
            Physics2D.IgnoreCollision(m_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (m_playerControllerScript.m_interact.triggered && m_isGrabbed == true)//Press interact to let go of object
        {
            m_canGrab = true;
            m_isGrabbed = false;
            gameObject.transform.parent = null;
            Physics2D.IgnoreCollision(m_player.GetComponent<Collider2D>(), m_collider, false);
        }
    }
}