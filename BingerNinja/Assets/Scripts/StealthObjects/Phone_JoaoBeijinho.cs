//Joao Beijinho

//Joao Beijinho 18/10/2020 - Created draft for Phone interaction, making the player stealth and unable to move for a few seconds
//Joao Beijinho 19/10/2020 - Updated Movement restriction using PlayerController, and now the player can move after a specific set of time
//Joao Beijinho 26/10/2020 - Removed reference to PlayerController script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class handles interaction with phone, makes player stealth for a while but unable to move
/// </summary>
public class Phone_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;
    protected ClueSystem_JoaoBeijinho m_clueSystemScript;
    protected DialogueManager_MarioFernandes m_dialogueManagerScript;

    public bool m_onPhone = false;

    private Dialogue m_dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_onPhone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_onPhone = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
        m_clueSystemScript = gameObject.GetComponent<ClueSystem_JoaoBeijinho>();
        m_dialogueManagerScript = FindObjectOfType<DialogueManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_onPhone == true)//Player interaction with button
        {

        }
    }
}
