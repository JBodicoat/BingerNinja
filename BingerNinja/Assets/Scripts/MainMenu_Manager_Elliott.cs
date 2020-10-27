//Elliott Desouza
/// class holds values for mainmenu UI

//Elliott 26/10/2020 - can open up settings menu and quit game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
       // SceneManager.LoadScene("Level 1");
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
