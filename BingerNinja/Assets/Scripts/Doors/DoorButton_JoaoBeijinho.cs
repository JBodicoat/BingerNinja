//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created a trigger for the buttons
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 05/11/2020 - Replaced collision.name to collision.tag on triggers
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

using System.Collections;
using System.Collections.Generic;
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

    public bool m_canPressButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))//Collision with Player
        {
            m_canPressButton = true;//Allow player to open the door
        }
        else if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_projectileTag))//Collision with Projectile
        {
            m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))//Collision with Player
        {
            m_canPressButton = false;//Don't allow player to open the door
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

            m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }
}