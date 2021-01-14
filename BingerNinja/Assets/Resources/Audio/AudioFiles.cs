// Jann

// Jann 28/10/20 - Implementation + test values
// Jann 13/12/20 - Resources folder instead of assets folder is now used

using System.IO;
using UnityEngine;

/// <summary>
/// Dictionary for all audio files
/// Use AudioFiles.<AudioVariable> to play sounds
/// Example: PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_MeeleAttack);
/// </summary>
public static class AudioFiles
{
    public static string a = "Sound_Detection";
    public static string b = "Sound_Death";
    public static string c = "Sound_Damage";
    public static string d = "Sound_Eating";
    public static string e = "Sound_PlayerThrow";
    public static string f = "Sound_PlayerAttack";
    public static string g = "Sound_EnemyAttack";
    public static string h = "Music_MainMenu";
    public static string i = "Music_Level1";
    public static string j = "Music_Level2";
    public static string k = "Music_Level3";
    public static string l = "Music_Level4";
    public static string m = "Music_Level5";
    public static string n = "Music_Level6";
    public static string o = "Music_Level7";    
    public static string p = "Sound_DialogSFX";
    public static TextAsset q(string r)
    {
        TextAsset s = Resources.Load<TextAsset>("Audio/" + r);
        if (s == null)
        {
            throw new FileNotFoundException("File not found at: Resources/Audio/" + r + ".json");
        }

        return s;
    }
}
