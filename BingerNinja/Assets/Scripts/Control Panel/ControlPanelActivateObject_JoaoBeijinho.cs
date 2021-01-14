//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created script, enum for the type of item, a switch to activate the objects functionality and the door functionality
//Joao Beijinho 06/11/2020 - Created freezer functionality along with FreezerLockAndDamage IEnumerator
//Joao Beijinho 14/11/2020 - Created StunLight functionality for yakuza boss fight along with OnTriggerEnter2D and "Stunning Lights" variables
//Joao Beijinho 19/11/2020 - Made m_stunLight variable public
//                           Changed FreezerLockAndDamage() to work individualy for each enemy, and lock freezer even if no enemy is inside
//                           Added cooldown to the freezer usage and one usage(not spammable)
//Joao Beijinho 27/11/2020 - Created LightColor() to change the color of the light to a color of the ColorChanger()
//Joao Beijinho 07/12/2020 - Replaced activation of sprite on stun light to activate/deactivate gameObject
//Joao Beijinho 12/01/2021 - Turn levelScripting.drawFreezer TRUE when freezer IS in use
//                           Turn levelScripting.drawFreezer FALSE when freezer ISN'T in use

using System.Collections;
using UnityEngine;

/// <summary>
/// This class declares which type of item the object is and what functionality each type of item has
/// </summary>
public class ControlPanelActivateObject_JoaoBeijinho : MonoBehaviour
{
    public enum a
    {
        q,
        w,
        e,
        r,
        t,
        y,
    }

    public a u;

    protected FreezerTrigger_JoaoBeijinho i;//Reference script that checks if enemy is in the freezer
    private BaseEnemy_SebastianMol o;
    private ColorChanger_Jann p;
    private LevelScripting s;

    public int d;
    public float f;
    public float g;
    public float j;
    private bool k = false;
    public bool[] l = new bool[] {false, false, false};
    private Color z;

    public bool h = false; //stun effect on/off

    public void x()//Call this function to activate object functionality
    {
        switch (u)//Define object functionality
        {
            case a.q:
                gameObject.GetComponent<Collider2D>().enabled = false;//Unlock door
                break;
            case a.w:
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;//Light On
                break;
            case a.e:
                if (gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);//Light On
                    Q();
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case a.r:
                //make computer sound
                break;
            case a.t:
                if (!k)
                {
                    s.c = true;//Enable freezer
                    StartCoroutine(v());
                }
                break;
            case a.y:
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                break;
        }
    }

    private IEnumerator v()
    {
        k = true;
        for (int i = 0; i < d; i++)
        {
            foreach (Collider2D n in this.i.b)
            {
                o = n.GetComponent<BaseEnemy_SebastianMol>();

                o.WB(f);
                o.O -= g;//Deal damage
                o.WA();
            }

            yield return new WaitForSeconds(f);//Delay before doing damage again
        }
        
        s.c = false;//Unlock freezer door
        yield return new WaitForSeconds(j);
    }

    private void m()
    {
        if (l[0] == true)
        {
            z = p.w;
        }
        else if (l[1] == true)
        {
            z = p.e;
        }
        else if (l[2] == true)
        {
            z = p.r;
        }

        gameObject.GetComponent<SpriteRenderer>().color = z;
    }

    private void Q()
    {
        if (gameObject.activeSelf)
        {
            h = true;
        }
        else
        {
            h = false;
        }
    }

    void Awake()
    {
        s = GameObject.Find("Player").GetComponent<LevelScripting>();
        i = FindObjectOfType<FreezerTrigger_JoaoBeijinho>();
        p = FindObjectOfType<ColorChanger_Jann>();

        if (gameObject.CompareTag(Tags_JoaoBeijinho.m_lightTag))
        {
            m();
        }
    }
}