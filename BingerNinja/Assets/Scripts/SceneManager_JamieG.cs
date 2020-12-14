//Jamie - Functions used to load different scenes

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
    private GameObject m_player;
    private Vector3 m_checkpointOnReset;
    private bool m_loadLastCheckpoint = false;

    private DialogueManager_MarioFernandes m_dialogueManager;
    
    public float m_fadeTime = 0.8f;
    private RectTransform m_fader;
    private bool m_fading;
    private Vector2 center, below = new Vector2(0f, 1000f);

    public void Awake()
    {
        base.Awake();

        m_fader = GameObject.Find("Fader")?.GetComponent<RectTransform>();
        
        GameObject dialogManager = GameObject.Find("DialogManager");
        if (dialogManager != null)
        {
            m_dialogueManager = dialogManager.GetComponent<DialogueManager_MarioFernandes>();
        }
    }

    private void Start()
    {
        FadeOut();
    }

    public void ResetLevel()
    {
        LoadCurrentLevel();
    }

    public void ResetToCheckpoint()
    {
        int checkpointLevel = SaveLoadSystem_JamieG.LoadCheckpoint().m_lastCheckpointLevel;
        StartCoroutine(Load(checkpointLevel > 0 ? checkpointLevel : 1));
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
        FadeIn();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Reloads the current scene using its buildIndex
    private void LoadCurrentLevel()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator Load(int level)
    {
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(level);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called everytime a scene is loaded
    //It moves the player to the last checkpoint if m_loadLastCheckpoint is true
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!m_loadLastCheckpoint)
        {
            return;
        }
        
        LoadGameState();
        
        if (m_player == null)
        {
            m_player = GameObject.FindWithTag("Player");
        }

        m_player.transform.position = m_checkpointOnReset;

        m_loadLastCheckpoint = false;
    }

    public void SaveGameState()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Inventory_JoaoBeijinho inventory = player.GetComponent<Inventory_JoaoBeijinho>();
            SaveLoadSystem_JamieG.SaveInventory(inventory);
            
            SaveLoadSystem_JamieG.SaveGameplay(
                SceneManager.GetActiveScene().buildIndex,
                GameObject.FindGameObjectsWithTag("Enemy"),
                new GameObject[0]//GameObject.FindGameObjectsWithTag("Door")
            );
        }
    }
    
    public void LoadGameState()
    {
        GameplayData gameplayData = SaveLoadSystem_JamieG.LoadGameplay();
        
        // Load enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (!gameplayData.m_enemyIds.Contains(enemy.name))
            {
                enemy.SetActive(false);
            }
        }
    }

    public void FadeBoth()
    {
        m_dialogueManager.PauseGame();
        
        float fadeDelta = m_fadeTime / 2f;
        StartCoroutine(FadeImage(fadeDelta, false));
        StartCoroutine(FadeImage(fadeDelta, true, fadeDelta));
    }
    
    public void FadeIn()
    {
        StartCoroutine(FadeImage(m_fadeTime / 2f, false));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(m_fadeTime / 2f, true));
    }


    IEnumerator FadeImage(float fadeTime, bool fadeAway, float delay = 0f)
    {
        if (m_fader == null)
            yield break;

        if (delay > 0)
            yield return new WaitForSeconds(delay);
        
        m_fading = true;
        
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            float normalizedTime = i / fadeTime;

            if (fadeAway)
            {
                m_fader.anchoredPosition = Vector2.Lerp(center, -below, normalizedTime);
            }
            else
            {
                m_fader.anchoredPosition = Vector2.Lerp(below, center, normalizedTime);
            }
            
            yield return null;
        }

        if (!m_dialogueManager.m_dialogBox.activeInHierarchy)
        {
            m_dialogueManager.ResumeGame();   
        }
    }
}
