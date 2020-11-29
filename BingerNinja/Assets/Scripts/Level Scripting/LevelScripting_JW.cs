using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScripting_JW : MonoBehaviour
{
    GameObject levelLiftTrigger, keyTrigger;
    BossDialogue_MarioFernandes bossDialogue;
    BaseEnemy_SebastianMol boss;
    bool levelBossIntro = false, bossDead = false;
    
    private void Awake()
    {

        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            boss = GameObject.Find("Yakuza Boss").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 12 Lift");
        }
        if(SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelLiftTrigger = GameObject.Find("Level 14 Lift");
            keyTrigger = GameObject.Find("Key");
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelLiftTrigger.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 12 && !bossDead)
        {
            if (!levelBossIntro)
            {
                bossDialogue.TriggerDialogue(0);
                levelBossIntro = true;
            }
            if (boss.m_health <= 0)
            {
 
                bossDialogue.TriggerDialogue(1);
                levelLiftTrigger.SetActive(true);
                bossDead = true;

            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            if(!keyTrigger.activeInHierarchy)
            {
                levelLiftTrigger.SetActive(true);
            }
        }
    }
    
}
