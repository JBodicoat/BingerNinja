using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveatePauseMenu_Elliott : MonoBehaviour
{
    public GameObject m_pauseMenu;

    public void OpenPauseMenu()
    {
        if (VendingMachineMenu_Elliott.m_gameIsPaused == false)
        {
            Time.timeScale = 0f;
            m_pauseMenu.SetActive(true);
        }
    }

    private void Start()
    {
        m_pauseMenu.SetActive(false);
    }
    void Update()
    {
        var gamepad = Keyboard.current;
        if (gamepad == null)
            return;
 
        if (gamepad.pKey.wasPressedThisFrame)
        {
            OpenPauseMenu();
        }
    }
}