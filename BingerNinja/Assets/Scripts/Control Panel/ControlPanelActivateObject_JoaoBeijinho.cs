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
    public enum ObjectType
    {
        Door,
        Light,
        StunLight,
        Computer,
        Freezer,
        SecurityCamera,
    }

    public ObjectType m_functionality;

    protected FreezerTrigger_JoaoBeijinho m_freezerArea;//Reference script that checks if enemy is in the freezer
     BaseEnemy_SebastianMol a;
     ColorChanger_Jann b;
     LevelScripting c;

    public int m_maxTicks;
    public float m_damageInterval;
    public float m_damageAmount;
    public float m_freezerCooldown;
     bool d = false;
    public bool[] m_pickColor = new bool[] {false, false, false};
     Color e;

    public bool m_stunLight = false; //stun effect on/off

    public void ActivateObject()//Call this function to activate object functionality
    {
        switch (m_functionality)//Define object functionality
        {
            case ObjectType.Door:
                gameObject.GetComponent<Collider2D>().enabled = false;//Unlock door
                break;
            case ObjectType.Light:
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;//Light On
                break;
            case ObjectType.StunLight:
                if (gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);//Light On
                    p();
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case ObjectType.Computer:
                //make computer sound
                break;
            case ObjectType.Freezer:
                if (!d)
                {
                    c.drawFreezer = true;//Enable freezer
                    StartCoroutine(r());
                }
                break;
            case ObjectType.SecurityCamera:
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                break;
        }
    }

     IEnumerator r()
    {
        d = true;
        for (int i = 0; i < m_maxTicks; i++)
        {
            foreach (Collider2D enemy in m_freezerArea.m_enemyList)
            {
                a = enemy.GetComponent<BaseEnemy_SebastianMol>();

                a.RO(m_damageInterval);
                a.CO -= m_damageAmount;//Deal damage
                a.EB();
            }

            yield return new WaitForSeconds(m_damageInterval);//Delay before doing damage again
        }
        
        c.drawFreezer = false;//Unlock freezer door
        yield return new WaitForSeconds(m_freezerCooldown);
    }

     void s()
    {
        if (m_pickColor[0] == true)
        {
            e = b.m_colorOutGrey60;
        }
        else if (m_pickColor[1] == true)
        {
            e = b.m_colorOutGrey122;
        }
        else if (m_pickColor[2] == true)
        {
            e = b.m_colorOutGrey174;
        }

        gameObject.GetComponent<SpriteRenderer>().color = e;
    }

     void p()
    {
        if (gameObject.activeSelf)
        {
            m_stunLight = true;
        }
        else
        {
            m_stunLight = false;
        }
    }

    void Awake()
    {
        c = GameObject.Find("Player").GetComponent<LevelScripting>();
        m_freezerArea = FindObjectOfType<FreezerTrigger_JoaoBeijinho>();
        b = FindObjectOfType<ColorChanger_Jann>();

        if (gameObject.CompareTag(Tags_JoaoBeijinho.UL))
        {
            s();
        }
    }
}