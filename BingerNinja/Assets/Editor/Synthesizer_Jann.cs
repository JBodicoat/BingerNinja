// Jann

/// Window for the Synthesizer in Tools/Synthesizer

// Jann 21/10/20 - GUI Layout implemented
// Jann 25/10/20 - Added frequency generation

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Synthesizer_Jann : EditorWindow
{
    private static Vector2 m_windowSize = new Vector2(600, 300);
    private Rect m_settingsBounds;
    private Rect m_notesBounds;
    private Vector2 m_scrollPosition;
    
    private string m_title;
    private int m_bpm = 60;
    private int m_length = 10;
    private int m_channels = 3;

    private NotesCreator_Jann m_noteCreator;
    private List<Note_Jann>[] m_channelsData = {new List<Note_Jann>()};

    [MenuItem ("Tools/Synthesizer")]
    public static void ShowWindow () 
    {
        EditorWindow window = GetWindow<Synthesizer_Jann>();
        
        window.titleContent = new GUIContent("Synthesizer");
        window.position = new Rect(window.position.x, window.position.y, m_windowSize.x, m_windowSize.y);
        
        window.Show();
    }

    void Awake()
    {
        m_noteCreator = new NotesCreator_Jann();
    }

    void OnGUI () 
    {
        m_settingsBounds = new Rect(0, 0, position.width, 120);
        m_notesBounds = new Rect(0, m_settingsBounds.height, position.width, position.height - m_settingsBounds.height);

        GUILayout.BeginArea(m_settingsBounds);
        OnCreateSynthesizerSettings();
        GUILayout.EndArea();
        
        GUILayout.Space(100);
        
        if(m_channelsData.Length * m_channelsData[0].Count != m_length * m_channels)
            SetupNotes();

        GUILayout.BeginArea(m_notesBounds);
        OnCreateNotesInterface();
        GUILayout.EndArea();
    }

    private void OnCreateSynthesizerSettings()
    {
        GUILayout.Label("Audio Settings", EditorStyles.boldLabel);
        m_title = EditorGUILayout.TextField ("Title", m_title);

        m_bpm = Mathf.Clamp(EditorGUILayout.IntField("BPM", m_bpm), 50, 120);
        m_length = Mathf.Clamp(EditorGUILayout.IntField("Length", m_length), 1, 100);
        m_channels = Mathf.Clamp(EditorGUILayout.IntField("Channels", m_channels), 1, 5);

        EditorGUILayout.BeginHorizontal();
        
        if(GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(20)))
        {
            ResetNotes();
        }
        
        EditorGUILayout.Space(10);
        
        if(GUILayout.Button("Save Track", GUILayout.Width(100), GUILayout.Height(20)))
        {
            SaveTrack();
        }
        
        EditorGUILayout.EndHorizontal();
    }

    private void OnCreateNotesInterface()
    {
        GUILayout.Label("Create Notes", EditorStyles.boldLabel);
        
        m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);

        for (int y = 0; y < m_channels; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < m_length; x++)
            {
                m_channelsData[y][x].MNoteName = (NotesCreator_Jann.Note) EditorGUILayout.EnumPopup(
                    "", m_channelsData[y][x].MNoteName,
                    GUILayout.Width(50));
                m_channelsData[y][x].Frequence = m_noteCreator.getFrequency(m_channelsData[y][x].MNoteName);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.EndScrollView();
    }

    // Adjusts the channelsData array to fit given length * channels
    // Keeps old data in the process
    private void SetupNotes()
    {
        List<Note_Jann>[] temp = new List<Note_Jann>[m_channels];
        
        for (int y = 0; y < m_channels; y++)
        {
            temp[y] = new List<Note_Jann>();
            
            for (int x = 0; x < m_length; x++)
            {
                if (y >= m_channelsData.Length || x >= m_channelsData[y].Count)
                {
                    temp[y].Add(
                        new Note_Jann(NotesCreator_Jann.Note.None, 
                        0f)
                        );
                }
                else
                {
                    temp[y].Add(m_channelsData[y][x]);   
                }
            }
        }

        m_channelsData = temp;
    }

    private void SaveTrack()
    {
        Track_Jann track = new Track_Jann();
        track.name = m_title;
        track.bpm = m_bpm;
        track.channelLength = m_length;

        float[] data = new float[m_channels * m_length];
        for (int y = 0; y < m_channels; y++)
        {
            for (int x = 0; x < m_length; x++)
            {
                data[y * m_length + x] = m_channelsData[y][x].Frequence;
            }
        }

        track.data = data;
        
        string trackJson = JsonUtility.ToJson(track);
        System.IO.File.WriteAllText(Application.dataPath + "/Audio/" + track.name + ".json", trackJson);
    }
    
    // Resets all notes to None/0f in every channel
    private void ResetNotes()
    {
        m_channelsData = new List<Note_Jann>[m_channels];
        
        for (int y = 0; y < m_channels; y++)
        {
            m_channelsData[y] = new List<Note_Jann>();
            
            for (int x = 0; x < m_length; x++)
            {
                m_channelsData[y].Add(new Note_Jann(NotesCreator_Jann.Note.None, 0f));
            }
        }
    }
}