using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScripting_JW : MonoBehaviour
{
    GameObject levelLiftTrigger;
    BossDialogue_MarioFernandes bossDialogue;
    BaseEnemy_SebastianMol boss;

    private void Awake()
    {

        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 12 Lift");
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            levelLiftTrigger.SetActive(false);
        }
    }
    /*1
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
                //dramatic death SE
                //freeze on enemy as he dies  
                bossDialogue.TriggerDialogue(1);
                levelLiftTrigger.SetActive(true);
                bossDead = true;

            }
        }
    }
    */
}
