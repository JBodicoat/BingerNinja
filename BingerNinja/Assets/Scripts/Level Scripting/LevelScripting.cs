/*
 * Chase Wilding - 8/11/2020
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelScripting : MonoBehaviour
{
    internal bool Q = false, W = false, E, R = false, T = false, Y = true, U = false, I = false, O = false;
     GameObject a, b, c, d, e, f, g;
    public GameObject P;
     BaseEnemy_SebastianMol h, i, j, k;
     BossDialogue_MarioFernandes l, m, n;
     Tilemap o, p, q, r;
    public Tile A, S;
     int s = 0, t = 0;
    public int D;
     PlayerController_JamieG u;
    // Dan timeline script
     Timeline_Script w;
     float x;

     WeaponUI_LouieWilliamson y;

     void Awake()
    {
        D = SceneManager.GetActiveScene().buildIndex;
        u = GameObject.Find("Player").GetComponent<PlayerController_JamieG>();
        switch (D)
        {
            case 1:
                {
                    b = GameObject.Find("Key Trigger");
                    p = GameObject.Find("Walls1_Map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_Map").GetComponent<Tilemap>();
                }
                break;
            case 2:
                {
                    n = GameObject.Find("EndLevelTwo").GetComponent<BossDialogue_MarioFernandes>();
                    j = GameObject.Find("Enemy 1").GetComponent<BaseEnemy_SebastianMol>();
                    k = GameObject.Find("Enemy 2").GetComponent<BaseEnemy_SebastianMol>();
                    a = GameObject.Find("Level 2 Lift");
                }
                break;
            case 3:
                {
                    h = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 3 Lift");
                }
                break;
            case 4:
                {
                    b = GameObject.Find("Key Trigger");
                    a = GameObject.Find("Level 7 Lift");
                }
                break;
            case 5:
                {
                    b = GameObject.Find("Key Trigger");
                    c = GameObject.Find("Key Trigger 2");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    o = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
                    q = GameObject.Find("ObjectsBehindWalls_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 6:
                {
                    h = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 9 Lift");
                }
                break;
            case 7:
                {
                    b = GameObject.Find("Key Trigger");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 8:
                {
                    h = GameObject.Find("Yakuza Leader").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Yakuza Leader").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 12 Lift");
                }
                break;
            case 9:
                {
                    a = GameObject.Find("Level 17 Lift");
                    b = GameObject.Find("Key");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 10:
                {
                    h = GameObject.Find("Ninjaroth").GetComponent<BaseEnemy_SebastianMol>();
                    i = GameObject.Find("Tadashi").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Ninjaroth").GetComponent<BossDialogue_MarioFernandes>();
                    m = GameObject.Find("Tadashi").GetComponent<BossDialogue_MarioFernandes>();
                    e = GameObject.Find("DialogBox");
                    a = GameObject.Find("Level 20 Lift");
                    // find game object with timeline script to change playable director
                    w = GameObject.Find("Cinematics").GetComponent<Timeline_Script>();
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

     void Start()
    {
        switch (D)
        {

            case 2:
                {
                    a.SetActive(false);
                }
                break;
            case 3:
                {
                    a.SetActive(false);
                }
                break;
            case 7:
                {
                    a.SetActive(false);
                }
                break;
            case 9:
                {
                    a.SetActive(false);
                }
                break;
            case 12:
                {
                    a.SetActive(false);
                }
                break;
            case 20:
                a.SetActive(false);
                break;
            default:
                break;
        }
        y = GameObject.Find("WeaponsUI").GetComponent<WeaponUI_LouieWilliamson>();

    }
     void Update()
    {
        if (u.m_changeLevel.triggered)
        {
            SceneManager_JamieG.Instance.F();
        }
        switch (D)
        {
            case 1:
                {
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            y.setKey(false);

                            r.SetTile(new Vector3Int(6, 13, 0), null);
                            r.SetTile(new Vector3Int(5, 13, 0), null);
                            r.SetTile(new Vector3Int(4, 13, 0), null);

                            p.SetTile(new Vector3Int(3, 12, 0), null);
                            p.SetTile(new Vector3Int(4, 12, 0), null);
                            p.SetTile(new Vector3Int(5, 12, 0), null);


                            W = true;
                            

                        }
                    }
                }
                break;
            case 2:
                {
                    if(!Q)
                    {
                        if (j.CO <= 0 || k.CO <= 0)
                        {
                            a.SetActive(true);
                            n.A(0);
                            Q = true;

                        }
                    }
                }
                break;
            case 3:
                {
                    if(!R)
                    {
                        if (!E)
                        {
                           // cinematics.PlayZoomIn();
                            l.A(0);
                            E = true;
                        }
                        if (h.CO <= 0)
                        {
                            //dramatic death SE
                            //freeze on enemy as he dies  
                            l.A(1);
                            a.SetActive(true);
                            R = true;

                        }
                    }
                }
                break;
            case 4:
                {
                    if (!b.activeInHierarchy && !W)
                    {
                        a.SetActive(true);
                        W = true;
                    }
                }
                break;
            case 5:
                {
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            r.SetTile(new Vector3Int(22, 28, 0), null);
                            r.SetTile(new Vector3Int(22, 29, 0), null);
                            r.SetTile(new Vector3Int(22, 27, 0), null);

                            p.SetTile(new Vector3Int(21, 27, 0), null);
                            p.SetTile(new Vector3Int(21, 28, 0), null);
                            p.SetTile(new Vector3Int(21, 26, 0), null);

                        }
                        if (!c.activeInHierarchy)
                        {
                            r.SetTile(new Vector3Int(29, 10, 0), null);
                            r.SetTile(new Vector3Int(29, 11, 0), null);
                            r.SetTile(new Vector3Int(29, 9, 0), null);

                            p.SetTile(new Vector3Int(28, 9, 0), null);
                            p.SetTile(new Vector3Int(28, 10, 0), null);
                            p.SetTile(new Vector3Int(28, 8, 0), null);
                        }
                    }
                }
                break;
            case 6:
                {
                    if(!R)
                    {
                        if (!E)
                        {
                            l.A(0);
                            E = true;
                        }
                        if (h.CO <= 0)
                        {
                            //dramatic death SE
                            //freeze on enemy as he dies  
                            l.A(1);
                            a.SetActive(true);
                            R = true;

                        }
                    }
                }
                break;
            case 7:
                {
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            y.setKey(false);
                            W = true;
                            //door top
                            r.SetTile(new Vector3Int(36, 32, 0), null);
                            r.SetTile(new Vector3Int(37, 32, 0), null);
                            r.SetTile(new Vector3Int(38, 32, 0), null);

                            p.SetTile(new Vector3Int(35, 31, 0), null);
                            p.SetTile(new Vector3Int(36, 31, 0), null);
                            p.SetTile(new Vector3Int(37, 31, 0), null);

                            //door mid-right
                            r.SetTile(new Vector3Int(33, 19, 0), null);
                            r.SetTile(new Vector3Int(33, 18, 0), null);
                            r.SetTile(new Vector3Int(33, 17, 0), null);

                            p.SetTile(new Vector3Int(32, 18, 0), null);
                            p.SetTile(new Vector3Int(32, 17, 0), null);
                            p.SetTile(new Vector3Int(32, 16, 0), null);

                            //door right
                            r.SetTile(new Vector3Int(35, 31, 0), null);
                            r.SetTile(new Vector3Int(36, 31, 0), null);
                            r.SetTile(new Vector3Int(37, 31, 0), null);

                            p.SetTile(new Vector3Int(38, 10, 0), null);
                            p.SetTile(new Vector3Int(37, 10, 0), null);
                            p.SetTile(new Vector3Int(36, 10, 0), null);

                            //door left
                            r.SetTile(new Vector3Int(11, 26, 0), null);
                            r.SetTile(new Vector3Int(12, 26, 0), null);
                            r.SetTile(new Vector3Int(13, 26, 0), null);

                            p.SetTile(new Vector3Int(10, 25, 0), null);
                            p.SetTile(new Vector3Int(11, 25, 0), null);
                            p.SetTile(new Vector3Int(12, 25, 0), null);
                        }
                    }
                }
                break;
            case 8:
                {
                    if(!R)
                    {
                        if (!E)
                        {
                            l.A(0);
                            E = true;

                            
                        }
                        if (h.CO <= 0)
                        {
                            l.A(1);
                            a.SetActive(true);
                            R = true;

                        }
                    }
                }
                break;
                case 10:
                {
                    if(!R)
                    {
                        if (!E && !e.activeInHierarchy)
                        {
                            l.A(s);
                            s++;
                            if (s == 14)
                                //cinematic
                                E = true;
                        }
                        if (h.CO <= 0 && !e.activeInHierarchy)
                        {
                            //if boss is ninjaroth change to Good Ending
                            if(w.playableDirector != w.timeline[1])
                                w.ChangeDirector("Bad Ending");
                            l.A(s);
                            s++;
                            a.SetActive(true);
                            if(s == 19)
                            {
                                R = true;
                            }
                            

                        }
                        else if (i != null)
                            if (i.CO <= 0 && !e.activeInHierarchy)
                            {
                                // just for timeline QA
                                w.ChangeDirector("Good Ending");
                                m.A(t);
                                t++;
                                if (t == 8)
                                R = true;
                                //dramatic death SE
                                //freeze on enemy as he dies  
                                //end cinematic
                                a.SetActive(true);
                            }
                    }
                }
                break;

            default:
                break;
        }
    }
}
