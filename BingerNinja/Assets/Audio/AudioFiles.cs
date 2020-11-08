// Jann

/// Dictionary for all audio files
/// Use AudioFiles.<AudioVariable> to play sounds
/// Example: PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_MeeleAttack);

// Jann 28/10/20 - Implementation + test values

using System.IO;
using UnityEditor;
using UnityEngine;

public static class AudioFiles
{
    public static string Sound_MeeleAttack = "Sound_MeeleAttack";

    public static string Music_MainMenu = "Music_MainMenu";

    public static TextAsset GetAudio(string filename)
    {
        //TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Audio/" + filename + ".json");
        //if (file == null)
        //{
        //    throw new FileNotFoundException("File not found at: Assets/Audio/" + filename + ".json");
        //}

        //return file;

        return null;
    }
}
