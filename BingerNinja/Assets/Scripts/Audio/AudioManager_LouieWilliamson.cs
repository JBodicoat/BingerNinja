﻿//Louie Williamson
///This script handles the functionality for playing music and sound effects.
///It also contains 2 dictionaries. One each for sfx and music.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager_LouieWilliamson : MonoBehaviour
{
    //Sound Effect Enum and Dictionary setup
    public enum SFX { PlayerDamage, PlayerDeath, PlayerAttack, PlayerThrowAttack, Detection, Eating, EnemyAttack, EnemyThrowAttack };
    public List<AudioClip> SFXList = new List<AudioClip>();
    private Dictionary<SFX, AudioClip> SFXDictionary = new Dictionary<SFX, AudioClip>();
    public GameObject SFXPrefab;
   // private PlayTrack_Jann music;

    //Music enum and Dictionary setup
    public enum Music { MainMenu, Level1 };
    public AudioSource MusicPrefab;
    public List<AudioClip> MusicList = new List<AudioClip>();
    private Dictionary<Music, AudioClip> MusicDictionary = new Dictionary<Music, AudioClip>();
    
    //This function plays a sfx given an enum value from the SFX enum
    public void PlaySFX(SFX s)
    {
        //spawn sfx prefab and get audio source component
        AudioSource SoundEffect = Instantiate(SFXPrefab).GetComponent<AudioSource>();
        //play sfx
        SoundEffect.PlayOneShot(SFXDictionary[s]);
        //destroy prefab instance when sound has played
        Destroy(SoundEffect.gameObject, SFXDictionary[s].length);
    }
    //This function sets the music to the track given through a Music enum value
    public void SetMusicSource(Music m)
    {
        MusicPrefab.clip = MusicDictionary[m];
        MusicPrefab.Play();
    }

    //This function pauses/ plays the music depending on the bool value given
    public void SetMusicOnorOff(bool on) //Jack or Mario I know this is a bad bool name so pls advise on what I should call it XD
    {
        if (on == true)
        {
            MusicPrefab.Play();
        }
        else
        {
            MusicPrefab.Pause();
        }
    }
    
    void Start()
    {
        //Populate Dictionaries
        SFXDictionary.Add(SFX.PlayerDamage, SFXList[0]);
        SFXDictionary.Add(SFX.PlayerDeath, SFXList[1]);
        SFXDictionary.Add(SFX.PlayerAttack, SFXList[2]);
        SFXDictionary.Add(SFX.PlayerThrowAttack, SFXList[3]);
        SFXDictionary.Add(SFX.Detection, SFXList[4]);
        SFXDictionary.Add(SFX.Eating, SFXList[5]);
        SFXDictionary.Add(SFX.EnemyAttack, SFXList[6]);
        SFXDictionary.Add(SFX.EnemyThrowAttack, SFXList[7]);

        MusicDictionary.Add(Music.MainMenu, MusicList[0]);
        MusicDictionary.Add(Music.Level1, MusicList[1]);

        GetBuildIndex();
    }


    void Update()
    {

    }
    private void GetBuildIndex()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        switch (currentScene)
        {
            case 0:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_MainMenu);
                // SetMusicSource(Music.MainMenu);
                break;
            case 1:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level1);
              //  SetMusicSource(Music.Level1);
                break;
            case 2:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level1);
                //SetMusicSource(Music.Level1);
                break;
            case 3:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level1);
                break;
            case 4:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level2);
                break;
            case 5:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level2);
                break;
            case 6:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level2);
                break;
            case 7:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level3);
                break;
            case 8:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level3);
                break;
            case 9:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level3);
                break;
            case 10:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level4);
                break;
            case 11:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level4);
                break;
            case 12:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level4);
                break;
            case 13:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level5);
                break;
            case 14:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level5);
                break;
            case 15:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level5);
                break;
            case 16:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level6);
                break;
            case 17:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level6);
                break;
            case 18:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level6);
                break;
            case 19:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level7);
                break;
            case 20:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_Level7);
                break;
            case 21:
                PlayTrack_Jann.Instance.PlayMusic(AudioFiles.Music_MainMenu);
                break;
            default:
                break;
        }
    }
}
