﻿//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented
// Jann - 06/11/20 - Hooked up the savesystem and the checkpoints
// Jann - 14/12/20 - New ResetToCheckpoint implementation

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// IMPORTANT - Correct scene order must be setup in projects build settings scene index
/// </summary>
public class SceneManager_JamieG : Singleton_Jann<SceneManager_JamieG>
{
     GameObject a;
     Vector3 b;
     bool c = false;

     DialogueManager_MarioFernandes g;
    
    public float m_fadeTime = 0.8f, m_portalFadeTime = 0.8f, m_transitionTime = 1.0f;
     RectTransform e;
     bool f;
     Vector2 y, u = new Vector2(0f, 1000f);

    public void Awake()
    {
        base.Awake();

        e = GameObject.Find("Fader")?.GetComponent<RectTransform>();
        
        GameObject s = GameObject.Find("DialogManager");
        if (s != null)
        {
            g = s.GetComponent<DialogueManager_MarioFernandes>();
        }
    }

     void Start()
    {
        FadeOut();
    }

    public void ResetLevel()
    {
        LoadCurrentLevel();
    }

    public void ResetToCheckpoint()
    {
        int x = SaveLoadSystem_JamieG.LoadCheckpoint().m_lastCheckpointLevel;
        StartCoroutine(Load(x > 0 ? x : 1));
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(Load(level));
    }
    
    //This will assume that the MainMenu scene is build index 0
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); 
    }

    //Assumes that the scenes are in the correct order of build indexes in build settings
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Inventory_JoaoBeijinho y = GameObject.Find("Player").GetComponent<Inventory_JoaoBeijinho>();
            if(y.HasItem(ItemType.LiftKey, y.m_inventoryItems[ItemType.LiftKey]))
            {
                y.RemoveItem(ItemType.LiftKey, y.m_inventoryItems[ItemType.LiftKey]);
            }
           
            SaveLoadSystem_JamieG.SaveInventory(y);
        }

        FadeIn();

        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //Reloads the current scene using its buildIndex
     void LoadCurrentLevel()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator Load(int level)
    {
        yield return new WaitForSeconds(m_transitionTime);
        
        SceneManager.LoadScene(level);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called everytime a scene is loaded
    //It moves the player to the last checkpoint if m_loadLastCheckpoint is true
     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!c)
        {
            return;
        }
        
        LoadGameState();
        LoadSoundSettings();
        
        if (a == null)
        {
            a = GameObject.FindWithTag("Player");
        }

        a.transform.position = b;

        c = false;
    }

    public void SaveGameState()
    {
        GameObject z = GameObject.Find("Player");
        if (z != null)
        {
            Inventory_JoaoBeijinho j = z.GetComponent<Inventory_JoaoBeijinho>();
            SaveLoadSystem_JamieG.SaveInventory(j);
            
            SaveLoadSystem_JamieG.SaveGameplay(
                SceneManager.GetActiveScene().buildIndex,
                GameObject.FindGameObjectsWithTag("Enemy"),
                new GameObject[0]//GameObject.FindGameObjectsWithTag("Door")
            );
        }
    }
    
    public void LoadGameState()
    {
        GameplayData k = SaveLoadSystem_JamieG.LoadGameplay();
        
        // Load enemies
        GameObject[] l = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in l)
        {
            if (!k.m_enemyIds.Contains(enemy.name))
            {
                enemy.SetActive(false);
            }
        }
    }

    public void LoadSoundSettings()
    {
        SettingsData m = SaveLoadSystem_JamieG.LoadSettings();

        if (!m.Equals(default(SettingsData)))
        {
            PlayTrack_Jann.Instance.UpdateMusicVolume(m.m_musicVolume);
            PlayTrack_Jann.Instance.UpdateSfxVolume(m.m_sfxVolume);
        }
    }

    public void FadeBoth()
    {
        g.PauseGame();
        
        float n = m_portalFadeTime / 2f;
        StartCoroutine(FadeImage(n, false));
        StartCoroutine(FadeImage(n, true, n));
    }
    
    public void FadeIn()
    {
        StartCoroutine(FadeImage(m_fadeTime / 2f, false));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(m_fadeTime / 2f, true));
    }

    IEnumerator FadeImage(float p, bool q, float r = 0f)
    {
        if (e == null)
            yield break;

        if (r > 0)
            yield return new WaitForSeconds(r);
        
        f = true;
        
        for (float i = 0; i <= p; i += Time.deltaTime)
        {
            float o = i / p;

            if (q)
            {
                e.anchoredPosition = Vector2.Lerp(y, -u, o);
            }
            else
            {
                e.anchoredPosition = Vector2.Lerp(u, y, o);
            }
            
            yield return null;
        }

        if (!g.m_dialogBox.activeInHierarchy)
        {
            g.ResumeGame();   
        }
    }
}
