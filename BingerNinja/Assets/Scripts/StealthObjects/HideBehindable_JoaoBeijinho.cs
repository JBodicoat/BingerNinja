//Joao Beijinho

//Joao Beijinho 23/10/2020 - Created class to work with crouching
//Joao Beijinho 26/10/2020 - Removed Crouching. Created trigger that toggles stealth/unstealth in the player and disable/enable movement
//Joao Beijinho 27/10/2020 - Changed order of if statemants in update. Created m_isCrouching so that the player can't hide while crouched and n_changeLayer
//Elliott Desouza 07/11/2020 - 
// Jack 07/11/2020 - Set canHide & isHiding to private
//                   Removed isCrouching
//                   Added comments within Update
//                   Moved particle system onto the prefab rather than on the player
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//sebastian mol 11/12/2020 - chnaged the layer of plants based on stealth
//Joao Beijinho 14/12/2020 - Change player sprite order instead of the plant sprite order
//                           Change player position to plant position when hiding

using UnityEngine;

/// <summary>
///This class allows player to interact with objects that can be used to crouch behind.
/// </summary>
public class HideBehindable_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    //Required to change sprite orders
    private SpriteRenderer m_changeLayer;
    private SpriteRenderer m_playerSprite;
    private string m_playerSpriteLayer;

    private GameObject m_player;
    private ParticleSystem m_smokeParticleSystem;
    private bool m_canHide = false;
    private bool m_isHiding = false;
    //private bool m_isCrouching;

    private string m_playerTag = "Player";

    /// <summary>
    /// Enable player ability to hide
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canHide = true;
        }
    }

    /// <summary>
    /// Disable player ability to hide
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canHide = false;
        }
    }

    void Start()
    {
        m_player = GameObject.Find("Player");
        m_playerSprite = m_player.GetComponent<SpriteRenderer>();
        m_playerSpriteLayer = m_playerSprite.sortingLayerName;
        m_changeLayer = GetComponent<SpriteRenderer>();
        m_smokeParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    /// <summary>
    /// Check if player can hide a chooses to do so, also checks if player is already hiding and chooses to get out of hiding
    /// </summary>
    void Update()
    {
      //  m_isCrouching = m_playerStealthScript.m_crouched;

        if (m_playerControllerScript.m_interact.triggered && m_isHiding == true)
        {
            m_canHide = true;
            m_isHiding = false;
            m_playerSprite.sortingOrder = 10;
            m_playerSprite.sortingLayerName = m_playerSpriteLayer;
            Hide();
            m_playerControllerScript.m_movement.Enable();
            m_playerControllerScript.m_crouch.Enable();
            gameObject.layer = 2; 
        }
        else if (m_playerControllerScript.m_interact.triggered && m_canHide == true && !m_playerStealthScript.m_crouched)
        {
            // hide behind this object
            m_canHide = false;
            m_isHiding = true;
            m_playerSprite.sortingOrder = 8;
            m_playerSprite.sortingLayerName = m_changeLayer.sortingLayerName;
            m_player.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, m_player.transform.position.z);
            Hide();
            m_playerControllerScript.m_movement.Disable();
            m_playerControllerScript.m_crouch.Disable();
            m_smokeParticleSystem.Play();
            gameObject.layer = 0; 
        }
    }
}