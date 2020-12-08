//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented
//Jann  - 06/11/20 - Hooked up the savesystem and the checkpoints

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

    public void ResetLevel()
    {
        LoadCurrentLevel();
    }

    public void ResetToCheckpoint()
    {
        GameplayData gameplayData = SaveLoadSystem_JamieG.LoadGameplay();
        m_checkpointOnReset = new Vector3(
            gameplayData.m_checkpointPosition[0],
            gameplayData.m_checkpointPosition[1],
            gameplayData.m_checkpointPosition[2]);

        m_loadLastCheckpoint = true;
        
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
    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //This is called everytime a scene is loaded
    //It moves the player to the last checkpoint if m_loadLastCheckpoint is true
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!m_loadLastCheckpoint)
        {
            return;
        }
        
        if (m_player == null)
        {
            m_player = GameObject.FindWithTag("Player");
        }

        m_player.transform.position = m_checkpointOnReset;

        m_loadLastCheckpoint = false;
    }
}
