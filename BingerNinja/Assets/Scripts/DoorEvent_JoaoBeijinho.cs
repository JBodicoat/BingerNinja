//Joao Beijinho

//Joao Beijinho 26/10/2020 - Created triggers for pressure pads, buttons and keys. They filter collisions by names in an array

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script has all states for pressure pads and buttons to open and close doors
/// </summary>
public class DoorEvent_JoaoBeijinho : MonoBehaviour
{
    PlayerController_JamieG m_playerControllerScript;

    public GameObject m_door;

    public string[] m_triggerDoor = {"MeleeWeapon", "Crate", "Player", "Projectile" };

    private bool m_canPressButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Trigger PressurePad
        if (gameObject.name.Contains("PressurePad"))//If this script is in a PressurePad
        {
            for (int i = 0; i < m_triggerDoor.Length-1; i++)//read array up until Player
            {
                if (collision.name == m_triggerDoor[i])//collision with every array object except Projectile
                {
                    m_door.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }
        #endregion
        #region Trigger Button
        else if (gameObject.name.Contains("Button"))//If this script is in a Button
        {
            if (collision.name == m_triggerDoor[2])//Collision with Player
            {
                m_canPressButton = true;
            }
            else if (collision.name == m_triggerDoor[3])//Collision with Projectile
            {
                m_door.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        #endregion
        #region Trigger Key
        else//If this script is in a Key
        {
            if (collision == m_door.GetComponent<BoxCollider2D>())
            {
                m_door.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        #endregion
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name.Contains("PressurePad"))//if this scirpt is in a Pressure Pad
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_playerControllerScript = player.GetComponent<PlayerController_JamieG>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton)//Interact with button
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}