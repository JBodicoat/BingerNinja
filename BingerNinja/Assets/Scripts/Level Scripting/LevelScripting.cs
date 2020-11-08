/*
 * Chase Wilding - 8/11/2020
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScripting : MonoBehaviour
{
    bool businessBossDead = false;
    GameObject finalTrigger;
    private void Awake()
    {
        finalTrigger = GameObject.Find("Post Fight");
        finalTrigger.SetActive(false);
    }

    private void Update()
    {
        if(!businessBossDead)
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameObject boss = GameObject.FindGameObjectWithTag("Enemy");
                
                if (boss.GetComponent<BaseEnemy_SebastianMol>().m_health <= 0)
                {
                    //dramatic death SE
                    //freeze on enemy as he dies
                    finalTrigger.SetActive(true);
                    businessBossDead = true;
                }
            }
        }
       
    }
}
