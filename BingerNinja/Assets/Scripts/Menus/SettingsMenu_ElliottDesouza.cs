//Elliott Desouza
/// class holds values for the settings menu

//Elliott 26/10/2020 -  the back function closes the settings menu
//Elliott 27/10/2020 -  can now increse the music and sfx slider value via input
//Jann    20/11/2020 -  Hooked up the save system

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsMenu_ElliottDesouza : MonoBehaviour
{
    public Slider m_SFXSlider;
    public Slider m_musicSlider;

    public string m_selectedLanguage;
    private bool m_isEnglish;

    private void Start()
    {
        SettingsData settings = SaveLoadSystem_JamieG.LoadSettings();
        m_musicSlider.value = settings.m_musicVolume;
        m_SFXSlider.value = settings.m_sfxVolume;
        m_selectedLanguage = settings.m_chosenLanguage;
        
        GameObject.Find("English Button").GetComponent<Button>().interactable =
            !m_selectedLanguage.Equals("English");
        
        GameObject.Find("Portuguese Button").GetComponent<Button>().interactable =
            m_selectedLanguage.Equals("English");
    }

    public void OnEnglishSelected()
    {
        m_selectedLanguage = "English";
        LanguageResolver_Jann.Instance.RefreshTranslation(m_selectedLanguage);
    }

    public void OnPortugueseSelected()
    {
        m_selectedLanguage = "Portuguese";
        LanguageResolver_Jann.Instance.RefreshTranslation(m_selectedLanguage);
    }

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
        SaveLoadSystem_JamieG.SaveSettings(this);
        gameObject.SetActive(false);
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