using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelScripting_JW : MonoBehaviour
{
    GameObject levelLiftTrigger, keyTrigger, doorCloseTrigger, dialogBox, enemyToKill;
    BossDialogue_MarioFernandes bossDialogue;
    BaseEnemy_SebastianMol boss;
    bool levelBossIntro = false, bossDead = false, doorsClosed = false;
    Tilemap walls1, walls2;
    Tile bottomDoorTile, topDoorTile;
    int bossDialogIndex = 0;

    
    private void Awake()
    {
        walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();

        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            boss = GameObject.Find("Yakuza Boss").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.Find("Yakuza Boss").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 12 Lift");
        }
        if(SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelLiftTrigger = GameObject.Find("Level 14 Lift");
            keyTrigger = GameObject.Find("Help");
            doorCloseTrigger = GameObject.Find("DialogTrigger");
            bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12,26,0));
            walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
            bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12, 26, 0));
            topDoorTile = walls2.GetTile<Tile>(new Vector3Int(13, 27, 0));
        }
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            levelLiftTrigger = GameObject.Find("Level 14 Lift");
            enemyToKill = GameObject.Find("AlienEnemy");
        }
        if (SceneManager.GetActiveScene().buildIndex == 18)
        {
            boss = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 18 Lift");
            dialogBox = GameObject.Find("DialogBox");
        }
        if (SceneManager.GetActiveScene().buildIndex == 19)
        {
            levelLiftTrigger = GameObject.Find("Level 19 Lift");
            keyTrigger = GameObject.Find("Key Trigger");
           // keyTrigger = GameObject.Find("My Key");
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
            walls1.SetTile(new Vector3Int(12, 26, 0), null);
            walls1.SetTile(new Vector3Int(12, 25, 0), null);
            walls1.SetTile(new Vector3Int(12, 24, 0), null);
            walls2.SetTile(new Vector3Int(13, 27, 0), null);
            walls2.SetTile(new Vector3Int(13, 26, 0), null);
            walls2.SetTile(new Vector3Int(13, 25, 0), null);
        }
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            levelLiftTrigger.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 18)
        {
            levelBossIntro = false;
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
            if (!keyTrigger.activeInHierarchy)
            {
                levelLiftTrigger.SetActive(true);
            }
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
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            if(!enemyToKill.activeInHierarchy)
            {
                levelLiftTrigger.SetActive(true);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 18 && !bossDead)
        {
            if (!levelBossIntro && !dialogBox.activeInHierarchy)
            {
                bossDialogue.TriggerDialogue(bossDialogIndex);
                bossDialogIndex++;
                if(bossDialogIndex == 8)
                {
                    levelBossIntro = true;
                }
            }
            if (boss.m_health <= 0)
            {
                Debug.Log("DEAD");
                bossDialogue.TriggerDialogue(9);
                levelLiftTrigger.SetActive(true);
                bossDead = true;

            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 19)
        {
            if(!keyTrigger.activeInHierarchy)
            {
                walls1.SetTile(new Vector3Int(24,10,0), null);
                walls1.SetTile(new Vector3Int(25, 10, 0), null);
                walls1.SetTile(new Vector3Int(26, 10, 0), null);
                walls2.SetTile(new Vector3Int(25, 11, 0), null);
                walls2.SetTile(new Vector3Int(26, 11, 0), null);
                walls2.SetTile(new Vector3Int(27, 11, 0), null);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SceneManager.GetActiveScene().buildIndex == 14)
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
