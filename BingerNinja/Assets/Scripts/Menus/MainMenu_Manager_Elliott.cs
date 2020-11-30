//Elliott Desouza

//Elliott 26/10/2020 - can open up settings menu and quit game
// Jack 02/11/2020 - set StartNewGame() to open scene of build index 1 for VS
//                   moved class header comment to the correct place and reformated.
//                   linked this scripts functions with the MainMenu scene buttons

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// class holds values for mainmenu UI
/// </summary>
public class MainMenu_Manager_Elliott : MonoBehaviour
{
    public GameObject m_settingMenu;
    protected bool m_openSettings;

    public void Resume()
    {

    }

    public void StartNewGame()
    {
        ///load level 1 here
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
