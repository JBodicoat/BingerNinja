//Joao Beijinho

//Joao Beijinho 19/10/2020 - Created draft of the enter and exit states for the vents
//Joao Beijinho 20/10/2020 - Implemented vent walls and vent path. Moved trigger events to StealthObject
//Joao Beijinho 25/10/2020 - Moved Triggers back into this script 
//Joao Beijinho 09/11/2020 - Replaced tag with the tag in the Tags_JoaoBeijinho script
//Joao Beijinho 10/11/2020 - Replaced IgnoreCollision with IgnoreLayerCollision
//Joao Beijinho 11/10/2020 - Call ToggleVent() in VentEnter() and VentExit()
//Joao Beijinho 06/12/2020 - Player SpriteRenderer enable/disable on VentEnter()/VentExit()
//Joao Beijinho 11/12/2020 - Cleaned unused variables
//                           Put VentExit() on OnTriggerExit2D for use in VentPath instead of vent enter and exit
//                           Erased m_playerPos from VentEnter() and VentExit() to stop player from snapping to vent position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class makes the player enter and exit vents aswell as restrict the player movement inside the vent
/// </summary>
public class Vent_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    private GameObject m_player;
    
    private Collider2D[] m_ventWallsCollider;

    #region Enter and Exit triggers
    private void OnTriggerEnter2D(Collider2D collision)//Enter and Exit vent
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            if (!m_playerStealthScript.m_stealthed)//If player isn't stealthed he's not inside the vent
            {
                VentEnter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            if (m_playerStealthScript.m_stealthed)//If player isn't stealthed he's not inside the vent
            {
                VentExit();
            }
        }
    }
    #endregion
    
    /// <summary>
    /// Snap player to the position of the vent, disable collision with wall tiles
    /// </summary>
    private void VentEnter()
    {
        Hide();
        ToggleVent();
        
        m_player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        
        foreach (Collider2D ventWall in m_ventWallsCollider)
        {
            ventWall.enabled = true;
        }
        
        Physics2D.IgnoreLayerCollision(0, 10, true);
    }
    
    /// <summary>
    /// snap player to the position of the vent, enable collision with wall tiles
    /// </summary>
    private void VentExit()
    {
        Hide();
        ToggleVent();
        
        m_player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        
        foreach (Collider2D ventWall in m_ventWallsCollider)
        {
            ventWall.enabled = false;
        }
        
        Physics2D.IgnoreLayerCollision(0, 10, false);
    } 

    // Start is called before the first frame update
    void Start()
    {
        //Get the player GameObject for sprite changing
        m_player = GameObject.Find("Player");

        //Get the vent walls collider
        m_ventWallsCollider = gameObject.GetComponentsInChildren<BoxCollider2D>();

        //Turn VentPath GameObject OFF at the start, it will only be enabled after the player passes a trigger
        gameObject.SetActive(false);
    }
}