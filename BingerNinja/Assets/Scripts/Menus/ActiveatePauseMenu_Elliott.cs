﻿using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveatePauseMenu_Elliott : MonoBehaviour
{
    public GameObject m_pauseMenu;

    public void OpenPauseMenu()
    {

            Time.timeScale = 0f;
            m_pauseMenu.SetActive(true);
    }

    void Start()
    {
        m_pauseMenu.SetActive(false);
    }
    void Update()
    {
        var a = Keyboard.current;
        if (a == null)
            return;

        if (a.escapeKey.wasPressedThisFrame)
        {
            OpenPauseMenu();
        }
    }
}