//Joao Beijinho

//Joao Beijinho 18/10/2020 - Created draft for Phone interaction, making the player stealth and unable to move for a few seconds
//Joao Beijinho 19/10/2020 - Updated Movement restriction using PlayerController, and now the player can move after a specific set of time
//Joao Beijinho 26/10/2020 - Removed reference to PlayerController script
//Joao Beijinho 06/11/2020 - Added Dialogue and Clues to the Update()

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class handles interaction with phone, makes player stealth for a while but unable to move
/// </summary>
public class Phone_JoaoBeijinho : StealthObject_JoaoBeijinho
{
    protected ClueSystem_JoaoBeijinho m_clueSystemScript;
    protected DialogueManager_MarioFernandes m_dialogueManagerScript;

    private bool m_onPhone = false;

    private Dialogue m_dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_onPhone = true;

            StartCoroutine(PhoneDuration());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_onPhone = false;
        }
    }

    //Go into stealth and stop movement while on phone, unstealth and resume movement after a set time
    IEnumerator PhoneDuration()
    {
        Hide();
        m_playerControllerScript.m_movement.Disable();

        yield return new WaitForSeconds(5);

        Hide();
        m_playerControllerScript.m_movement.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_clueSystemScript = gameObject.GetComponent<ClueSystem_JoaoBeijinho>();
        m_dialogueManagerScript = FindObjectOfType<DialogueManager_MarioFernandes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_onPhone == true)//Player interaction with button
        {
            m_clueSystemScript.GetCurrentClue();//Refresh current clue

            m_dialogue.m_name = "Phone";//Set phone as dialogue name
            m_dialogue.m_sentences = m_clueSystemScript.m_currentClue;//Set current clue as dialogue sentence

            m_dialogueManagerScript.StartDialogue(m_dialogue);//Show dialogue in-game
        }
    }
}
