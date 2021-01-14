// Mário Fernandes

// Mário 24/10/2020 - Creation of the class
// Mário 25/10/2020 - Read from m_csvFile
// Mário 26/10/2020 - Ajust Dialogue to the boss Dialogue script
// Mário 28/10/2020 - Optimisation and Stop player whene in dialogs
// Mário 06/11/2020 - Dialog Title update, Pause Systems, Use "|" to saperate Dialogues
// Mário 13/11/2020 - Solve "," bug and stop AI when in dialog
// Jann  07/11/2020 - Added a quick check to swap the dialogue file based on the settings
// Jann  25/11/2020 - Added in-game language change
// Louie 28/11/2020 - Added weapon ui animation code
// Mário 29/11/2020 - PassDialogue using controller
// Louie 01/12/2020 - Weapon UI animations
// Mário 12/12/2020 - apply hotkey to pass dialog, show all txt if button pressed mid sentence, addapt to big senetences, play sounds on dialog

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// My class takes care of Displaying the Dialog on the screen
/// </summary>
public class DialogueManager_MarioFernandes : MonoBehaviour
{
    public GameObject m_dialogBox;
    public Text m_nameText;
    public Text m_dialogueText;
    public float m_TextAnimationSpeed = 0;
   
     Queue<string> q;

    public TextAsset m_csvFile;
    public TextAsset m_csvFilePortuguese;

    string w = "DialogTrigger";
    PlayerController_JamieG e;

     GameObject[] r;
     WeaponUI_LouieWilliamson t;

     bool y = false;
    public void LoadLanguageFile()
    {
        WT u = SaveLoadSystem_JamieG.QU();

        if(u.QO != null && u.QO.Equals("Portuguese"))
        {
            m_csvFile = m_csvFilePortuguese;
        };
        
        LoadDialog(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartDialogue(Dialogue i)
    {

        ///////////////////
        //Insert Start Animation here if needed
        ///////////////////
        t.QF(false);
        PauseGame();        

        m_dialogBox.SetActive(true);

        m_nameText.text = i.m_name;

        q.Clear();

        foreach (string o in i.m_sentences)
        {
            q.Enqueue(o);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(y)
        {
        y = !y;
        }
        else{
           
            if (q.Count == 0)
            {
                EndDialogue();
                return;
            }            

            string p =  q.Dequeue();

            string a = "";


            //Detect if the sentence is biger that the dialog box and cut extra in a new sentence
            if(p.Length > 181)
            {
                for (int i = 181; i < p.Length; i++)
                {
                    a += p.ToCharArray()[i];                    
                }
                q.Enqueue(a);
            }

            //Letter by letter animation effects
            ////////////////
            StopAllCoroutines();
            StartCoroutine(TypeSentence(p));
            ////////////////
        }

    }

    IEnumerator TypeSentence(string s)
    {
        PlayTrack_Jann.Instance.EM(AudioFiles.Sound_DialogSFX);
        y = true;
        m_dialogueText.text = "";
        foreach (char d in s.ToCharArray())
        {
            if(d == ' ')
            PlayTrack_Jann.Instance.EM(AudioFiles.Sound_DialogSFX);

            m_dialogueText.text += d;
            yield return new WaitForSeconds(m_TextAnimationSpeed);

            if(!y)
            {
                m_dialogueText.text = s;
                break;
            }
            
        }
        y = false;
    }

    void EndDialogue()
    {
        //End Effect

        m_dialogBox.SetActive(false);

        t.QF(true);
        ResumeGame();
    }

	///<summary>Load the Level dialog from CSV doc</summary>
    void LoadDialog(int f = 0)
    {
        GameObject g;
        GameObject h;

		//Split Colunes using "\n" as reference
        string[] j = m_csvFile.text.Split("\n"[0]);

        for (int i = 0; i < j.Length; i++)
        {
			//Split sentence using "," as reference
            List<string> k = j[i].Split((char)9).ToList();

			//Delete empty spaces
            if (k[0] == f.ToString())
            {
                //Remove unecessary spaces
                for (int e = k.Count - 1; e >= 0; e--)
                {
                    if (k[e] == "" || k[e][0] == (char)13)
                    {
                        k.RemoveAt(e);
                    }
                }


                h = null;
                g = null;

                g = GameObject.Find(k[1]);
                h = g.transform.Find(w)?.gameObject;

                //Use Dialog triger For normal gameobjects and Boss Dialog to Bosses
                if (h)
                {
                    DialogueTrigger_MarioFernandes l = h.GetComponent<DialogueTrigger_MarioFernandes>();
                    if(l)
                    {
                        //Give the m_name in the VCs file to the dialog
                        l.m_dialogue.m_name = k[2];

                        //Remove level and m_name value
                        k.RemoveRange(0, 3);
                        l.m_dialogue.m_sentences = k.ToArray();

                    }
                }
				//Check if Boss DIalog existes on the scene
                else if (g.GetComponent<BossDialogue_MarioFernandes>())
                {

                    Dialogue a = new Dialogue();
                    //Give the m_name in the VCs file to the dialog
                    a.m_name = k[2];

                    //Remove level and m_name value
                    k.RemoveRange(0, 3);
                    a.m_sentences = k.ToArray();
                    g.GetComponent<BossDialogue_MarioFernandes>().m_dialogue.Add(a);
                }
            }
        }
    }

    public void PauseGame()
    {
        e.OnDisable();
        e.GetComponentInParent<PlayerHealthHunger_MarioFernandes>().m_paused = true;
        e.GetComponentInParent<EffectManager_MarioFernandes>().m_paused = true;
        r = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in r)
        {
            Enemy.GetComponentInParent<BaseEnemy_SebastianMol>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        e.OnEnable();
        e.GetComponentInParent<PlayerHealthHunger_MarioFernandes>().m_paused = false;
        e.GetComponentInParent<EffectManager_MarioFernandes>().m_paused = false;
        r = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject ç in r)
        {
            ç.GetComponentInParent<BaseEnemy_SebastianMol>().enabled = true;
        }
        
    }
    // Use this for initialization
    void Start()
    {
        m_dialogBox = m_nameText.transform.parent.gameObject;
        e = FindObjectOfType<PlayerController_JamieG>();
        t = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
        q = new Queue<string>();
        
        LoadDialog(SceneManager.GetActiveScene().buildIndex);


        LoadLanguageFile();
    }

     void Update() {
        if(m_dialogBox.active && e.m_passDialogue.triggered)
        {
            DisplayNextSentence();
        }
    }
}

[Serializable]
public struct Dialogue
{

    public string m_name;

    [TextArea(3, 10)]
    public string[] m_sentences;

}
