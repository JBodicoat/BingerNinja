// Mário Fernandes
/// My class takes care of storing the Dialog for the boss

// Mário 26/10/2020 - Creation of the class
// Mário 28/10/2020 - Check if index exists
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue_MarioFernandes : MonoBehaviour
{
    public List<Dialogue> m_dialogue;
    
    ///<summary>Call the dialog you want, giving it the number on the index</summary>
	public void TriggerDialogue (int index)
	{
		if(index < m_dialogue.Capacity - 1 && index >= 0)
		FindObjectOfType<DialogueManager_MarioFernandes>().StartDialogue(m_dialogue[index]);
		else
		print("Invalid dialog index");
	}

}
