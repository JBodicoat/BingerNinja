// Mário Fernandes
/// My class takes care of Displaying the Dialog on the screen

// Mário 24/10/2020 - Creation of the class
// Mário 25/10/2020 - Read from csvFile
// Mário 26/10/2020 - Ajust Dialog to the boss Dialog script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class DialogueManager_MarioFernandes : MonoBehaviour
{
 public Text nameText;
	public Text dialogueText;

	public float TextAnimationSpeed = 0; 

	private Queue<string> sentences;

	public TextAsset csvFile;
	int level = 0;
	public GameObject DialogTrigger;

	public void StartDialogue (Dialogue dialogue)
	{
        
        ///////////////////
		//Insert Start Animation here if needed
        ///////////////////
		nameText.transform.parent.gameObject.SetActive(true);

		
		nameText.text = dialogue.name;
		
		sentences.Clear();
		
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		print(dialogue.sentences[0]);
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();

        //Letter by letter animation effects
        ////////////////
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
        ////////////////

        //Show sentence
        /*
        dialogueText.text = sentence;
        */
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(TextAnimationSpeed);
		}
	}

	void EndDialogue()
	{
		//End Effect

		nameText.transform.parent.gameObject.SetActive(false);
	}

	void LoadDialog()
	{		
		GameObject Target;
		GameObject dialogtrig;

		string[] lines = csvFile.text.Split ("\n" [0]);
		for (int i = 0; i < lines.Length; i++)
		{
			List<string> parts = lines[i].Split ("," [0]).ToList();
			
			if(parts[0] == level.ToString())
			{				
				//Remove unecessary spaces
				for (int e = parts.Count-1; e >= 0; e--)
				{									
					if(parts[e] == "" || parts[e][0] == (char)13)
					{
						parts.RemoveAt(e);					
					}
				}	

				
				dialogtrig = null;
				Target = null;
				
				Target = GameObject.Find(parts[1]);
				dialogtrig = Target.transform.Find("DialogTrigger")?.gameObject ;
				//Use Dialog triger For normal gameobjects and Boss Dialog to Bosses
				if(dialogtrig && dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>())
				{				
					//Give the name in the VCs file to the dialog
					dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>().dialogue.name = parts[1];

					//Remove level and name value
					parts.RemoveRange(0,2);
					dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>().dialogue.sentences = parts.ToArray();					
					
				}				
				else if(Target.GetComponent<BossDialog_MarioFernandes>())
				{
					
					Dialogue a = new Dialogue();
					//Give the name in the VCs file to the dialog
					a.name = parts[1];

					//Remove level and name value
					parts.RemoveRange(0,2);
					a.sentences = parts.ToArray();
					Target.GetComponent<BossDialog_MarioFernandes>().dialogue.Add(a);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();

		LoadDialog();		
	}
}

[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}
