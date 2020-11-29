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
    Tile bottomDoorTile, topDoorTile;

    
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
            bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12,26,0));
            walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
            bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12, 26, 0));
            topDoorTile = walls1.GetTile<Tile>(new Vector3Int(13, 27, 0));
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
        if(SceneManager.GetActiveScene().buildIndex == 16)
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
