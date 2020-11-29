using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class levelScriptingMorgan : MonoBehaviour
{ 
 #region VARIABLES
    bool enemyDead = false, keyUsed = false, levelBossIntro, bossDead = false;
    GameObject levelLiftTrigger, keyTrigger, keyTriggerTwo;
    BaseEnemy_SebastianMol boss, level2Enemy1, level2Enemy2;
    BossDialogue_MarioFernandes bossDialogue, level2End;
    Tilemap objInfWalls, objWalls, objBehWalls, walls2;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            keyTrigger = GameObject.Find("Key Trigger");
            objWalls = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
            objInfWalls = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 15)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
            bossDialogue = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
            levelLiftTrigger = GameObject.Find("Level 15 Lift");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
