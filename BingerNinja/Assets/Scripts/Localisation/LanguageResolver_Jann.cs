//Jann

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
    private const char S = '=';
    private Dictionary<string, TextAsset> lfs = new Dictionary<string, TextAsset>();
    private Dictionary<string, string> trs = new Dictionary<string, string>();
    
    void Awake()
    {
        base.Awake();
        
        string l = SaveLoadSystem_JamieG.QU().QO;

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

    public void RefreshTranslation(string l)
    {
        RP(l);
        RT();
    }

    private void RT()
    {
        LanguageText_Jann[] ats = Resources.FindObjectsOfTypeAll<LanguageText_Jann>();
        foreach (LanguageText_Jann lt in ats)
        {
            try
            {
                Text t = lt.GetComponent<Text>();
                t.text = Regex.Unescape(trs[lt.identifier]);
            }
            catch (KeyNotFoundException)
            {
            }
        }
    }
    
    private void RP(string l)
    {
        TextAsset lf = LLF(l);
        foreach (string li in lf.text.Split('\n'))
        {
            var p = li.Split(S);
            trs[p[0]] = p[1];
        }
    }

    private TextAsset LLF(string l)
    {
        if (lfs.ContainsKey(l))
        {
            return lfs[l];
        }
        
        TextAsset file = Resources.Load<TextAsset>(FP + l);
        
        if (file == null)
        {
            file = Resources.Load<TextAsset>(FP + "English");
        }
        lfs.Add(l, file);
        return file;
    }
}
