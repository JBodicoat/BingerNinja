using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SettingsMenu_ElliottDesouza : MonoBehaviour
{
    
    public void IncreaseVolume()
    {

    }

    public void DecreaseVolume()
    {

    }

    public void IncreaseSFX()
    {

    }

    public void DecreaseSFX()
    {

    }

    public void ExitSettingMenu()
    {
        //open up settings
        Debug.Log("back");
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        var gamepad = Mouse.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.leftButton.wasPressedThisFrame)
        {
            // 'Use' code here
            //  Quit();
            ExitSettingMenu();
        }

        // Vector2 move = gamepad.leftStick.ReadValue();
        // 'Move' code here
    }
}
