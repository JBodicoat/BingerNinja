﻿//Jann

//Jann  - 18/11/20 - Implementation
//Jann  - 25/11/20 - Caching implemented
//Jann  - 13/12/20 - Resources folder instead of assets folder is now used

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class applies the correct localisation to all UI Text elements
/// </summary>
public class LanguageResolver_Jann : Singleton_Jann<LanguageResolver_Jann>
{
    private const string FP = "";
    private const char Sep = '=';
    private Dictionary<string, TextAsset> lfs = new Dictionary<string, TextAsset>();
    private Dictionary<string, string> trs = new Dictionary<string, string>();
    
    private void Awake()
    {
        base.Awake();
        
        string l = SaveLoadSystem_JamieG.LoadSettings().m_chosenLanguage;

        if (l == null)
        {
            l = "English"; // Change to English for testing
        }
        
        RP(l);
    }

    private void Start()
    {
        RT();
    }

    public void RefreshTranslation(string language)
    {
        RP(language);
        RT();
    }

    private void RT()
    {
        LanguageText_Jann[] ats = Resources.FindObjectsOfTypeAll<LanguageText_Jann>();
        foreach (LanguageText_Jann lt in ats)
        {
            try
            {
                Text text = lt.GetComponent<Text>();
                text.text = Regex.Unescape(trs[lt.identifier]);
            }
            catch (KeyNotFoundException)
            {
            }
        }
    }
    
    private void RP(string language)
    {
        TextAsset languageFile = LLF(language);
        foreach (string line in languageFile.text.Split('\n'))
        {
            var prop = line.Split(Sep);
            trs[prop[0]] = prop[1];
        }
    }

    private TextAsset LLF(string language)
    {
        if (lfs.ContainsKey(language))
        {
            return lfs[language];
        }
        
        TextAsset file = Resources.Load<TextAsset>(FP + language);
        
        if (file == null)
        {
            file = Resources.Load<TextAsset>(FP + "English");
        }
        lfs.Add(language, file);
        return file;
    }
}
