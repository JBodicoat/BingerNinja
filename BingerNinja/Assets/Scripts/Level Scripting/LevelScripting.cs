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
     BossIntroCineScript_AdamG v;
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
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    o = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 5:
                {
                    b = GameObject.Find("Key Trigger");
                    a = GameObject.Find("Level 5 Lift");
                    f = GameObject.Find("Vents");
                    g = GameObject.Find("Exit Vent");
                }
                break;
            case 6:
                {
                    h = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 6 Lift");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 7:
                {
                    b = GameObject.Find("Key Trigger");
                    a = GameObject.Find("Level 7 Lift");
                }
                break;
            case 8:
                {
                    b = GameObject.Find("Key Trigger");
                    c = GameObject.Find("Key Trigger 2");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    o = GameObject.Find("ObjectsInFrontOfWalls_map").GetComponent<Tilemap>();
                    q = GameObject.Find("ObjectsBehindWalls_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 9:
                {
                    h = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 9 Lift");
                }
                break;
            case 11:
                {
                    b = GameObject.Find("Key Trigger");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 12:
                {
                    h = GameObject.Find("Yakuza Leader").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Yakuza Leader").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 12 Lift");
                    v = GameObject.Find("BossIntroCinematic").GetComponent<BossIntroCineScript_AdamG>();
                }
                break;
            case 13:
                {
                    b = GameObject.Find("Key Trigger");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 14:
                {
                    a = GameObject.Find("Level 14 Lift");
                    b = GameObject.Find("Real Key");
                    d = GameObject.Find("DialogTrigger");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                    A = p.GetTile<Tile>(new Vector3Int(12, 26, 0));
                    S = r.GetTile<Tile>(new Vector3Int(13, 27, 0));
                }
                break;
            case 15:
                {
                    h = GameObject.Find("Toby the Tiger").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Toby the Tiger").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 15 Lift");
                }
                break;
            case 17:
                {
                    a = GameObject.Find("Level 17 Lift");
                    b = GameObject.Find("Key");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 18:
                {
                    h = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 18 Lift");
                    e = GameObject.Find("DialogBox");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 19:
                {
                    a = GameObject.Find("Level 19 Lift");
                    b = GameObject.Find("Key Trigger");
                    // keyTrigger = GameObject.Find("My Key");
                    p = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
                    r = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
                }
                break;
            case 20:
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
            case 5:
                {
                    a.SetActive(false);
                    g.SetActive(false);
                }
                break;
            case 6:
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
            case 14:
                {
                    a.SetActive(false);
                    p.SetTile(new Vector3Int(12, 26, 0), null);
                    p.SetTile(new Vector3Int(12, 25, 0), null);
                    p.SetTile(new Vector3Int(12, 24, 0), null);
                    r.SetTile(new Vector3Int(13, 27, 0), null);
                    r.SetTile(new Vector3Int(13, 26, 0), null);
                    r.SetTile(new Vector3Int(13, 25, 0), null);
                }
                break;
            case 15:
                {
                    a.SetActive(false);
                }
                break;
            case 18:
                {
                    h = GameObject.Find("Space Ninja").GetComponent<BaseEnemy_SebastianMol>();
                    l = GameObject.Find("Space Ninja").GetComponent<BossDialogue_MarioFernandes>();
                    a = GameObject.Find("Level 18 Lift");
                    e = GameObject.Find("DialogBox");
                    E = false;
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
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            y.setKey(false);
                            W = true;
                            r.SetTile(new Vector3Int(25, 11, 0), null);
                            r.SetTile(new Vector3Int(25, 10, 0), null);
                            r.SetTile(new Vector3Int(25, 9, 0), null);
                            p.SetTile(new Vector3Int(24, 9, 0), null);
                            p.SetTile(new Vector3Int(24, 10, 0), null);
                            p.SetTile(new Vector3Int(24, 8, 0), null);
                        }
                    }


                }
                break;
            case 5:
                {
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            y.setKey(false);
                            W = true;
                            a.SetActive(true);
                        }
                    }
                    if(!P.activeInHierarchy && !g.activeInHierarchy)
                    {
                        g.SetActive(true);
                    }
                    //if(endVentDialogueCanPlay)
                    //{
                    //    endVentDialogue.SetActive(true);
                    //    endVentDialogueCanPlay = false;
                    //}
                   
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
                   if(!O)
                   {
                        r.SetTile(new Vector3Int(23, 10, 0), null);
                        r.SetTile(new Vector3Int(23, 9, 0), null);
                        r.SetTile(new Vector3Int(23,8, 0), null);

                        r.SetTile(new Vector3Int(22, 7, 0), null);
                        r.SetTile(new Vector3Int(22, 8, 0), null);
                        r.SetTile(new Vector3Int(22, 9, 0), null);
                    }
                    else if (O)
                    {
                        r.SetTile(new Vector3Int(23, 10, 0),S);
                        r.SetTile(new Vector3Int(23, 9, 0), S);
                        r.SetTile(new Vector3Int(23, 8, 0), S);

                        r.SetTile(new Vector3Int(22, 7, 0), A);
                        r.SetTile(new Vector3Int(22, 8, 0), A);
                        r.SetTile(new Vector3Int(22, 9, 0), A);
                    }
                }
                break;

            case 7:
                {
                    if (!b.activeInHierarchy && !W)
                    {
                        a.SetActive(true);
                        W = true;
                    }
                }
                break;
            case 8:
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
            case 9:
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
            case 11:
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
            case 12:
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
            case 13:
                {
                    if(!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            y.setKey(false);
                            
                            //door top
                            r.SetTile(new Vector3Int(33, 17, 0), null);
                            r.SetTile(new Vector3Int(34, 17, 0), null);
                            r.SetTile(new Vector3Int(35, 17, 0), null);

                            p.SetTile(new Vector3Int(34, 16, 0), null);
                            p.SetTile(new Vector3Int(33, 16, 0), null);
                            p.SetTile(new Vector3Int(32, 16, 0), null);
                            W = true;
                        }
                    }
                }
                break;
            case 14:
                {
                    if (!b.activeInHierarchy)
                    {
                        a.SetActive(true);
                        y.setKey(false);
                    }
                }
                break;
            case 15:
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
                    if(!R)
                    {
                        if (!E && !e.activeInHierarchy)
                        {
                            l.A(s);
                            s++;
                            if (s == 8)
                            {
                                E = true;
                            }
                        }
                        if (h.CO <= 0)
                        {

                            l.A(9);
                            a.SetActive(true);
                            R = true;

                        }
                    }
                }
                break;
            case 19:
                {
                    if (!W)
                    {
                        if (!b.activeInHierarchy)
                        {
                            p.SetTile(new Vector3Int(24, 9, 0), null);
                            p.SetTile(new Vector3Int(25, 9, 0), null);
                            p.SetTile(new Vector3Int(23, 9, 0), null);
                            r.SetTile(new Vector3Int(25, 10, 0), null);
                            r.SetTile(new Vector3Int(26, 10, 0), null);
                            r.SetTile(new Vector3Int(24, 10, 0), null);
                            y.setKey(false);
                            W = true;
                        }
                    }   
                }
                break;
            case 20:
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
     void OnTriggerEnter2D(Collider2D z)
    {
        if (D == 14)
        {
            //Mechanic for closing the doors behind the player on lvl 14
            if (z.gameObject == d && !T)
            {
               
                r.SetTile(new Vector3Int(13, 27, 0), S);
                r.SetTile(new Vector3Int(13, 26, 0), S);
                r.SetTile(new Vector3Int(13, 25, 0), S);
                p.SetTile(new Vector3Int(12, 26, 0), A);
                p.SetTile(new Vector3Int(12, 25, 0), A);
                p.SetTile(new Vector3Int(12, 24, 0), A);
                T = true;
            }
        }

    }
}
