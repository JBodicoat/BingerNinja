﻿// Mário Fernandes

// Mário 24/10/2020 - Creation of the class
// Mário 28/10/2020 - Check if triger is the player and one time only dialog
//sebastian mol 10/12/2020 - made trigger dissapear after activated

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My class takes care of passing the dialog to the manager and activating it
/// </summary>
public class DialogueTrigger_MarioFernandes : MonoBehaviour
{
	public Dialogue m_dialogue;

	bool m_state = true;

	///<summary>Call the dialog </summary>
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager_MarioFernandes>().StartDialogue(m_dialogue);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player" && m_state)
		{
			TriggerDialogue();
			m_state = false;
			gameObject.SetActive(false);
		}
	}	
}


