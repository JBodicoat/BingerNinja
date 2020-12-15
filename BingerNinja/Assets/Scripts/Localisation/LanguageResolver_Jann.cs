//Jann

//Jann  - 18/11/20 - Implementation
//Jann  - 25/11/20 - Caching implemented
//Jann  - 13/12/20 - Resources folder instead of assets folder is now used

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class applies the correct localisation to all UI Text elements
/// </summary>
public class LanguageResolver_Jann : Singleton_Jann<LanguageResolver_Jann>
{
    private const string FilePath = "";
    private const char Separator = '=';
    private Dictionary<string, TextAsset> m_languageFiles = new Dictionary<string, TextAsset>();
    private Dictionary<string, string> m_translations = new Dictionary<string, string>();
    
    private void Awake()
    {
        base.Awake();
        
        string language = SaveLoadSystem_JamieG.LoadSettings().m_chosenLanguage;

        if (language == null)
        {
            language = "English"; // Change to English for testing
        }
        
        ReadProperties(language);
    }

    private void Start()
    {
        ResolveTexts();
    }

    public void RefreshTranslation(string language)
    {
        ReadProperties(language);
        ResolveTexts();
    }

    private void ResolveTexts()
    {
        LanguageText_Jann[] allTexts = Resources.FindObjectsOfTypeAll<LanguageText_Jann>();
        foreach (LanguageText_Jann languageText in allTexts)
        {
            try
            {
                Text text = languageText.GetComponent<Text>();
                text.text = Regex.Unescape(m_translations[languageText.identifier]);
            }
            catch (KeyNotFoundException e)
            {
                print("No translation for key found: " + languageText.identifier);
                throw;
            }
        }
    }
    
    private void ReadProperties(string language)
    {
        TextAsset languageFile = LoadLanguageFile(language);
        foreach (string line in languageFile.text.Split('\n'))
        {
            var prop = line.Split(Separator);
            m_translations[prop[0]] = prop[1];
        }
    }

    private TextAsset LoadLanguageFile(string language)
    {
        if (m_languageFiles.ContainsKey(language))
        {
            return m_languageFiles[language];
        }
        
        TextAsset file = Resources.Load<TextAsset>(FilePath + language);
        
        if (file == null)
        {
            Debug.LogWarning("File not found: " + FilePath + language + ".txt");
            Debug.LogWarning("Loading default language...");
            file = Resources.Load<TextAsset>(FilePath + "English");
        }
        m_languageFiles.Add(language, file);
        return file;
    }
}
