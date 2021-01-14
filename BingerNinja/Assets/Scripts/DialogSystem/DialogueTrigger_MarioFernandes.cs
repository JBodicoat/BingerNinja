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
	public w P;

	bool q = true;

	///<summary>Call the dialog </summary>
	public void w ()
	{
		FindObjectOfType<DialogueManager_MarioFernandes>().f(P);
	}

	private void OnTriggerEnter2D(Collider2D e) {
		if(e.tag == "Player" && q)
		{
			w();
			q = false;
			gameObject.SetActive(false);
		}
	}	
}


