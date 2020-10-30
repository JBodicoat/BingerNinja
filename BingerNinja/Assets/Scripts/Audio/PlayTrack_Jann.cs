// Jann

/// Plays the selected track

// Jann 25/10/20 - Playing a track from a .json file implemented
// Jann 28/10/20 - Singleton, play sound/music methods, QA improvements

using UnityEngine;

public class PlayTrack_Jann : MonoBehaviour
{
    private static PlayTrack_Jann m_instance;
    
    private int SamplingFrequency = 48000;

    [SerializeField] private TextAsset m_musicOnStart;
    
    [SerializeField] private float m_volumeSound = 0.1f;
    [SerializeField] private float m_volumeMusic = 0.1f;
    [SerializeField] private bool m_loopMusic;
    
    private int m_position;
    private float m_currentFrequency;

    [SerializeField] private AudioSource[] m_musicAudioSources; // Three channels for music
    [SerializeField] private AudioSource[] m_soundAudioSources; // Five channels for sounds

    private void Awake()
    {
        // Singleton setup
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        } else {
            m_instance = this;
        }
        
        foreach (AudioSource musicSource in m_musicAudioSources)
        {
            musicSource.volume = m_volumeMusic;
            musicSource.loop = m_loopMusic;
        }
        
        foreach (AudioSource soundSource in m_soundAudioSources)
        {
            soundSource.volume = m_volumeSound;
        }
    }
    
    private void Start()
    {
        if (m_musicOnStart != null)
        {
            PlayMusic(m_musicOnStart);
        }
    }

    #region Play audio
    public void PlaySound(string audioName)
    {
        Track_Jann track = LoadTrack(audioName);

        foreach (AudioSource soundSource in m_soundAudioSources)
        {
            if (!soundSource.isPlaying)
            {
                AudioClip clip = CreateClip(track.name, (float) track.bpm / 60, track.frequencies[0]);
                soundSource.clip = clip;
                soundSource.Play();
                break;
            }
            else if(soundSource.clip.name == track.name)
            {
                soundSource.Stop();
                soundSource.Play();
                break;
            }
        }
    }

    public void PlayMusic(string audioName)
    {
        Track_Jann track = LoadTrack(audioName);

        for (int i = 0; i < track.frequencies.Length; i++)
        {
            AudioClip clip = CreateClip(track.name, (float) track.bpm / 60, track.frequencies[i]);
            m_musicAudioSources[i].clip = clip;
            m_musicAudioSources[i].Play();
        }
    }
    
    public void PlayMusic(TextAsset trackFile)
    {
        Track_Jann track = JsonUtility.FromJson<Track_Jann>(trackFile.text);
        track.GenerateFrequencies();
            
        for (int i = 0; i < track.frequencies.Length; i++)
        {
            AudioClip clip = CreateClip(track.name, (float) track.bpm / 60, track.frequencies[i]);
            m_musicAudioSources[i].clip = clip;
            m_musicAudioSources[i].Play();
        }
    }
    #endregion

    #region Generate audio from track file
    private Track_Jann LoadTrack(string audioName)
    {
        TextAsset trackFile = AudioFiles.GetAudio(audioName);
        Track_Jann track = JsonUtility.FromJson<Track_Jann>(trackFile.text);
        track.GenerateFrequencies();

        return track;
    }
    
    // Creates an AudioClip from the notes in a channel
    private AudioClip CreateClip(string trackName, float bps, float[] frequencies)
    {
        int lengthSamples = (int) (SamplingFrequency * bps);
        float[] data = new float[lengthSamples * frequencies.Length];
        int clipLength = 0;
        
        for (int i = 0; i < frequencies.Length; i++)
        {
            m_currentFrequency = frequencies[i];
            AudioClip clip = AudioClip.Create("", lengthSamples, 1, SamplingFrequency, false, OnAudioRead, OnAudioSetPosition);
            
            float[] buffer = new float[clip.samples];
            clip.GetData(buffer, 0);
            buffer.CopyTo(data, clipLength);
            clipLength += clip.samples;
        }
        
        AudioClip newClip = AudioClip.Create(trackName, clipLength, 1, SamplingFrequency, false);
        newClip.SetData(data, 0);
        return newClip;
    }

    // Called once at clip creation
    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            // Sets data[count] to -1 or 1, resulting in 8-bit like sounds
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * m_currentFrequency * m_position / SamplingFrequency));
            
            m_position++;
            count++;
        }
    }
    
    // Called when track loops or changes playback position
    void OnAudioSetPosition(int newPosition)
    {
        m_position = newPosition;
    }
    #endregion

    #region Properties

    public float VolumeSound
    {
        get => m_volumeSound;
        set => m_volumeSound = value;
    }

    public float VolumeMusic
    {
        get => m_volumeMusic;
        set => m_volumeMusic = value;
    }

    public bool LoopMusic
    {
        get => m_loopMusic;
        set => m_loopMusic = value;
    }

    public static PlayTrack_Jann Instance => m_instance;
    #endregion
}
