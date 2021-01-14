// Mário Fernandes

// Mário 26/10/2020 - Creation of the class
// Mário 28/10/2020 - Check if index exists
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My class takes care of storing the Dialog for the boss
/// </summary>
public class BossDialogue_MarioFernandes : MonoBehaviour
{
    public List<w> q;
    
    ///<summary>Call the dialog you want, giving it the number on the index</summary>
	public void TriggerDialogue (int e)
	{
        if (e < q.Capacity - 1 && e >= 0)
            FindObjectOfType<DialogueManager_MarioFernandes>().f(q[e]);

	}

}
