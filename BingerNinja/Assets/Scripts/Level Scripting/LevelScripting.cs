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
    bool enemyDead = false, keyUsed = false, levelBossIntro, bossDead = false, doorsClosed = false;
    GameObject levelLiftTrigger, keyTrigger, keyTriggerTwo, doorCloseTrigger, dialogBox;
    BaseEnemy_SebastianMol boss, level2Enemy1, level2Enemy2;
    BossDialogue_MarioFernandes bossDialogue, level2End;
    Tilemap objInfWalls, objWalls, objBehWalls, walls2;
    Tile bottomDoorTile, topDoorTile;
    int bossDialogIndex = 0;

    #endregion

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_Map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_Map").GetComponent<Tilemap>();
        }
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
            walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
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
            walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 9 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            boss = GameObject.Find("Yakuza Leader").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.Find("Yakuza Leader").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 12 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelLiftTrigger = GameObject.Find("Level 14 Lift");
            keyTrigger = GameObject.Find("Key");
            doorCloseTrigger = GameObject.Find("Pressure pad intro").transform.Find("DialogTrigger").gameObject;
            bottomDoorTile = objWalls.GetTile<Tile>(new Vector3Int(12, 26, 0));
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
            bottomDoorTile = objWalls.GetTile<Tile>(new Vector3Int(12, 26, 0));
            topDoorTile = objWalls.GetTile<Tile>(new Vector3Int(13, 27, 0));
        }
        if (SceneManager.GetActiveScene().buildIndex == 18)
        {
            boss = GameObject.Find("SpaceNinjaBOSS").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.Find("SpaceNinjaBOSS").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 18 Lift");
            dialogBox = GameObject.Find("DialogBox");
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
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelLiftTrigger.SetActive(false);
            objWalls.SetTile(new Vector3Int(12, 26, 0), null);
            objWalls.SetTile(new Vector3Int(12, 25, 0), null);
            objWalls.SetTile(new Vector3Int(12, 24, 0), null);
            walls2.SetTile(new Vector3Int(13, 27, 0), null);
            walls2.SetTile(new Vector3Int(13, 26, 0), null);
            walls2.SetTile(new Vector3Int(13, 25, 0), null);
        }
        if (SceneManager.GetActiveScene().buildIndex == 15)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 15 Lift");
        }
        if (SceneManager.GetActiveScene().buildIndex == 18)
        {
            levelLiftTrigger.SetActive(false);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !keyUsed)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                Debug.Log("key worked");
                objInfWalls.SetTile(new Vector3Int(7, 14, 0), null);
                objInfWalls.SetTile(new Vector3Int(6, 14, 0), null);
                objInfWalls.SetTile(new Vector3Int(5, 14, 0), null);

                objWalls.SetTile(new Vector3Int(4, 13, 0), null);
                objWalls.SetTile(new Vector3Int(5, 13, 0), null);
                objWalls.SetTile(new Vector3Int(6, 13, 0), null);

            }
            
        }
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
                walls2.SetTile(new Vector3Int(26, 10, 0), null);
                walls2.SetTile(new Vector3Int(26, 11, 0), null);
                walls2.SetTile(new Vector3Int(26, 12, 0), null);
                objWalls.SetTile(new Vector3Int(25, 9, 0), null);
                objWalls.SetTile(new Vector3Int(25, 10, 0), null);
                objWalls.SetTile(new Vector3Int(25, 11, 0), null);
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
                walls2.SetTile(new Vector3Int(23, 28, 0), null);
                walls2.SetTile(new Vector3Int(23, 29, 0), null);
                walls2.SetTile(new Vector3Int(23, 30, 0), null);

                objWalls.SetTile(new Vector3Int(22, 27, 0), null);
                objWalls.SetTile(new Vector3Int(22, 28, 0), null);
                objWalls.SetTile(new Vector3Int(22, 29, 0), null);

            }
            if (!keyTriggerTwo.activeInHierarchy)
            {
                walls2.SetTile(new Vector3Int(30, 10, 0), null);
                walls2.SetTile(new Vector3Int(30, 11, 0), null);
                walls2.SetTile(new Vector3Int(30, 12, 0), null);

                objWalls.SetTile(new Vector3Int(29, 9, 0), null);
                objWalls.SetTile(new Vector3Int(29, 10, 0), null);
                objWalls.SetTile(new Vector3Int(29, 11, 0), null);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 9 && !bossDead)
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
        if (SceneManager.GetActiveScene().buildIndex == 11 && !keyUsed)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                Debug.Log("key worked");
                //door top
                objInfWalls.SetTile(new Vector3Int(36, 32, 0), null);
                objInfWalls.SetTile(new Vector3Int(37, 32, 0), null);
                objInfWalls.SetTile(new Vector3Int(38, 32, 0), null);

                objWalls.SetTile(new Vector3Int(35, 31, 0), null);
                objWalls.SetTile(new Vector3Int(36, 31, 0), null);
                objWalls.SetTile(new Vector3Int(37, 31, 0), null);

                //door mid-right
                objInfWalls.SetTile(new Vector3Int(33, 19, 0), null);
                objInfWalls.SetTile(new Vector3Int(33, 18, 0), null);
                objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);

                objWalls.SetTile(new Vector3Int(32, 18, 0), null);
                objWalls.SetTile(new Vector3Int(32, 17, 0), null);
                objWalls.SetTile(new Vector3Int(32, 16, 0), null);

                //door right
                objInfWalls.SetTile(new Vector3Int(35, 31, 0), null);
                objInfWalls.SetTile(new Vector3Int(36, 31, 0), null);
                objInfWalls.SetTile(new Vector3Int(37, 31, 0), null);

                objWalls.SetTile(new Vector3Int(38, 10, 0), null);
                objWalls.SetTile(new Vector3Int(37, 10, 0), null);
                objWalls.SetTile(new Vector3Int(36, 10, 0), null);

                //door left
                objInfWalls.SetTile(new Vector3Int(11, 26, 0), null);
                objInfWalls.SetTile(new Vector3Int(12, 26, 0), null);
                objInfWalls.SetTile(new Vector3Int(13, 26, 0), null);

                objWalls.SetTile(new Vector3Int(10, 25, 0), null);
                objWalls.SetTile(new Vector3Int(11, 25, 0), null);
                objWalls.SetTile(new Vector3Int(12, 25, 0), null);
            }
        }
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
        if (SceneManager.GetActiveScene().buildIndex == 13 && !keyUsed)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                Debug.Log("key worked");
                //door top
                objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);
                objInfWalls.SetTile(new Vector3Int(34, 17, 0), null);
                objInfWalls.SetTile(new Vector3Int(35, 17, 0), null);

                objWalls.SetTile(new Vector3Int(34, 16, 0), null);
                objWalls.SetTile(new Vector3Int(33, 16, 0), null);
                objWalls.SetTile(new Vector3Int(32, 16, 0), null);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            if (!keyTrigger.activeInHierarchy)
            {
                levelLiftTrigger.SetActive(true);
            }

        }
        if (SceneManager.GetActiveScene().buildIndex == 15 && !bossDead)
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
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            //If pressure pad activated then reopen the above doors (awaiting pressure pad prefab)
            /*
             * Below to go into the if function
            walls1.SetTile(new Vector3Int(12, 26, 0), null);
            walls1.SetTile(new Vector3Int(12, 25, 0), null);
            walls1.SetTile(new Vector3Int(12, 24, 0), null);
            walls2.SetTile(new Vector3Int(13, 27, 0), null);
            walls2.SetTile(new Vector3Int(13, 26, 0), null);
            walls2.SetTile(new Vector3Int(13, 25, 0), null);
            */
        }
        if (SceneManager.GetActiveScene().buildIndex == 18 && !bossDead)
        {
            if (!levelBossIntro && !dialogBox.activeInHierarchy)
            {
                bossDialogue.TriggerDialogue(bossDialogIndex);
                bossDialogIndex++;
                if (bossDialogIndex == 8)
                {
                    levelBossIntro = true;
                }
            }
            if (boss.m_health <= 0)
            {

                bossDialogue.TriggerDialogue(9);
                levelLiftTrigger.SetActive(true);
                bossDead = true;

            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            //Mechanic for closing the doors behind the player on lvl 14
            if (other.gameObject == doorCloseTrigger && !doorsClosed)
            {
                objWalls.SetTile(new Vector3Int(12, 26, 0), bottomDoorTile);
                objWalls.SetTile(new Vector3Int(12, 25, 0), bottomDoorTile);
                objWalls.SetTile(new Vector3Int(12, 24, 0), bottomDoorTile);
                walls2.SetTile(new Vector3Int(13, 27, 0), topDoorTile);
                walls2.SetTile(new Vector3Int(13, 26, 0), topDoorTile);
                walls2.SetTile(new Vector3Int(13, 25, 0), topDoorTile);
                doorsClosed = true;
            }
        }

    }
}
