//Elliott Desouza

//Elliott 26/10/2020 - can open up settings menu and quit game
// Jack 02/11/2020 - set StartNewGame() to open scene of build index 1 for VS
//                   moved class header comment to the correct place and reformated.
//                   linked this scripts functions with the MainMenu scene buttons

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// class holds values for mainmenu UI
/// </summary>
public class MainMenu_Manager_Elliott : MonoBehaviour
{
    public Button m_continueButton;
    public GameObject m_settingMenu;
    protected bool m_openSettings;
    private int m_lastCheckpointLevel;

    public void Resume()
    {
        if (m_lastCheckpointLevel > 0)
        {
            SceneManager_JamieG.Instance.LoadLevel(m_lastCheckpointLevel);
        }
    }

    public void StartNewGame()
    {
        ///load level 1 here
        SaveLoadSystem_JamieG.DeleteSaves();
        SceneManager_JamieG.Instance.LoadNextLevel();
        print("start");
    }

    public void OpenSettingsMenu()
    {
        ///open up settings
        Debug.Log("open Settings");
        m_settingMenu.SetActive(true);
    }

    public void Quit()
    {
        ///quit application
        Debug.Log("quit");
        Application.Quit();
    }
  
    void Start()
    {
        m_openSettings = false;

        m_lastCheckpointLevel = SaveLoadSystem_JamieG.LoadCheckpoint().m_lastCheckpointLevel;
        m_continueButton.enabled = m_lastCheckpointLevel > 0;
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Keyboard.current;
        if (gamepad == null)
            return; 

        if (gamepad.sKey.wasPressedThisFrame)
        {       
            //  Quit();
            OpenSettingsMenu();
        }
    }
}
