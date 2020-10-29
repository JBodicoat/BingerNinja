//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created a trigger for the buttons

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for Buttons that open doors. Either if activated by the player or hit by a projectile
/// </summary>
public class DoorButton_JoaoBeijinho : MonoBehaviour
{
    PlayerController_JamieG m_playerControllerScript;

    public GameObject m_door;

    private string m_playerTag = "Player";
    private string m_projectileTag = "Projectile";

    public bool m_canPressButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == m_playerTag)//Collision with Player
        {
            m_canPressButton = true;
        }
        else if (collision.name == m_projectileTag)//Collision with Projectile
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == m_playerTag)//Collision with Player
        {
            m_canPressButton = false;
        }
    }

    void Awake()
    {
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton)//Player interaction with button
        {
            m_door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}