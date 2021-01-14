// Mário Fernandes

// Mário 24/10/2020 - Creation of the class
// Mário 28/10/2020 - Check if triger is the player and one time only dialog
//sebastian mol 10/12/2020 - made trigger dissapear after activated

using UnityEngine;

/// <summary>
/// My class takes care of passing the dialog to the manager and activating it
/// </summary>
public class DialogueTrigger_MarioFernandes : MonoBehaviour
{
	public Dialogue m_dialogue;

	bool q = true;

	///<summary>Call the dialog </summary>
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager_MarioFernandes>().StartDialogue(m_dialogue);
	}

	 void OnTriggerEnter2D(Collider2D w) {
		if(w.tag == "Player" && q)
		{
			TriggerDialogue();
			q = false;
			gameObject.SetActive(false);
		}
	}	
}


