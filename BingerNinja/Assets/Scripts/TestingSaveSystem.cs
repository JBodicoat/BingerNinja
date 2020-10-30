using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSaveSystem : MonoBehaviour
{
    // Testing for scene manager
    public bool loadMainMenu = false;
    public bool loadNextLevel = false;
    public bool loadCurrentLevel = false;


    // Testing for save system
    public bool saveSettings = true;
    public bool saveUpgrades = false;
    public bool saveCheckpointNum = false;

    // Update is called once per frame
    void Update()
    {
    // Testing for scene manager
        if (loadMainMenu)
        {
            loadMainMenu = false;
            SceneManager_JamieG.LoadMainMenu();
        }
        else if(loadNextLevel)
        {
            loadNextLevel = false;
            SceneManager_JamieG.LoadNextLevel();
		}
        else if(loadCurrentLevel)
        {
            loadCurrentLevel = false;
            SceneManager_JamieG.LoadCurrentLevel(1);
		}

        // Testing for save system
        if (saveSettings)
        {
            saveSettings = false;
            SaveLoadSystem.SaveSettings();
            Debug.Log(Application.persistentDataPath);
        }
        else if (saveUpgrades)
        {
            saveUpgrades = false;
            SaveLoadSystem.SaveUpgrades();
        }
        else if(saveCheckpointNum)
        {
            saveCheckpointNum = false;
            SaveLoadSystem.SaveCheckpointNum();
		}
    }
}
