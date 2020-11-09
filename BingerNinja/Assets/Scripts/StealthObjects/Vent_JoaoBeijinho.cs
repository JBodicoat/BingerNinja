//Joao Beijinho

//Joao Beijinho 19/10/2020 - Created draft of the enter and exit states for the vents
//Joao Beijinho 20/10/2020 - Implemented vent walls and vent path. Moved trigger events to StealthObject
//Joao Beijinho 25/10/2020 - Moved Triggers back into this script 

//!!!CHECK IF CAN REMOVE M_ENABLEVENTWALLCOLLISION BOOL!!!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class makes the player enter and exit vents aswell as restrict the player movement inside the vent
/// </summary>
public class Vent_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    private GameObject m_player;
    private GameObject m_ventPath;
    private GameObject m_walls;

    //Player and wall tiles collider
    private BoxCollider2D m_playerCollider;
    private BoxCollider2D[] m_ventWallsCollider;

    private Collider2D m_wallsCollider;

    //Player and vents position to snap player when entering or exiting vent
    private Transform m_playerPos;
    private Transform m_ventPos;

    #region Enter and Exit triggers
    private void OnTriggerEnter2D(Collider2D collision)//Enter and Exit vent
    {
        if (collision.tag == "Player")
        {
            if (m_playerStealthScript.m_stealthed == false)//If player isn't stealthed he's not inside the vent
            {
                VentEnter();
            }
            else
            {
                VentExit();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    #endregion
    
    /// <summary>
    /// Snap player to the position of the vent, disable collision with wall tiles
    /// </summary>
    private void VentEnter()
    {
        Hide();

        m_playerPos.position = new Vector3(m_ventPos.position.x, m_ventPos.position.y, m_ventPos.position.z);

        foreach (BoxCollider2D ventWall in m_ventWallsCollider)
        {
            ventWall.enabled = true;
        }

        Physics2D.IgnoreCollision(m_playerCollider, m_wallsCollider);
    }
    
    /// <summary>
    /// snap player to the position of the vent, enable collision with wall tiles
    /// </summary>
    private void VentExit()
    {
        Hide();

        m_playerPos.position = new Vector2(m_ventPos.position.x, m_ventPos.position.y);

        foreach (BoxCollider2D ventWall in m_ventWallsCollider)
        {
            ventWall.enabled = false;
        }

        Physics2D.IgnoreCollision(m_playerCollider, m_wallsCollider, false);
    } 

    // Start is called before the first frame update
    void Start()
    {
        //Get the player, its position and its collider
        m_player = GameObject.Find("Player");
        m_playerPos = m_player.GetComponent<Transform>();
        m_playerCollider = m_player.GetComponent<BoxCollider2D>();

        //Get the wall tiles and their collider
        m_walls = GameObject.Find("Walls1_map");
        m_wallsCollider = m_walls.GetComponent<Collider2D>();

        //Get the vent, vent path and the vent walls collider
        m_ventPos = gameObject.GetComponent<Transform>();
        m_ventPath = GameObject.Find("VentPath");
        m_ventWallsCollider = m_ventPath.GetComponentsInChildren<BoxCollider2D>();
    }
}