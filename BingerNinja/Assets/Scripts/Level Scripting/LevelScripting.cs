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
    bool enemyDead = false, keyUsed = false, levelThreeBossIntro, businessBossDead = false;
    GameObject levelTwoLiftTrigger, levelThreeLiftTrigger, keyTrigger;
    BaseEnemy_SebastianMol businessBoss, level2Enemy1, level2Enemy2;
    BossDialogue_MarioFernandes bossDialogue, level2End;
    Tilemap objInfWalls, objWalls;
    
    #endregion

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level2End = GameObject.Find("EndLevelTwo").GetComponent<BossDialogue_MarioFernandes>();
            level2Enemy1 = GameObject.Find("Enemy 1").GetComponent<BaseEnemy_SebastianMol>();
            level2Enemy2 = GameObject.Find("Enemy 2").GetComponent<BaseEnemy_SebastianMol>();
            levelTwoLiftTrigger = GameObject.Find("Level 2 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            businessBoss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelThreeLiftTrigger = GameObject.Find("Level 3 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            levelTwoLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            levelThreeLiftTrigger.SetActive(false);
        }        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && !enemyDead)
        {
            if (level2Enemy1.m_health <= 0 || level2Enemy2.m_health <= 0)
            {
                levelTwoLiftTrigger.SetActive(true);
                level2End.TriggerDialogue(0);
                enemyDead = true;

            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 && !businessBossDead)
        {
            if (businessBoss.m_health <= 0)
            {
                //dramatic death SE
                //freeze on enemy as he dies  
                bossDialogue.TriggerDialogue(0);
                levelThreeLiftTrigger.SetActive(true);
                businessBossDead = true;
              
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
    }
   
}
