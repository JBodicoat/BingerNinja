/*
 * Chase Wilding - 8/11/2020
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelScripting : MonoBehaviour
{
    #region VARIABLES
    bool enemyDead = false, keyUsed = false, levelBossIntro, bossDead = false;
    GameObject levelLiftTrigger, keyTrigger, keyTriggerTwo;
    BaseEnemy_SebastianMol boss, level2Enemy1, level2Enemy2;
    BossDialogue_MarioFernandes bossDialogue, level2End;
    Tilemap objInfWalls, objWalls, objBehWalls;
    
    #endregion

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level2End = GameObject.Find("EndLevelTwo").GetComponent<BossDialogue_MarioFernandes>();
            level2Enemy1 = GameObject.Find("Enemy 1").GetComponent<BaseEnemy_SebastianMol>();
            level2Enemy2 = GameObject.Find("Enemy 2").GetComponent<BaseEnemy_SebastianMol>();
            levelLiftTrigger = GameObject.Find("Level 2 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 3 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            levelLiftTrigger = GameObject.Find("Level 5 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 6 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            keyTriggerTwo = GameObject.Find("Key Trigger 2");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
            objBehWalls = GameObject.Find("ObjectsBehindWalls_map").GetComponent<Tilemap>();
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            levelLiftTrigger.SetActive(false);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && !enemyDead)
        {
            if (level2Enemy1.m_health <= 0 || level2Enemy2.m_health <= 0)
            {
                levelLiftTrigger.SetActive(true);
                level2End.TriggerDialogue(0);
                enemyDead = true;

            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 && !bossDead)
        {
            if (boss.m_health <= 0)
            {
                //dramatic death SE
                //freeze on enemy as he dies  
                bossDialogue.TriggerDialogue(0);
                levelLiftTrigger.SetActive(true);
                bossDead = true;
              
            }   
        }
      
        if (SceneManager.GetActiveScene().buildIndex == 4 && !keyUsed)
        {
           if(!keyTrigger.activeInHierarchy)
            {
                objInfWalls.SetTile(new Vector3Int(25, 9, 0), null);
                objInfWalls.SetTile(new Vector3Int(25, 10, 0), null);
                objInfWalls.SetTile(new Vector3Int(25, 11, 0), null);
                objWalls.SetTile(new Vector3Int(24, 8, 0), null);
                objWalls.SetTile(new Vector3Int(24, 9, 0), null);
                objWalls.SetTile(new Vector3Int(24, 10, 0), null);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 5 && !keyUsed)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                levelLiftTrigger.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 6 && !bossDead)
        {
            if(!levelBossIntro)
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
        if (SceneManager.GetActiveScene().buildIndex == 8 && !keyUsed)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                objInfWalls.SetTile(new Vector3Int(22, 29, 0), null);
                objInfWalls.SetTile(new Vector3Int(22, 28, 0), null);
                
                objWalls.SetTile(new Vector3Int(21, 27, 0), null);
                objWalls.SetTile(new Vector3Int(21, 28, 0), null);
                
            }
            if (!keyTriggerTwo.activeInHierarchy)
            {
                objInfWalls.SetTile(new Vector3Int(29, 10, 0), null);
                objInfWalls.SetTile(new Vector3Int(29, 11, 0), null);

                objBehWalls.SetTile(new Vector3Int(28, 9, 0), null);
                objBehWalls.SetTile(new Vector3Int(28, 10, 0), null);
            }
        }
    }
   
}
