using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_JamieG : MonoBehaviour
{
    void Start()
    {
        //Load MainMenu on project startup
        LoadMainMenu();
    }

    //This will assume that the MainMenu scene is build index 0
    public void LoadMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //SceneManager.LoadScene(0); // - Commented out for the moment until the the build settings index order is correctly setup
        }

    }

    //Assumes that the scenes are in the correct order of build indexes in build settings
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Reloads the current scene using its buildIndex
    public void LoadCurrentLevel(int curCheckpoint)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
