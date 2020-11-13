using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LanguageResolver_Jann : Singleton_Jann<LanguageResolver_Jann>
{
    private const string FilePath = "Assets/Scripts/Localisation/";
    private const char Separator = '=';
    private Dictionary<string, string> m_translations = new Dictionary<string, string>();
    private string m_language;    
    
    private void Awake()
    {
        ReadProperties();
    }

    private void Start()
    {
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
    
    private void ReadProperties()
    {
        m_language = SaveLoadSystem_JamieG.LoadSettings().m_chosenLanguage;

        if (m_language == null)
        {
            m_language = "Portugese";
        }
        
        TextAsset languageFile = LoadLanguageFile(m_language);
        foreach (string line in languageFile.text.Split('\n'))
        {
            var prop = line.Split(Separator);
            m_translations[prop[0]] = prop[1];
        }
    }

    private TextAsset LoadLanguageFile(string language)
    {
        TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>(FilePath + m_language + ".txt");
        
        if (file == null)
        {
            Debug.LogWarning("File not found: " + FilePath + m_language + ".txt");
            Debug.LogWarning("Loading default language...");
            file = AssetDatabase.LoadAssetAtPath<TextAsset>(FilePath + "English.txt");
        }
        
        return file;
    }
}
