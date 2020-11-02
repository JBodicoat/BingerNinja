﻿// Mário Fernandes

// Mário 24/10/2020 - Creation of the class
// Mário 25/10/2020 - Read from m_csvFile
// Mário 26/10/2020 - Ajust Dialogue to the boss Dialogue script
// Mário 28/10/2020 - Optimisation and Stop player whene in dialogs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// My class takes care of Displaying the Dialog on the screen
/// </summary>
public class DialogueManager_MarioFernandes : MonoBehaviour
{
    public Text m_nameText;
    public Text m_dialogueText;

    public float m_TextAnimationSpeed = 0;

    private Queue<string> m_sentences;

    public TextAsset m_csvFile;

    string m_TrigerDialoguePrefab = "DialogTrigger";
    PlayerController_JamieG playerControllerScript;

    public void StartDialogue(Dialogue dialogue)
    {

        ///////////////////
        //Insert Start Animation here if needed
        ///////////////////

        playerControllerScript.m_movement.Disable();

        m_nameText.transform.parent.gameObject.SetActive(true);

        m_nameText.text = dialogue.m_name;

        m_sentences.Clear();

        foreach (string sentence in dialogue.m_sentences)
        {
            m_sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = m_sentences.Dequeue();

        //Letter by letter animation effects
        ////////////////
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        ////////////////

        //Show all sentence
        //m_dialogueText.text = sentence;
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        m_dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            m_dialogueText.text += letter;
            yield return new WaitForSeconds(m_TextAnimationSpeed);
        }
    }

    void EndDialogue()
    {
        //End Effect

        m_nameText.transform.parent.gameObject.SetActive(false);

        playerControllerScript.m_movement.Enable();
    }

	///<summary>Load the Level dialog from CSV doc</summary>
    void LoadDialog(int level = 0)
    {
        GameObject Target;
        GameObject dialogtrig;

		//Split Colunes using "\n" as reference
        string[] lines = m_csvFile.text.Split("\n"[0]);

        for (int i = 0; i < lines.Length; i++)
        {
			//Split sentence using "," as reference
            List<string> parts = lines[i].Split(","[0]).ToList();

			//Delete empty spaces
            if (parts[0] == level.ToString())
            {
                //Remove unecessary spaces
                for (int e = parts.Count - 1; e >= 0; e--)
                {
                    if (parts[e] == "" || parts[e][0] == (char)13)
                    {
                        parts.RemoveAt(e);
                    }
                }


                dialogtrig = null;
                Target = null;

                Target = GameObject.Find(parts[1]);
                dialogtrig = Target.transform.Find(m_TrigerDialoguePrefab)?.gameObject;

                //Use Dialog triger For normal gameobjects and Boss Dialog to Bosses
                if (dialogtrig)
                {
                    DialogueTrigger_MarioFernandes dialogTrigScript = dialogtrig.GetComponent<DialogueTrigger_MarioFernandes>();
                    if(dialogTrigScript)
                    {
                        //Give the m_name in the VCs file to the dialog
                        dialogTrigScript.m_dialogue.m_name = parts[1];

                        //Remove level and m_name value
                        parts.RemoveRange(0, 2);
                        dialogTrigScript.m_dialogue.m_sentences = parts.ToArray();

                    }
                }
				//Check if Boss DIalog existes on the scene
                else if (Target.GetComponent<BossDialogue_MarioFernandes>())
                {

                    Dialogue a = new Dialogue();
                    //Give the m_name in the VCs file to the dialog
                    a.m_name = parts[1];

                    //Remove level and m_name value
                    parts.RemoveRange(0, 2);
                    a.m_sentences = parts.ToArray();
                    Target.GetComponent<BossDialogue_MarioFernandes>().m_dialogue.Add(a);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController_JamieG>();

        m_sentences = new Queue<string>();

        LoadDialog(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public struct Dialogue
{

    public string m_name;

    [TextArea(3, 10)]
    public string[] m_sentences;

}