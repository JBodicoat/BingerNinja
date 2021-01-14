﻿//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created a trigger for the buttons
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 05/11/2020 - Replaced collision.name to collision.tag on triggers
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Chase Wilding 16/11/2020 - Edited to add tilemap removal/drawing for doors, added close functionality

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This script is for Buttons that open doors. Either if activated by the player or hit by a projectile
/// </summary>
public class DoorButton_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;

    //Reference the door collider to turn if on/off(closed/open)
    public Collider2D m_doorCollider;

    public Tile bottomDoor, topDoor;
     Tilemap a, b;

    public bool m_canPressButton = false;
    public bool m_doorIsOpen = false;

    public string buttonName = "";

     void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))//Collision with Player
        {
            m_canPressButton = true;//Allow player to open the door
        }
        else if (c.gameObject.CompareTag(Tags_JoaoBeijinho.m_projectileTag))//Collision with Projectile
        {
            m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }

     void OnTriggerExit2D(Collider2D d)
    {
        if (d.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))//Collision with Player
        {
            m_canPressButton = false;//Don't allow player to open the door
        }
    }

    void Awake()
    {
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
        a = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        b = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton && buttonName == "")//Player interaction with button
        {
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_PlayerThrow);
            if (!m_doorIsOpen)
            {
                m_doorIsOpen = true;
                b.SetTile(new Vector3Int(24, 9, 0), null);
                b.SetTile(new Vector3Int(24, 10, 0), null);
                b.SetTile(new Vector3Int(24, 11, 0), null);

                a.SetTile(new Vector3Int(23, 8, 0), null);
                a.SetTile(new Vector3Int(23, 9, 0), null);
                a.SetTile(new Vector3Int(23, 10, 0), null);
              //  m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
            }
            else
            {
                b.SetTile(new Vector3Int(24, 9, 0), topDoor);
                b.SetTile(new Vector3Int(24, 10, 0), topDoor);
                b.SetTile(new Vector3Int(24, 11, 0), topDoor);

                a.SetTile(new Vector3Int(23, 8, 0), bottomDoor);
                a.SetTile(new Vector3Int(23, 9, 0), bottomDoor);
                a.SetTile(new Vector3Int(23, 10, 0), bottomDoor);
                m_doorIsOpen = false;
            }
        }

        if (m_playerControllerScript.m_interact.triggered && m_canPressButton && buttonName == "S11.B1")//Player interaction with button
        {
            if (!m_doorIsOpen)
            {
                m_doorIsOpen = true;
                b.SetTile(new Vector3Int(12, 20, 0), null);
                b.SetTile(new Vector3Int(13, 20, 0), null);
                b.SetTile(new Vector3Int(14, 20, 0), null);

                a.SetTile(new Vector3Int(13, 19, 0), null);
                a.SetTile(new Vector3Int(12, 19, 0), null);
                a.SetTile(new Vector3Int(11, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(12, 20, 0), topDoor);
                b.SetTile(new Vector3Int(13, 20, 0), topDoor);
                b.SetTile(new Vector3Int(14, 20, 0), topDoor);

                a.SetTile(new Vector3Int(13, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(12, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(11, 19, 0), bottomDoor);
                m_doorIsOpen = false;
            }

        }
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton && buttonName == "S11.B2")//Player interaction with button
        {
            if (!m_doorIsOpen)
            {

                m_doorIsOpen = true;
                b.SetTile(new Vector3Int(20, 20, 0), null);
                b.SetTile(new Vector3Int(19, 20, 0), null);
                b.SetTile(new Vector3Int(18, 20, 0), null);

                a.SetTile(new Vector3Int(19, 19, 0), null);
                a.SetTile(new Vector3Int(18, 19, 0), null);
                a.SetTile(new Vector3Int(17, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(20, 20, 0), topDoor);
                b.SetTile(new Vector3Int(19, 20, 0), topDoor);
                b.SetTile(new Vector3Int(18, 20, 0), topDoor);

                a.SetTile(new Vector3Int(19, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(18, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(17, 19, 0), bottomDoor);
                m_doorIsOpen = false;
            }

        }
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton && buttonName == "S11.B3")//Player interaction with button
        {
            if (!m_doorIsOpen)
            {
                m_doorIsOpen = true;
                b.SetTile(new Vector3Int(26, 20, 0), null);
                b.SetTile(new Vector3Int(25, 20, 0), null);
                b.SetTile(new Vector3Int(24, 20, 0), null);

                a.SetTile(new Vector3Int(25, 19, 0), null);
                a.SetTile(new Vector3Int(24, 19, 0), null);
                a.SetTile(new Vector3Int(23, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(26, 20, 0), topDoor);
                b.SetTile(new Vector3Int(25, 20, 0), topDoor);
                b.SetTile(new Vector3Int(24, 20, 0), topDoor);

                a.SetTile(new Vector3Int(25, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(24, 19, 0), bottomDoor);
                a.SetTile(new Vector3Int(23, 19, 0), bottomDoor);
                m_doorIsOpen = false;
            }

        }
    }
}