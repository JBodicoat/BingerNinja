//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented
///IMPORTANT - Correct scene order must be setup in projects build settings scene index
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManager_JamieG 
{
    //This will assume that the MainMenu scene is build index 0
    public static void LoadMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //SceneManager.LoadScene(0); // - Commented out for the moment until the the build settings index order is correctly setup
        }
    }

    //Assumes that the scenes are in the correct order of build indexes in build settings
    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Reloads the current scene using its buildIndex
    public static void LoadCurrentLevel(int curCheckpoint)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
