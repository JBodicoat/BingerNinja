//Joao Beijinho

//Joao Beijinho 05/11/2020 - Created script, enum for the type of item, a switch to activate the objects functionality and the door functionality
//Joao Beijinho 06/11/2020 - Created freezer functionality along with FreezerLockAndDamage IEnumerator

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
        Computer,
        Freezer,
    }

    public ObjectType m_functionality;

    private GameObject m_freezerBoss;//Reference enemy to do damage
    private FreezerTrigger_JoaoBeijinho m_freezerArea;//Reference script that checks if enemy is in the freezer
    private MeleeEnemy_SebastianMol enemyOne, enemyTwo, enemyThree;

    private void Start()
    {
        enemyOne = GameObject.Find("EnemyOne").GetComponent<MeleeEnemy_SebastianMol>();
        enemyTwo = GameObject.Find("EnemyTwo").GetComponent<MeleeEnemy_SebastianMol>();
        enemyThree = GameObject.Find("EnemyThree").GetComponent<MeleeEnemy_SebastianMol>();
    }
    public void ActivateObject()//Call this function to activate object functionality
    {
        switch (m_functionality)//Define object functionality
        {
            case ObjectType.Door:
                gameObject.GetComponent<Collider2D>().enabled = false;//Unlock door
                break;
            case ObjectType.Light:
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;//Lights On
                break;
            case ObjectType.Computer:
                print("This a computer");
                enemyOne.StunEnemyWithDeleyFunc(3.0f);
                enemyTwo.StunEnemyWithDeleyFunc(3.0f);
                enemyThree.StunEnemyWithDeleyFunc(3.0f);
                //make computer sound
                break;
            case ObjectType.Freezer:
                //gameObject.GetComponent<Collider2D>().enabled = true;//Lock freezer door
                StartCoroutine(FreezerLockAndDamage(5, 1.5f, 3f));
                break;
        }
    }

    IEnumerator FreezerLockAndDamage(int maxTicks, float damageInterval, float damageAmount)
    {
        if (m_freezerArea.m_enemyInFreezer == true)
        {
            for (int i = 0; i < maxTicks; i++)
            {
                print("Dealt " + damageAmount + " damage");
                m_freezerBoss.GetComponent<BaseEnemy_SebastianMol>().m_health -= damageAmount;//Do damage, player for test
                print("Enemy HP: " + m_freezerBoss.GetComponent<BaseEnemy_SebastianMol>().m_health);
                yield return new WaitForSeconds(damageInterval);//Delay before doing damage again

                i++;
            }

        }

        gameObject.GetComponent<Collider2D>().enabled = false;//Unlock freezer door
    }

    void Awake()
    {
        m_freezerBoss = GameObject.Find("ThugEnemy (1)");//Instance of ThugEnemy for testing
        m_freezerArea = FindObjectOfType<FreezerTrigger_JoaoBeijinho>();
    }
}