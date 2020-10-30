//Elliott Desouza
/// class holds values for the settings menu

//Elliott 26/10/2020 -  the back function closes the settings menu
//Elliott 27/10/2020 -  can now increse the music and sfx slider value via input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SettingsMenu_ElliottDesouza : MonoBehaviour
{
   
    public Slider m_SFXSlider;
    public Slider m_musicSlider;

    public void IncreaseVolume()
    {
        m_musicSlider.value = m_musicSlider.value + 0.1f;
    }

    public void DecreaseVolume()
    {
        m_musicSlider.value = m_musicSlider.value - 0.1f;
    }

    public void IncreaseSFX()
    {
        m_SFXSlider.value = m_SFXSlider.value + 0.1f;
    }

    public void DecreaseSFX()
    {
        m_SFXSlider.value = m_SFXSlider.value - 0.1f;
    }

    public void ExitSettingMenu()
    {
        ///close up settings
        Debug.Log("back");
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        var gamepad = Keyboard.current;
        if (gamepad == null)
            return; 

        if (gamepad.rKey.wasPressedThisFrame)
        {
            // 'Use' code here          
            //ExitSettingMenu();
            IncreaseVolume();
        }
    }
}
