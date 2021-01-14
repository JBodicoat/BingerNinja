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
     bool d;

     void Start()
    {
        WT settings = SaveLoadSystem_JamieG.QU();
        m_musicSlider.value = settings.WY;
        m_SFXSlider.value = settings.WU;
        m_selectedLanguage = settings.QO;
        
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
        SaveLoadSystem_JamieG.QW(this);
        
        // Change language in DialogManager if it is present in scene
        GameObject e = GameObject.Find("DialogManager");
        if (e != null)
        {
            e.GetComponent<DialogueManager_MarioFernandes>().LoadLanguageFile();
        }
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        var f = Keyboard.current;
        if (f == null)
            return;

        if (f.rKey.wasPressedThisFrame)
        {
            // 'Use' code here          
            //ExitSettingMenu();
            IncreaseVolume();
        }
    }
}