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
    protected PlayerController_JamieG q;
    public ControlPanelActivateObject_JoaoBeijinho[] w;

    public bool e = false;

    private void OnTriggerEnter2D(Collider2D r)
    {
        if (r.gameObject.CompareTag(Tags_JoaoBeijinho.QC))
        {
           
            e = true;
        }
    }

    private void OnTriggerExit2D(Collider2D t)
    {
        if (t.gameObject.CompareTag(Tags_JoaoBeijinho.QC))
        {
         
            e = false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        q = FindObjectOfType<PlayerController_JamieG>();
    }

    // Update is called once per frame
    void Update()
    {
        if (q.m_interact.triggered && e == true)//Player interaction with button
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i].x();
            }
        }
    }
}
