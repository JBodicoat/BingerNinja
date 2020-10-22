using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script for the pause menu
/// </summary>

public class MenuScript_Jonathan : MonoBehaviour
{

    private void Update()
    {
        pauseMenuToggle();
    }

    public InputAction m_uiActions;
    public bool gameIsPaused = false;

    public void pauseMenuToggle()
    {
        /*
        //No idea how this Input Action thing works
        if (m_uiActions.enabled)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
            gameIsPaused = !gameIsPaused;
        }
        

    }

}
