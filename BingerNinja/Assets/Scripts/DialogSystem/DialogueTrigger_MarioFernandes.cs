// Mário Fernandes
/// My class takes care of passing the dialog to the manager and activating it

// Mário 24/10/2020 - Creation of the class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_MarioFernandes : MonoBehaviour
{
	public Dialogue m_dialogue;

	///<summary>Call the dialog </summary>
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager_MarioFernandes>().StartDialogue(m_dialogue);
	}

	private void OnTriggerEnter2D(Collider2D other) {

		TriggerDialogue();
	}	
}


