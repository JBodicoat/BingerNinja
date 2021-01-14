//Joao Beijinho

//Joao Beijinho 23/10/2020 - Created class to work with crouching
//Joao Beijinho 26/10/2020 - Removed Crouching. Created trigger that toggles stealth/unstealth in the player and disable/enable movement
//Joao Beijinho 27/10/2020 - Changed order of if statemants in update. Created m_isCrouching so that the player can't hide while crouched and n_changeLayer
//Elliott Desouza 07/11/2020 - 
// Jack 07/11/2020 - Set canHide & isHiding to 
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
     SpriteRenderer a;
     SpriteRenderer b;
     string c;

     GameObject e;
     ParticleSystem d;
     bool f = false;
     bool g = false;
    // bool m_isCrouching;

     string h = "Player";

    /// <summary>
    /// Enable player ability to hide
    /// </summary>
     void OnTriggerEnter2D(Collider2D i)
    {
        if (i.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            f = true;
        }
    }

    /// <summary>
    /// Disable player ability to hide
    /// </summary>
     void OnTriggerExit2D(Collider2D j)
    {
        if (j.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            f = false;
        }
    }

    void Start()
    {
        e = GameObject.Find("Player");
        b = e.GetComponent<SpriteRenderer>();
        c = b.sortingLayerName;
        a = GetComponent<SpriteRenderer>();
        d = GetComponentInChildren<ParticleSystem>();
    }

    /// <summary>
    /// Check if player can hide a chooses to do so, also checks if player is already hiding and chooses to get out of hiding
    /// </summary>
    void Update()
    {
      //  m_isCrouching = m_playerStealthScript.m_crouched;

        if (A.m_interact.triggered && g)
        {
            f = true;
            g = false;
            b.sortingOrder = 10;
            b.sortingLayerName = c;
            W();
            A.m_movement.Enable();
            A.m_crouch.Enable();
            gameObject.layer = 2; 
        }
        else if (A.m_interact.triggered && f&& !B.B)
        {
            // hide behind this object
            f = false;
            g = true;
            b.sortingOrder = 8;
            b.sortingLayerName = a.sortingLayerName;
            e.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, e.transform.position.z);
            W();
            A.m_movement.Disable();
            A.m_crouch.Disable();
            d.Play();
            gameObject.layer = 0; 
        }
    }
}