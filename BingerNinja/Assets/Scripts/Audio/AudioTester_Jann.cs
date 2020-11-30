// Jann

/// Script for testing audio files via UI

// Jann 28/10/20 - Implementation + test values

using UnityEngine;

public class AudioTester_Jann : MonoBehaviour
{
    public void OnMeeleAttack()
    {
        PlayTrack_Jann.Instance.PlaySound(AudioFiles.Sound_PlayerAttack);
    }
    
    public void OnMainMenu()
    {
        PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_MainMenu);
    }
}
