//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented
//Jann  - 06/11/20 - Hooked up the savesystem and the checkpoints

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// IMPORTANT - Correct scene order must be setup in projects build settings scene index
/// </summary>
public class SceneManager_JamieG : MonoBehaviour
{
    private static SceneManager_JamieG m_instance;

    private GameObject m_player;
    private Vector3 checkpointOnReset;
    
    private void Awake()
    {
        // Singleton setup
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        } else {
            m_instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ResetLevel()
    {
        LoadCurrentLevel();
    }
    
    public void ResetToCheckpoint()
    {
        GameplayData gameplayData = SaveLoadSystem_JamieG.LoadGameplay();
        checkpointOnReset = new Vector3(
            gameplayData.m_checkpointPosition[0],
            gameplayData.m_checkpointPosition[1],
            gameplayData.m_checkpointPosition[2]);
        
        LoadCurrentLevel();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This will assume that the MainMenu scene is build index 0
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); 
    }

    //Assumes that the scenes are in the correct order of build indexes in build settings
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Reloads the current scene using its buildIndex
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_player == null)
        {
            m_player = GameObject.FindWithTag("Player");
        }

        m_player.transform.position = checkpointOnReset;
    }

    public static SceneManager_JamieG Instance => m_instance;
}
