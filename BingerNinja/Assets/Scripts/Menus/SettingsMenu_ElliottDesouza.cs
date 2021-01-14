//Elliott Desouza
/// class holds values for the settings menu

//Elliott 26/10/2020 -  the back function closes the settings menu
//Elliott 27/10/2020 -  can now increse the music and sfx slider value via input
//Jann    20/11/2020 -  Hooked up the save system
//Jann    25/11/2020 -  Saving now updates the DialogueManager
//Jann    28/11/2020 -  Added GameState saving

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsMenu_ElliottDesouza : MonoBehaviour
{
    public Button m_englishButton;
    public Button m_portugueseButton;
    
    public Slider m_SFXSlider;
    public Slider m_musicSlider;

    public string m_selectedLanguage;
    private bool m_isEnglish;

    private void Start()
    {
        o settings = SaveLoadSystem_JamieG.i();
        m_musicSlider.value = settings.a;
        m_SFXSlider.value = settings.d;
        m_selectedLanguage = settings.s;
        
        m_englishButton.interactable = !m_selectedLanguage.Equals("English");
        m_portugueseButton.interactable = m_selectedLanguage.Equals("English");
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
        
        // Change language in DialogManager if it is present in scene
        GameObject dialogManager = GameObject.Find("DialogManager");
        if (dialogManager != null)
        {
            dialogManager.GetComponent<DialogueManager_MarioFernandes>().f();
        }
        
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