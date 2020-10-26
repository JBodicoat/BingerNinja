using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu_Manager_Elliott : MonoBehaviour
{


    public GameObject SettingMenu;
    protected bool m_openSettings;

    public void Resume()
    {

    }

    public void StartNewGame()
    {
        //load level 1 here
       // SceneManager.LoadScene("Level 1");
    }

    public void OpenSettingsMenu()
    {
        //open up settings
        Debug.Log("forward");
            SettingMenu.SetActive(true);
    }

    public void Quit()
    {
        //quit application
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
        var gamepad = Mouse.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.leftButton.wasPressedThisFrame)
        {
            // 'Use' code here
          //  Quit();
            OpenSettingsMenu();
        }
       // Vector2 move = gamepad.leftStick.ReadValue();
        // 'Move' code here
    }
}
