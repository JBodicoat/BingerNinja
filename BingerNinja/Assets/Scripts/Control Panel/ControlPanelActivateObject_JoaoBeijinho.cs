//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created script, enum for the type of item, a switch to activate the objects functionality and the door functionality
//Joao Beijinho 06/11/2020 - Created freezer functionality along with FreezerLockAndDamage IEnumerator
//Joao Beijinho 14/11/2020 - Created StunLight functionality for yakuza boss fight along with OnTriggerEnter2D and "Stunning Lights" variables
//Joao Beijinho 19/11/2020 - Made m_stunLight variable public

using System.Collections;
using System.Collections.Generic;
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
    }

    public ObjectType m_functionality;

    private GameObject m_freezerBoss;//Reference enemy to do damage
    protected FreezerTrigger_JoaoBeijinho m_freezerArea;//Reference script that checks if enemy is in the freezer

    [Header("Freezer Settings")]
    public int m_maxTicks;
    public float m_damageInterval;
    public float m_damageAmount;

    [Header("Stun Light(shouldn't be touched)")]
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
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;//Light On
                StunLight();
                break;
            case ObjectType.Computer:
                print("This a computer");
                //make computer sound
                break;
            case ObjectType.Freezer:
                //gameObject.GetComponent<Collider2D>().enabled = true;//Lock freezer door
                StartCoroutine(FreezerLockAndDamage());
                break;
        }
    }

    private IEnumerator FreezerLockAndDamage()
    {
        if (m_freezerArea.m_enemyInFreezer == true)
        {
            for (int i = 0; i < m_maxTicks; i++)
            {
                print("Dealt " + m_damageAmount + " damage");
                m_freezerBoss.GetComponent<BaseEnemy_SebastianMol>().m_health -= m_damageAmount;//Do damage, ThugEnemy for test
                print("Enemy HP: " + m_freezerBoss.GetComponent<BaseEnemy_SebastianMol>().m_health);
                yield return new WaitForSeconds(m_damageInterval);//Delay before doing damage again
            }

        }

        gameObject.GetComponent<Collider2D>().enabled = false;//Unlock freezer door
    }

    private void StunLight()
    {
        if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
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
        m_freezerBoss = GameObject.Find("ThugEnemy (1)");//Instance of ThugEnemy for testing
        m_freezerArea = FindObjectOfType<FreezerTrigger_JoaoBeijinho>();
    }
}