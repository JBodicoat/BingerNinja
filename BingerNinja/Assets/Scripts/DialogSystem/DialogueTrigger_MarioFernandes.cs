// Mário Fernandes
/// My class takes care of passing the dialog to the manager and activating it

// Mário 24/10/2020 - Creation of the class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_MarioFernandes : MonoBehaviour
{
	public Dialogue dialogue;

	bool a = true;

	private GameObject Player;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager_MarioFernandes>().StartDialogue(dialogue);
	}

	private void OnTriggerEnter2D(Collider2D other) {

		TriggerDialogue();
	}

	private void Start() {
		Player = GameObject.FindWithTag("Player");
	}
	void Update()
	{

	}

	
}


