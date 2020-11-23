using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LanguageResolver_Jann : Singleton_Jann<LanguageResolver_Jann>
{
    private const string FilePath = "Assets/Scripts/Localisation/";
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
            Text text = languageText.GetComponent<Text>();
            text.text = Regex.Unescape(m_translations[languageText.identifier]);
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
        
        TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>(FilePath + language + ".txt");
        
        if (file == null)
        {
            Debug.LogWarning("File not found: " + FilePath + language + ".txt");
            Debug.LogWarning("Loading default language...");
            file = AssetDatabase.LoadAssetAtPath<TextAsset>(FilePath + "English.txt");
        }
        m_languageFiles.Add(language, file);
        return file;
    }
}
