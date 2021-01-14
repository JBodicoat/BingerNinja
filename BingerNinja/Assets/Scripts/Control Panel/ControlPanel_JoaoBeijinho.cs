//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created the script, triggers to check if player can interact, and calling interaction in update
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Joao Beijinho 27/11/2020 - Made an array for ControlPanelActivateObject to allow the control panel to activate multiple objects
//Joao Beijinho 12/01/2021 - Moved LevelScripting bits to the ControlPanelActivateObject Script

using UnityEngine;

/// <summary>
/// This class acts as a button which calls interactions from the object that it is referencing
/// </summary>
public class ControlPanel_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG m_playerControllerScript;
    public ControlPanelActivateObject_JoaoBeijinho[] m_activateObjectScript;

    public bool m_canPressButton = false;

     void OnTriggerEnter2D(Collider2D a)
    {
        if (a.gameObject.CompareTag(Tags_JoaoBeijinho.QK))
        {
           
            m_canPressButton = true;
        }
    }

     void OnTriggerExit2D(Collider2D b)
    {
        if (b.gameObject.CompareTag(Tags_JoaoBeijinho.QK))
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
        if (m_playerControllerScript.m_interact.triggered && m_canPressButton)//Player interaction with button
        {
            for (int i = 0; i < m_activateObjectScript.Length; i++)
            {
                m_activateObjectScript[i].ActivateObject();
            }
        }
    }
}
