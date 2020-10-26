using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_LouieWilliamson : MonoBehaviour
{
    //Sound Effect Enum and Dictionary setup
    public enum SFX { sound1, sound2 };
    public List<AudioClip> SFXList = new List<AudioClip>();
    private Dictionary<SFX, AudioClip> SFXDictionary = new Dictionary<SFX, AudioClip>();
    public GameObject SFXPrefab;
    //Music enum and Dictionary setup
    public enum Music { MainMenu, Level1 };
    public AudioSource MusicPrefab;
    public List<AudioClip> MusicList = new List<AudioClip>();
    private Dictionary<Music, AudioClip> MusicDictionary = new Dictionary<Music, AudioClip>();

    //test
    float timer;
    void Start()
    {
        //Populate Dictionaries
        SFXDictionary.Add(SFX.sound1, SFXList[0]);
        SFXDictionary.Add(SFX.sound2, SFXList[1]);

        MusicDictionary.Add(Music.MainMenu, MusicList[0]);
        MusicDictionary.Add(Music.Level1, MusicList[1]);

        timer = 0;
        ChangeMusic(Music.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5.0f)
        {
            ToggleMusic();
            timer = 0;
        }
    }
    public void PlaySFX(SFX s)
    {
        //spawn sfx prefab and get audio source component
        AudioSource SoundEffect = Instantiate(SFXPrefab).GetComponent<AudioSource>();
        //play sfx
        SoundEffect.PlayOneShot(SFXDictionary[s]);
        //destroy prefab instance when sound has played
        Destroy(SoundEffect.gameObject, SFXDictionary[s].length);
    }

    public void ChangeMusic(Music m)
    {
        MusicPrefab.clip = MusicDictionary[m];
        ToggleMusic();
    }

    public void ToggleMusic()
    {
        if (MusicPrefab.isPlaying)
        {
            MusicPrefab.Pause();
        }
        else
        {
            MusicPrefab.Play();
        }
    }
}
