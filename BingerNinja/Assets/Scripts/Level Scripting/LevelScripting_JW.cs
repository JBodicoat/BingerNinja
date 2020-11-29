using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelScripting_JW : MonoBehaviour
{
    GameObject levelLiftTrigger, keyTrigger, doorCloseTrigger;
    BossDialogue_MarioFernandes bossDialogue;
    BaseEnemy_SebastianMol boss;
    bool levelBossIntro = false, bossDead = false, doorsClosed = false;
    Tilemap walls1, walls2;
    public Tile bottomDoorTile, topDoorTile;

    
    private void Awake()
    {
        walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();

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
            doorCloseTrigger = GameObject.Find("Pressure pad intro").transform.Find("DialogTrigger").gameObject;
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

    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().buildIndex == 14)
        {
            //Mechanic for closing the doors behind the player on lvl 14
            if (other.gameObject == doorCloseTrigger && !doorsClosed)
            {
                
                doorsClosed = true;
            }
        }
        
    }
    
}
