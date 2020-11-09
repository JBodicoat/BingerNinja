//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created the script, triggers to check if player can interact, and calling interaction in update
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class acts as a button which calls interactions from the object that it is referencing
/// </summary>
public class ControlPanel_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;
    public ControlPanelActivateObject_JoaoBeijinho m_activateObjectScript;

    private string m_playerTag = "Player";

    public bool m_canPressButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canPressButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag))
        {
            m_canPressButton = false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_playerControllerScript = FindObjectOfType<PlayerController_JamieG>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton == true)//Player interaction with button
        {
            m_activateObjectScript.ActivateObject();
        }
    }
}
