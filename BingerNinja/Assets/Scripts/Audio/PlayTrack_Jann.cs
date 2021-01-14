// Jann

/// Plays the selected track

// Jann 25/10/20 - Playing a track from a .json file implemented
// Jann 28/10/20 - Singleton, play sound/music methods, QA improvements
// Jann 11/11/20 - Bugfix and new Singleton applied
// Jann 25/11/20 - Adapt to smaller .json files

using UnityEngine;

public class PlayTrack_Jann : Singleton_Jann<PlayTrack_Jann>
{
    private int SF = 48000;

    [SerializeField] private TextAsset m_musicOnStart;
    
    [SerializeField] private float m_volumeSound = 0.1f;
    [SerializeField] private float m_volumeMusic = 0.1f;
    [SerializeField] private bool m_loopMusic;
    
    private int pos;
    private float cf;

    [SerializeField] private AudioSource[] m_musicAudioSources; // Three channels for music
    [SerializeField] private AudioSource[] m_soundAudioSources; // Five channels for sounds

    private void Awake()
    {
        base.Awake();
        
        Cursor.lockState = CursorLockMode.Confined;

        SettingsData sd = SaveLoadSystem_JamieG.LoadSettings();
        if (!sd.Equals(default(SettingsData)))
        {
            UpdateMusicVolume(sd.m_musicVolume);
            UpdateSfxVolume(sd.m_sfxVolume);
        }

        foreach (AudioSource ms in m_musicAudioSources)
        {
            ms.volume = m_volumeMusic;
            ms.loop = m_loopMusic;
        }
        
        foreach (AudioSource ss in m_soundAudioSources)
        {
            ss.volume = m_volumeSound;
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
    public void EM(string an)
    {
        Track_Jann t = LoadTrack(an);
        AudioClip c = CreateClip(t.n, (float)t.b / 60, t.f[0]);

        foreach (AudioSource ss in m_soundAudioSources)
        {
            ss.clip = c;
            if (!ss.isPlaying)
            {
                ss.Play();
                break;
            }
            else if(ss.clip.name == t.n)
            { 
                ss.Stop();
                ss.Play();
                break;
            }
        }
    }

    public void PlayMusic(string an)
    {
        Track_Jann t = LoadTrack(an);

        for (int i = 0; i < t.f.Length; i++)
        {
            AudioClip c = CreateClip(t.n, (float) t.b / 60, t.f[i]);
            m_musicAudioSources[i].clip = c;
            m_musicAudioSources[i].Play();
        }
    }
    
    public void PlayMusic(TextAsset tf)
    {
        Track_Jann t = JsonUtility.FromJson<Track_Jann>(tf.text);
        t.GenerateFrequencies();
            
        for (int i = 0; i < t.f.Length; i++)
        {
            AudioClip c = CreateClip(t.n, (float) t.b / 60, t.f[i]);
            m_musicAudioSources[i].clip = c;
            m_musicAudioSources[i].Play();
        }
    }
    #endregion

    #region Generate audio from track file
    public Track_Jann LoadTrack(string an)
    {
        TextAsset tf = AudioFiles.GetAudio(an);
        Track_Jann t = JsonUtility.FromJson<Track_Jann>(tf.text);
        t.GenerateFrequencies();

        return t;
    }
    
    // Creates an AudioClip from the notes in a channel
    private AudioClip CreateClip(string tn, float bps, int[] f)
    {
        int ls = (int) (SF / bps);
        float[] d = new float[ls * f.Length];
        int cl = 0;
        
        for (int i = 0; i < f.Length; i++)
        {
            cf = f[i];
            AudioClip c = AudioClip.Create("", ls, 1, SF, false, OnAudioRead, OnAudioSetPosition);
            
            float[] buffer = new float[c.samples];
            c.GetData(buffer, 0);
            buffer.CopyTo(d, cl);
            cl += c.samples;
        }
        
        AudioClip nc = AudioClip.Create(tn, cl, 1, SF, false);
        nc.SetData(d, 0);
        return nc;
    }

    // Called once at clip creation
    void OnAudioRead(float[] d)
    {
        int c = 0;
        while (c < d.Length)
        {
            // Sets data[count] to -1 or 1, resulting in 8-bit like sounds
            d[c] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * cf * pos / SF));
            
            pos++;
            c++;
        }
    }
    
    // Called when track loops or changes playback position
    void OnAudioSetPosition(int np)
    {
        pos = np;
    }
    #endregion

    public void UpdateMusicVolume(float v)
    {
        m_volumeMusic = v;

        foreach (var source in m_musicAudioSources)
        {
            source.volume = v / 30f;
        }
    }

    public void UpdateSfxVolume(float v)
    {
        m_volumeSound = v;
        
        foreach (var source in m_soundAudioSources)
        {
            source.volume = v / 50f;
        }
    }
}
