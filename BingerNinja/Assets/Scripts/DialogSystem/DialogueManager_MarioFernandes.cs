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
    public GameObject q;
    public Text w;
    public Text e;
    public float r = 0;
   
    private Queue<string> t;

    public TextAsset y;
    public TextAsset u;

    string o = "DialogTrigger";
    PlayerController_JamieG p;

    private GameObject[] a;
    private WeaponUI_LouieWilliamson s;

    private bool d = false;
    public void f()
    {
        o p = SaveLoadSystem_JamieG.i();

        if(p.s != null && p.s.Equals("Portuguese"))
        {
            y = u;
        };
        
        E(SceneManager.GetActiveScene().buildIndex);
    }

    public void f(w h)
    {

        ///////////////////
        //Insert Start Animation here if needed
        ///////////////////
        s.g(false);
        j();        

        q.SetActive(true);

        w.text = h.k;

        t.Clear();

        foreach (string l in h.z)
        {
            t.Enqueue(l);
        }
        
        x();
    }

    public void x()
    {
        if(d)
        {
        d = !d;
        }
        else{
           
            if (t.Count == 0)
            {
                c();
                return;
            }            

            string v =  t.Dequeue();

            string b = "";


            //Detect if the sentence is biger that the dialog box and cut extra in a new sentence
            if(v.Length > 181)
            {
                for (int i = 181; i < v.Length; i++)
                {
                    b += v.ToCharArray()[i];                    
                }
                t.Enqueue(b);
            }

            //Letter by letter animation effects
            ////////////////
            StopAllCoroutines();
            StartCoroutine(n(v));
            ////////////////
        }

    }

    IEnumerator n(string m)
    {
        PlayTrack_Jann.Instance.WS(AudioFiles.p);
        d = true;
        e.text = "";
        foreach (char Q in m.ToCharArray())
        {
            if(Q == ' ')
            PlayTrack_Jann.Instance.WS(AudioFiles.p);

            e.text += Q;
            yield return new WaitForSeconds(r);

            if(!d)
            {
                e.text = m;
                break;
            }
            
        }
        d = false;
    }

    void c()
    {
        //End Effect

        q.SetActive(false);

        s.g(true);
        W();
    }

	///<summary>Load the Level dialog from CSV doc</summary>
    void E(int R = 0)
    {
        GameObject T;
        GameObject Y;

		//Split Colunes using "\n" as reference
        string[] U = y.text.Split("\n"[0]);

        for (int i = 0; i < U.Length; i++)
        {
			//Split sentence using "," as reference
            List<string> I = U[i].Split((char)9).ToList();

			//Delete empty spaces
            if (I[0] == R.ToString())
            {
                //Remove unecessary spaces
                for (int e = I.Count - 1; e >= 0; e--)
                {
                    if (I[e] == "" || I[e][0] == (char)13)
                    {
                        I.RemoveAt(e);
                    }
                }


                Y = null;
                T = null;

                T = GameObject.Find(I[1]);
                Y = T.transform.Find(o)?.gameObject;

                //Use Dialog triger For normal gameobjects and Boss Dialog to Bosses
                if (Y)
                {
                    DialogueTrigger_MarioFernandes O = Y.GetComponent<DialogueTrigger_MarioFernandes>();
                    if(O)
                    {
                        //Give the m_name in the VCs file to the dialog
                        O.P.k = I[2];

                        //Remove level and m_name value
                        I.RemoveRange(0, 3);
                        O.P.z = I.ToArray();

                    }
                }
				//Check if Boss DIalog existes on the scene
                else if (T.GetComponent<BossDialogue_MarioFernandes>())
                {

                    w a = new w();
                    //Give the m_name in the VCs file to the dialog
                    a.k = I[2];

                    //Remove level and m_name value
                    I.RemoveRange(0, 3);
                    a.z = I.ToArray();
                    T.GetComponent<BossDialogue_MarioFernandes>().q.Add(a);
                }
            }
        }
    }

    public void j()
    {
        p.A();
        p.GetComponentInParent<PlayerHealthHunger_MarioFernandes>().S = true;
        p.GetComponentInParent<EffectManager_MarioFernandes>().D = true;
        a = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject F in a)
        {
            F.GetComponentInParent<BaseEnemy_SebastianMol>().enabled = false;
        }
    }

    public void W()
    {
        p.OnEnable();
        p.GetComponentInParent<PlayerHealthHunger_MarioFernandes>().S = false;
        p.GetComponentInParent<EffectManager_MarioFernandes>().D = false;
        a = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject G in a)
        {
            G.GetComponentInParent<BaseEnemy_SebastianMol>().enabled = true;
        }
        
    }
    // Use this for initialization
    void Start()
    {
        q = w.transform.parent.gameObject;
        p = FindObjectOfType<PlayerController_JamieG>();
        s = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();
        t = new Queue<string>();
        
        E(SceneManager.GetActiveScene().buildIndex);


        f();
    }

    private void Update() {
        if(q.active && p.H.triggered)
        {
            x();
        }
    }
}

[Serializable]
public struct w
{

    public string k;

    [TextArea(3, 10)]
    public string[] z;

}
