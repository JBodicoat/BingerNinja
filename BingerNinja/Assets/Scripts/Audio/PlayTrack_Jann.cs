// Jann

/// Plays the selected track

// Jann 25/10/20 - Playing a track from a .json file implemented
// Jann xx/10/20 - TODO: Sanity checks:& max channel

using UnityEngine;

public class PlayTrack_Jann : MonoBehaviour
{
    private int SamplingFrequency = 48000;

    public TextAsset m_trackFile;
    
    public float m_volume = 0.1f;
    public bool m_loop;
    
    private int m_audioLength;
    private int m_position;
    private float m_currentFrequency;
    
    private Track_Jann m_track;
    private AudioSource[] m_audioSources;

    private void Awake()
    {
        m_audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in m_audioSources)
        {
            audioSource.volume = m_volume;
            audioSource.loop = m_loop;
        }
    }
    
    private void Start()
    {
        m_track = JsonUtility.FromJson<Track_Jann>(m_trackFile.text);
        m_track.GenerateFrequencies();

        m_audioLength = m_track.bpm / 60;

        for (int i = 0; i < m_track.frequencies.Length; i++)
        {
            AudioClip clip = CreateClip(m_track.frequencies[i]);
            m_audioSources[i].clip = clip;
            m_audioSources[i].Play();
        }
    }
    
    private AudioClip CreateClip(float[] frequencies)
    {
        float[] data = new float[SamplingFrequency * frequencies.Length];
        int audioLength = 0;
        
        for (int i = 0; i < frequencies.Length; i++)
        {
            m_currentFrequency = frequencies[i];
            AudioClip clip = AudioClip.Create("", SamplingFrequency / m_audioLength, 1, SamplingFrequency, false, OnAudioRead, OnAudioSetPosition);
            
            float[] buffer = new float[clip.samples];
            clip.GetData(buffer, 0);
            buffer.CopyTo(data, audioLength);
            audioLength += clip.samples;
        }
        
        AudioClip newClip = AudioClip.Create(m_track.name, audioLength, 1, SamplingFrequency, false);
        newClip.SetData(data, 0);
        return newClip;
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * m_currentFrequency * m_position / SamplingFrequency));
            
            m_position++;
            count++;
        }
    }
    
    void OnAudioSetPosition(int newPosition)
    {
        m_position = newPosition;
    }
}
