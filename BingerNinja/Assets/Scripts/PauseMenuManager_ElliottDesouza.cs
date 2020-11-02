//Elliott Desouza
/// class holds values for the pause menu

//Elliott 28/10/2020 - can resume game, return to last check point, reload the scene, open settings and exit to main menu 
//Elliott 31/10/2020 - binded the keys to functions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenuManager_ElliottDesouza : MonoBehaviour
{
    public GameObject m_player;
    public GameObject m_settingMenu;
    protected bool m_openSettings;
    
    public void Resume()
    {
        //set active to false
        gameObject.SetActive(false);
    }
    
    public void ReturnToCheckpoint()
    {
        m_player.transform.position = GameObject.FindGameObjectWithTag("SaveCheckpoint").GetComponent<SaveSystem_ElliottDesouza>().m_currentCheckpoint.position;
        /// note might be better to call the function die(); in the player script as he will not respawn with full health. the function is private 
    }

    public void ResetScene()
    {
        // SceneManager.LoadScene("currnet seane");
    }

    public void OpenSettingsMenu()
    {
        //set ative to settings 
        m_settingMenu.SetActive(true);
    }

    public void ExitToMainMenu()
    {
        // SceneManager.LoadScene("MainMenu");
    }
   
    void Start()
    {
        m_openSettings = false;
    }

    void Update()
    {
        var gamepad = Keyboard.current;
        if (gamepad == null)
            return;

        if (gamepad.pKey.wasPressedThisFrame)
        {
            Resume();
        }
        if (gamepad.rKey.wasPressedThisFrame)
        {
            ReturnToCheckpoint();
        }
        if (gamepad.sKey.wasPressedThisFrame)
        {
            OpenSettingsMenu();
        }
    }
}
