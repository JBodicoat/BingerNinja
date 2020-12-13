/*
 * Chase Wilding - 8/11/2020
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class LevelScripting : MonoBehaviour
{
    #region VARIABLES
    private bool enemyDead = false, keyUsed = false, levelBossIntro, bossDead = false, doorsClosed = false, cinematicDone = true;
    private GameObject levelLiftTrigger, keyTrigger, keyTriggerTwo, doorCloseTrigger, dialogBox;
    private BaseEnemy_SebastianMol boss, level2Enemy1, level2Enemy2;
    private BossDialogue_MarioFernandes bossDialogue, level2End;
    private Tilemap objInfWalls, walls1, objBehWalls, walls2;
    public Tile bottomDoorTile, topDoorTile;
    private int bossDialogIndex = 0;
    public int currentLevel;
    private PlayerController_JamieG controller;
    private BossIntroCineScript_AdamG cinematics;
    // Dan timeline script
    private Timeline_Script timeline;
    private float cinematicsTimer;


    #endregion

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        controller = GameObject.Find("Player").GetComponent<PlayerController_JamieG>();
        switch (currentLevel)
        {
            case 1:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    walls1 = GameObject.Find("Walls1_Map").GetComponent<Tilemap>();
                    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_Map").GetComponent<Tilemap>();
                }
                break;
            case 2:
                {
                    level2End = GameObject.Find("EndLevelTwo").GetComponent<BossDialogue_MarioFernandes>();
                    level2Enemy1 = GameObject.Find("Enemy 1").GetComponent<BaseEnemy_SebastianMol>();
                    level2Enemy2 = GameObject.Find("Enemy 2").GetComponent<BaseEnemy_SebastianMol>();
                    levelLiftTrigger = GameObject.Find("Level 2 Lift");
                }
                break;
            case 3:
                {
                    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 3 Lift");
                }
                break;
            case 4:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 5:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    levelLiftTrigger = GameObject.Find("Level 5 Lift");
                }
                break;
            case 6:
                {
                    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 6 Lift");
                }
                break;
            case 8:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    keyTriggerTwo = GameObject.Find("Key Trigger 2");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
                    objBehWalls = GameObject.Find("ObjectsBehindWalls_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 9:
                {
                    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 9 Lift");
                }
                break;
            case 11:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 12:
                {
                    boss = GameObject.Find("Yakuza Leader").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.Find("Yakuza Leader").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 12 Lift");
                    cinematics = GameObject.Find("BossIntroCinematic").GetComponent<BossIntroCineScript_AdamG>();
                }
                break;
            case 13:
                {
                    keyTrigger = GameObject.Find("Key Trigger");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 14:
                {
                    levelLiftTrigger = GameObject.Find("Level 14 Lift");
                    keyTrigger = GameObject.Find("Help");
                    doorCloseTrigger = GameObject.Find("DialogTrigger");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                    bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12, 26, 0));
                    topDoorTile = walls1.GetTile<Tile>(new Vector3Int(13, 27, 0));
                }
                break;
            case 15:
                {
                    boss = GameObject.Find("Toby the Tiger").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.Find("Toby the Tiger").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 15 Lift");
                }
                break;
            case 17:
                {
                    levelLiftTrigger = GameObject.Find("Level 17 Lift");
                    keyTrigger = GameObject.Find("Key");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 18:
                {
                    boss = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 18 Lift");
                    dialogBox = GameObject.Find("DialogBox");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 19:
                {
                    levelLiftTrigger = GameObject.Find("Level 19 Lift");
                    keyTrigger = GameObject.Find("Key Trigger");
                    // keyTrigger = GameObject.Find("My Key");
                    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 20:
                {
                    boss = GameObject.Find("Ninjaroth").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.Find("Ninjaroth").GetComponent<BossDialogue_MarioFernandes>();
                    dialogBox = GameObject.Find("DialogBox");
                    // find game object with timeline script to change playable director
                    timeline = GameObject.Find("Cinematics").GetComponent<Timeline_Script>();
                }
                break;

            default:
                break;
        }
         // int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    walls1 = GameObject.Find("Walls1_Map").GetComponent<Tilemap>();
        //    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_Map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    level2End = GameObject.Find("EndLevelTwo").GetComponent<BossDialogue_MarioFernandes>();
        //    level2Enemy1 = GameObject.Find("Enemy 1").GetComponent<BaseEnemy_SebastianMol>();
        //    level2Enemy2 = GameObject.Find("Enemy 2").GetComponent<BaseEnemy_SebastianMol>();
        //    levelLiftTrigger = GameObject.Find("Level 2 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 3 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 4)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 5)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    levelLiftTrigger = GameObject.Find("Level 5 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 6)
        //{
        //    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 6 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 8)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    keyTriggerTwo = GameObject.Find("Key Trigger 2");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    objInfWalls = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
        //    objBehWalls = GameObject.Find("ObjectsBehindWalls_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 9)
        //{
        //    boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 9 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 12)
        //{
        //    boss = GameObject.Find("Yakuza Leader").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.Find("Yakuza Leader").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 12 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 14)
        //{
        //    levelLiftTrigger = GameObject.Find("Level 14 Lift");
        //    keyTrigger = GameObject.Find("Help");
        //    doorCloseTrigger = GameObject.Find("DialogTrigger");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //    bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12, 26, 0));
        //    topDoorTile = walls1.GetTile<Tile>(new Vector3Int(13, 27, 0));
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 17)
        //{
        //    levelLiftTrigger = GameObject.Find("Level 17 Lift");
        //    keyTrigger = GameObject.Find("Key");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 18)
        //{
        //    boss = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 18 Lift");
        //    dialogBox = GameObject.Find("DialogBox");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 19)
        //{
        //    levelLiftTrigger = GameObject.Find("Level 19 Lift");
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    // keyTrigger = GameObject.Find("My Key");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 20)
        //{
        //    boss = GameObject.Find("Ninjaroth").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.Find("Ninjaroth").GetComponent<BossDialogue_MarioFernandes>();
        //    dialogBox = GameObject.Find("DialogBox");
        //}
    }

    private void Start()
    {
        switch (currentLevel)
        {

            case 2:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 3:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 5:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 6:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 9:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 12:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 14:
                {
                    levelLiftTrigger.SetActive(false);
                    walls1.SetTile(new Vector3Int(12, 26, 0), null);
                    walls1.SetTile(new Vector3Int(12, 25, 0), null);
                    walls1.SetTile(new Vector3Int(12, 24, 0), null);
                    walls2.SetTile(new Vector3Int(13, 27, 0), null);
                    walls2.SetTile(new Vector3Int(13, 26, 0), null);
                    walls2.SetTile(new Vector3Int(13, 25, 0), null);
                }
                break;
            case 17:
                {
                    levelLiftTrigger.SetActive(false);
                }
                break;
            case 18:
                {
                    boss = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
                    bossDialogue = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
                    levelLiftTrigger = GameObject.Find("Level 18 Lift");
                    dialogBox = GameObject.Find("DialogBox");
                    levelBossIntro = false;
                    levelLiftTrigger.SetActive(false);
                }
                break;
            default:
                break;
        }
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 5)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 6)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 9)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 11)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 12)
        //{
        //    levelLiftTrigger.SetActive(false);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 13)
        //{
        //    keyTrigger = GameObject.Find("Key Trigger");
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 14)
        //{
        //    levelLiftTrigger.SetActive(false);
        //    walls1.SetTile(new Vector3Int(12, 26, 0), null);
        //    walls1.SetTile(new Vector3Int(12, 25, 0), null);
        //    walls1.SetTile(new Vector3Int(12, 24, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 27, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 26, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 25, 0), null);
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 15)
        //{
        //    boss = GameObject.Find("Toby the Tiger").GetComponent<BaseEnemy_SebastianMol>();
        //    bossDialogue = GameObject.Find("Toby the Tiger").GetComponent<BossDialogue_MarioFernandes>();
        //    levelLiftTrigger = GameObject.Find("Level 15 Lift");
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 17)
        //{
        //    levelLiftTrigger.SetActive(false);

        //}
        //if (SceneManager.GetActiveScene().buildIndex == 18)
        //{
        //    levelBossIntro = false;
        //    levelLiftTrigger.SetActive(false);
        //    walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        //    walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 20)
        //{
            
        //}
    }
    private void Update()
    {
        if (controller.m_changeLevel.triggered)
        {
            SceneManager_JamieG.Instance.LoadNextLevel();
        }
        switch (currentLevel)
        {
            case 1:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            Debug.Log("key worked");
                            objInfWalls.SetTile(new Vector3Int(7, 14, 0), null);
                            objInfWalls.SetTile(new Vector3Int(6, 14, 0), null);
                            objInfWalls.SetTile(new Vector3Int(5, 14, 0), null);

                            walls1.SetTile(new Vector3Int(4, 13, 0), null);
                            walls1.SetTile(new Vector3Int(5, 13, 0), null);
                            walls1.SetTile(new Vector3Int(6, 13, 0), null);

                        }
                    }
                }
                break;
            case 2:
                {
                    if(!enemyDead)
                    {
                        if (level2Enemy1.m_health <= 0 || level2Enemy2.m_health <= 0)
                        {
                            levelLiftTrigger.SetActive(true);
                            level2End.TriggerDialogue(0);
                            enemyDead = true;

                        }
                    }
                }
                break;
            case 3:
                {
                    if(!bossDead)
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
                }
                break;
            case 4:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            walls2.SetTile(new Vector3Int(26, 10, 0), null);
                            walls2.SetTile(new Vector3Int(26, 11, 0), null);
                            walls2.SetTile(new Vector3Int(26, 12, 0), null);
                            walls1.SetTile(new Vector3Int(25, 9, 0), null);
                            walls1.SetTile(new Vector3Int(25, 10, 0), null);
                            walls1.SetTile(new Vector3Int(25, 11, 0), null);
                        }
                    }

                }
                break;
            case 5:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            levelLiftTrigger.SetActive(true);
                        }
                    }
                   
                }
                break;
            case 6:
                {
                   if(!bossDead)
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
                break;
            case 8:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            walls2.SetTile(new Vector3Int(23, 28, 0), null);
                            walls2.SetTile(new Vector3Int(23, 29, 0), null);
                            walls2.SetTile(new Vector3Int(23, 30, 0), null);

                            walls1.SetTile(new Vector3Int(22, 27, 0), null);
                            walls1.SetTile(new Vector3Int(22, 28, 0), null);
                            walls1.SetTile(new Vector3Int(22, 29, 0), null);

                        }
                        if (!keyTriggerTwo.activeInHierarchy)
                        {
                            walls2.SetTile(new Vector3Int(30, 10, 0), null);
                            walls2.SetTile(new Vector3Int(30, 11, 0), null);
                            walls2.SetTile(new Vector3Int(30, 12, 0), null);

                            walls1.SetTile(new Vector3Int(29, 9, 0), null);
                            walls1.SetTile(new Vector3Int(29, 10, 0), null);
                            walls1.SetTile(new Vector3Int(29, 11, 0), null);
                        }
                    }
                }
                break;
            case 9:
                {
                    if(!bossDead)
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
                break;
            case 11:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            //door top
                            objInfWalls.SetTile(new Vector3Int(36, 32, 0), null);
                            objInfWalls.SetTile(new Vector3Int(37, 32, 0), null);
                            objInfWalls.SetTile(new Vector3Int(38, 32, 0), null);

                            walls1.SetTile(new Vector3Int(35, 31, 0), null);
                            walls1.SetTile(new Vector3Int(36, 31, 0), null);
                            walls1.SetTile(new Vector3Int(37, 31, 0), null);

                            //door mid-right
                            objInfWalls.SetTile(new Vector3Int(33, 19, 0), null);
                            objInfWalls.SetTile(new Vector3Int(33, 18, 0), null);
                            objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);

                            walls1.SetTile(new Vector3Int(32, 18, 0), null);
                            walls1.SetTile(new Vector3Int(32, 17, 0), null);
                            walls1.SetTile(new Vector3Int(32, 16, 0), null);

                            //door right
                            objInfWalls.SetTile(new Vector3Int(35, 31, 0), null);
                            objInfWalls.SetTile(new Vector3Int(36, 31, 0), null);
                            objInfWalls.SetTile(new Vector3Int(37, 31, 0), null);

                            walls1.SetTile(new Vector3Int(38, 10, 0), null);
                            walls1.SetTile(new Vector3Int(37, 10, 0), null);
                            walls1.SetTile(new Vector3Int(36, 10, 0), null);

                            //door left
                            objInfWalls.SetTile(new Vector3Int(11, 26, 0), null);
                            objInfWalls.SetTile(new Vector3Int(12, 26, 0), null);
                            objInfWalls.SetTile(new Vector3Int(13, 26, 0), null);

                            walls1.SetTile(new Vector3Int(10, 25, 0), null);
                            walls1.SetTile(new Vector3Int(11, 25, 0), null);
                            walls1.SetTile(new Vector3Int(12, 25, 0), null);
                        }
                    }
                }
                break;
            case 12:
                {
                    if(!bossDead)
                    {
                        if (!levelBossIntro)
                        {
                            cinematicDone = false;
                            cinematics.PlayZoomIn();
                            bossDialogue.TriggerDialogue(0);
                            
                            cinematicsTimer++;
                            if(cinematicsTimer >= 2.0f)
                            {
                                cinematics.PlayZoomOut();
                                levelBossIntro = true;
                                cinematicDone = true;
                            }
                            
                        }
                        if (boss.m_health <= 0)
                        {
                            cinematics.PlayZoomIn();
                            bossDialogue.TriggerDialogue(1);
                            levelLiftTrigger.SetActive(true);
                            bossDead = true;

                        }
                    }
                }
                break;
            case 13:
                {
                    if(!keyUsed)
                    {
                        if (!keyTrigger.activeInHierarchy)
                        {
                            //door top
                            objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);
                            objInfWalls.SetTile(new Vector3Int(34, 17, 0), null);
                            objInfWalls.SetTile(new Vector3Int(35, 17, 0), null);

                            walls1.SetTile(new Vector3Int(34, 16, 0), null);
                            walls1.SetTile(new Vector3Int(33, 16, 0), null);
                            walls1.SetTile(new Vector3Int(32, 16, 0), null);
                        }
                    }
                }
                break;
            case 14:
                {
                    if (!keyTrigger.activeInHierarchy)
                    {
                        levelLiftTrigger.SetActive(true);
                    }
                }
                break;
            case 15:
                {
                    if(!bossDead)
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
                break;
            case 16:
                {
                    //if (!keyTrigger.activeInHierarchy)
                    //{
                    //    levelLiftTrigger.SetActive(true);
                    //}
                }
                break;
            case 18:
                {
                    if(!bossDead)
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
                break;
            case 19:
                {
                    if (!keyTrigger.activeInHierarchy)
                    {
                        walls1.SetTile(new Vector3Int(24, 10, 0), null);
                        walls1.SetTile(new Vector3Int(25, 10, 0), null);
                        walls1.SetTile(new Vector3Int(26, 10, 0), null);
                        walls2.SetTile(new Vector3Int(25, 11, 0), null);
                        walls2.SetTile(new Vector3Int(26, 11, 0), null);
                        walls2.SetTile(new Vector3Int(27, 11, 0), null);
                    }
                }
                break;
            case 20:
                {
                    if(!bossDead)
                    {
                        if (!levelBossIntro && !dialogBox.activeInHierarchy)
                        {
                            bossDialogue.TriggerDialogue(bossDialogIndex);
                            bossDialogIndex++;
                            if (bossDialogIndex == 14)
                                //cinematic
                                levelBossIntro = true;
                        }
                        if (boss.m_health <= 0 && !dialogBox.activeInHierarchy)
                        {
                            //if boss is ninjaroth change to Good Ending
                            if(timeline.playableDirector != timeline.timeline[1])
                                timeline.ChangeDirector("Bad Ending");
                            bossDialogue.TriggerDialogue(bossDialogIndex);
                            bossDialogIndex++;
                            if (bossDialogIndex == 19)
                            {
                                // just for timeline QA
                                timeline.ChangeDirector("Good Ending");
                                bossDialogue.TriggerDialogue(bossDialogIndex);
                                bossDialogIndex++;
                                if (bossDialogIndex == 27)
                                bossDead = true;
                                //dramatic death SE
                                //freeze on enemy as he dies  
                                //end cinematic
                                levelLiftTrigger.SetActive(true);
                            }

                        }
                    }
                }
                break;

            default:
                break;
        }
        //if (SceneManager.GetActiveScene().buildIndex == 1 && !keyUsed)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        Debug.Log("key worked");
        //        objInfWalls.SetTile(new Vector3Int(7, 14, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(6, 14, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(5, 14, 0), null);

        //        walls1.SetTile(new Vector3Int(4, 13, 0), null);
        //        walls1.SetTile(new Vector3Int(5, 13, 0), null);
        //        walls1.SetTile(new Vector3Int(6, 13, 0), null);

        //    }
            
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 2 && !enemyDead)
        //{
        //    if (level2Enemy1.m_health <= 0 || level2Enemy2.m_health <= 0)
        //    {
        //        levelLiftTrigger.SetActive(true);
        //        level2End.TriggerDialogue(0);
        //        enemyDead = true;

        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 3 && !bossDead)
        //{
        //    if (boss.m_health <= 0)
        //    {
        //        //dramatic death SE
        //        //freeze on enemy as he dies  
        //        bossDialogue.TriggerDialogue(0);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;
              
        //    }   
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 4 && !keyUsed)
        //{
        //   if(!keyTrigger.activeInHierarchy)
        //    {
        //        walls2.SetTile(new Vector3Int(26, 10, 0), null);
        //        walls2.SetTile(new Vector3Int(26, 11, 0), null);
        //        walls2.SetTile(new Vector3Int(26, 12, 0), null);
        //        walls1.SetTile(new Vector3Int(25, 9, 0), null);
        //        walls1.SetTile(new Vector3Int(25, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(25, 11, 0), null);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 5 && !keyUsed)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        levelLiftTrigger.SetActive(true);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 6 && !bossDead)
        //{
        //    if(!levelBossIntro)
        //    {
        //        bossDialogue.TriggerDialogue(0);
        //        levelBossIntro = true;
        //    }
        //    if (boss.m_health <= 0)
        //    {
        //        //dramatic death SE
        //        //freeze on enemy as he dies  
        //        bossDialogue.TriggerDialogue(1);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;

        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 8 && !keyUsed)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        walls2.SetTile(new Vector3Int(23, 28, 0), null);
        //        walls2.SetTile(new Vector3Int(23, 29, 0), null);
        //        walls2.SetTile(new Vector3Int(23, 30, 0), null);

        //        walls1.SetTile(new Vector3Int(22, 27, 0), null);
        //        walls1.SetTile(new Vector3Int(22, 28, 0), null);
        //        walls1.SetTile(new Vector3Int(22, 29, 0), null);

        //    }
        //    if (!keyTriggerTwo.activeInHierarchy)
        //    {
        //        walls2.SetTile(new Vector3Int(30, 10, 0), null);
        //        walls2.SetTile(new Vector3Int(30, 11, 0), null);
        //        walls2.SetTile(new Vector3Int(30, 12, 0), null);

        //        walls1.SetTile(new Vector3Int(29, 9, 0), null);
        //        walls1.SetTile(new Vector3Int(29, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(29, 11, 0), null);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 9 && !bossDead)
        //{
        //    if (!levelBossIntro)
        //    {
        //        bossDialogue.TriggerDialogue(0);
        //        levelBossIntro = true;
        //    }
        //    if (boss.m_health <= 0)
        //    {
        //        //dramatic death SE
        //        //freeze on enemy as he dies  
        //        bossDialogue.TriggerDialogue(1);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;

        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 11 && !keyUsed)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        //door top
        //        objInfWalls.SetTile(new Vector3Int(36, 32, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(37, 32, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(38, 32, 0), null);

        //        walls1.SetTile(new Vector3Int(35, 31, 0), null);
        //        walls1.SetTile(new Vector3Int(36, 31, 0), null);
        //        walls1.SetTile(new Vector3Int(37, 31, 0), null);

        //        //door mid-right
        //        objInfWalls.SetTile(new Vector3Int(33, 19, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(33, 18, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);

        //        walls1.SetTile(new Vector3Int(32, 18, 0), null);
        //        walls1.SetTile(new Vector3Int(32, 17, 0), null);
        //        walls1.SetTile(new Vector3Int(32, 16, 0), null);

        //        //door right
        //        objInfWalls.SetTile(new Vector3Int(35, 31, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(36, 31, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(37, 31, 0), null);

        //        walls1.SetTile(new Vector3Int(38, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(37, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(36, 10, 0), null);

        //        //door left
        //        objInfWalls.SetTile(new Vector3Int(11, 26, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(12, 26, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(13, 26, 0), null);

        //        walls1.SetTile(new Vector3Int(10, 25, 0), null);
        //        walls1.SetTile(new Vector3Int(11, 25, 0), null);
        //        walls1.SetTile(new Vector3Int(12, 25, 0), null);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 12 && !bossDead)
        //{
        //    if (!levelBossIntro)
        //    {
        //        bossDialogue.TriggerDialogue(0);
        //        levelBossIntro = true;
        //    }
        //    if (boss.m_health <= 0)
        //    {

        //        bossDialogue.TriggerDialogue(1);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;

        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 13 && !keyUsed)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        //door top
        //        objInfWalls.SetTile(new Vector3Int(33, 17, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(34, 17, 0), null);
        //        objInfWalls.SetTile(new Vector3Int(35, 17, 0), null);

        //        walls1.SetTile(new Vector3Int(34, 16, 0), null);
        //        walls1.SetTile(new Vector3Int(33, 16, 0), null);
        //        walls1.SetTile(new Vector3Int(32, 16, 0), null);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 14)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        levelLiftTrigger.SetActive(true);
        //    }

        //}
        //if (SceneManager.GetActiveScene().buildIndex == 15 && !bossDead)
        //{
        //    if (!levelBossIntro)
        //    {
        //        bossDialogue.TriggerDialogue(0);
        //        levelBossIntro = true;
        //    }
        //    if (boss.m_health <= 0)
        //    {
        //        //dramatic death SE
        //        //freeze on enemy as he dies  
        //        bossDialogue.TriggerDialogue(1);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 16)
        //{
        //    //If pressure pad activated then reopen the above doors (awaiting pressure pad prefab)
        //    /*
        //     * Below to go into the if function
        //    walls1.SetTile(new Vector3Int(12, 26, 0), null);
        //    walls1.SetTile(new Vector3Int(12, 25, 0), null);
        //    walls1.SetTile(new Vector3Int(12, 24, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 27, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 26, 0), null);
        //    walls2.SetTile(new Vector3Int(13, 25, 0), null);
        //    */
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 16)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        levelLiftTrigger.SetActive(true);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 18 && !bossDead)
        //{
        //    if (!levelBossIntro && !dialogBox.activeInHierarchy)
        //    {
        //        bossDialogue.TriggerDialogue(bossDialogIndex);
        //        bossDialogIndex++;
        //        if (bossDialogIndex == 8)
        //        {
        //            levelBossIntro = true;
        //        }
        //    }
        //    if (boss.m_health <= 0)
        //    {

        //        bossDialogue.TriggerDialogue(9);
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;

        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 19)
        //{
        //    if (!keyTrigger.activeInHierarchy)
        //    {
        //        walls1.SetTile(new Vector3Int(24, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(25, 10, 0), null);
        //        walls1.SetTile(new Vector3Int(26, 10, 0), null);
        //        walls2.SetTile(new Vector3Int(25, 11, 0), null);
        //        walls2.SetTile(new Vector3Int(26, 11, 0), null);
        //        walls2.SetTile(new Vector3Int(27, 11, 0), null);
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 20 && !bossDead)
        //{
        //    if (!levelBossIntro && !dialogBox.activeInHierarchy)
        //    {
        //        bossDialogue.TriggerDialogue(bossDialogIndex);
        //        bossDialogIndex++;
        //        if (bossDialogIndex == 14)
        //            //cinematic
        //            levelBossIntro = true;
        //    }
        //    if (boss.m_health <= 0)
        //    {
        //        //dramatic death SE
        //        //freeze on enemy as he dies  
        //        //end cinematic
        //        levelLiftTrigger.SetActive(true);
        //        bossDead = true;

        //    }
        //}
     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentLevel == 14)
        {
            //Mechanic for closing the doors behind the player on lvl 14
            if (other.gameObject == doorCloseTrigger && !doorsClosed)
            {
                walls1.SetTile(new Vector3Int(12, 26, 0), bottomDoorTile);
                walls1.SetTile(new Vector3Int(12, 25, 0), bottomDoorTile);
                walls1.SetTile(new Vector3Int(12, 24, 0), bottomDoorTile);
                walls2.SetTile(new Vector3Int(13, 27, 0), topDoorTile);
                walls2.SetTile(new Vector3Int(13, 26, 0), topDoorTile);
                walls2.SetTile(new Vector3Int(13, 25, 0), topDoorTile);
                doorsClosed = true;
            }
        }

    }
}
