//Jamie - Functions used to load different scenes

//Jamie - 26/10/20 - First implemented

///IMPORTANT - Correct scene order must be setup in projects build settings scene index

using UnityEngine.SceneManagement;

public static class SceneManager_JamieG 
{
    //This will assume that the MainMenu scene is build index 0
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0); 
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
