// Mário Fernandes
/// My class takes care of Displaying the Dialog on the screen

// Mário 24/10/2020 - Creation of the class
// Mário 24/10/2020 - Read from csvFile

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

		print("aa");
		GameObject dialogtrig;
		string[] lines = csvFile.text.Split ("\n" [0]);
		for (int i = 0; i < lines.Length; i++)
		{
			string[] parts = lines[i].Split ("," [0]);

			print(parts[0]);
			
			if(parts[0] == level.ToString())
			{
				dialogtrig = null;
				

				dialogtrig = GameObject.Find(parts[1]).transform.Find("DialogTrigger").gameObject;

				if(dialogtrig)
				{
					
					dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>().dialogue.name = parts[1];

					string[] a = new string[parts.Length - 2];
					for (int u = 2; u < parts.Length; u++)
					{
						a[u-2] = parts[u];
					}				
					
					dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>().dialogue.sentences = a;					
					
				}
			}
		}
	}

	private void Awake() {
		
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
