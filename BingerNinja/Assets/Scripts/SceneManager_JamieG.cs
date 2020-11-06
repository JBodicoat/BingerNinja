//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented
//Jann  - 06/11/20 - Hooked up the savesystem and the checkpoints

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// IMPORTANT - Correct scene order must be setup in projects build settings scene index
/// </summary>
public class SceneManager_JamieG : MonoBehaviour
{
    private static SceneManager_JamieG m_instance;

    private GameObject m_player;
    
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
        LoadCurrentLevel();

        if (m_player != null)
        {
            SaveSystem_ElliottDesouza checkpoint = GameObject.FindGameObjectWithTag("SaveCheckpoint")
                .GetComponent<SaveSystem_ElliottDesouza>();

            if (checkpoint != null)
            {
                m_player.transform.position = checkpoint.m_currentCheckpoint.position;
            }
            else
            {
                GameplayData gameplayData = SaveLoadSystem_JamieG.LoadGameplay();
                m_player.transform.position = new Vector3(
                    gameplayData.m_checkpointPosition[0],
                    gameplayData.m_checkpointPosition[1],
                    gameplayData.m_checkpointPosition[2]);
            }
        }
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

        if (!m_player)
        {
            m_player = GameObject.FindWithTag("Player");
        }
    }

    //Reloads the current scene using its buildIndex
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static SceneManager_JamieG Instance => m_instance;
}
