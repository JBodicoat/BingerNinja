// Jann

// Jann 28/10/20 - Implementation + test values

using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Dictionary for all audio files
/// Use AudioFiles.<AudioVariable> to play sounds
/// Example: PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_MeeleAttack);
/// </summary>
public static class AudioFiles
{
    public static string Sound_Detection = "Sound_Detection";
    public static string Sound_Death = "Sound_Death";
    public static string Sound_Damage = "Sound_Damage";
    public static string Sound_Eating = "Sound_Eating";
    public static string Sound_PlayerThrow = "Sound_PlayerThrow";
    public static string Sound_PlayerAttack = "Sound_PlayerAttack";
    public static string Sound_EnemyAttack = "Sound_EnemyAttack";
    public static string Music_MainMenu = "Music_MainMenu";
    public static string Music_Level1 = "Music_Level1";
    public static string Music_Level2 = "Music_Level2";
    public static string Music_Level3 = "Music_Level3";
    public static string Music_Level4 = "Music_Level4";
    public static string Music_Level5 = "Music_Level5";
    public static string Music_Level6 = "Music_Level6";
    public static string Music_Level7 = "Music_Level7";    
    public static string Sound_DialogSFX = "Sound_DialogSFX";
    public static TextAsset GetAudio(string filename)
    {
        TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Audio/" + filename + ".json");
        if (file == null)
        {
            throw new FileNotFoundException("File not found at: Assets/Audio/" + filename + ".json");
        }

        return file;
    }
}
