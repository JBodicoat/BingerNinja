// Jann

/// Window for the Synthesizer in Tools/Synthesizer

// Jann 21/10/20 - GUI Layout implemented
// Jann 25/10/20 - Added frequency generation
// Jann 28/10/20 - QA improvements
// Jann 11/11/20 - Added audio file loading and changed max bpm
// Jann 25/11/20 - Adapt to smaller .json files

using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        if (m_noteCreator == null)
        {
            m_noteCreator = new NotesCreator_Jann();
        }
        
        m_settingsBounds = new Rect(0, 0, position.width, 120);
        m_notesBounds = new Rect(0, m_settingsBounds.height, position.width, position.height - m_settingsBounds.height);

        // Create settings menu
        GUILayout.BeginArea(m_settingsBounds);
        OnCreateSynthesizerSettings();
        GUILayout.EndArea();
        
        GUILayout.Space(100);
        
        // Generate notes based on settings menu
        if(m_channelsData.Length * m_channelsData[0].Count != m_length * m_channels)
            SetupNotes();

        // Create notes UI
        GUILayout.BeginArea(m_notesBounds);
        OnCreateNotesInterface();
        GUILayout.EndArea();
    }

    // Creates the settings UI: Title, bpm, notes per channel, number of channels and buttons for reset and save
    private void OnCreateSynthesizerSettings()
    {
        GUILayout.Label("Audio Settings", EditorStyles.boldLabel);
        m_title = EditorGUILayout.TextField ("Title", m_title);

        // Clamp values
        m_bpm = Mathf.Clamp(EditorGUILayout.IntField("BPM", m_bpm), 40, 4000);
        m_length = Mathf.Clamp(EditorGUILayout.IntField("Length", m_length), 1, 200);
        m_channels = Mathf.Clamp(EditorGUILayout.IntField("Channels", m_channels), 1, 3);

        #region Generate and handle buttons
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(20)))
        {
            ResetNotes();
        }
        
        EditorGUILayout.Space(10);
        
        if(GUILayout.Button("Load Track", GUILayout.Width(100), GUILayout.Height(20)))
        {
            string path = EditorUtility.OpenFilePanel("Load audio file", "", "json");
            if (path.Length != 0)
            {
                LoadTrack(path);
            }
        }
        
        EditorGUILayout.Space(10);
        
        if(GUILayout.Button("Save Track", GUILayout.Width(100), GUILayout.Height(20)))
        {
            SaveTrack();
        }
        
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    // Creates the notes for each channel
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
                m_channelsData[y][x].Frequence = m_noteCreator.GetFrequency(m_channelsData[y][x].MNoteName);
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
        track.n = m_title;
        track.b = m_bpm;
        track.c = m_length;

        string[] data = new string[m_channels * m_length];
        for (int y = 0; y < m_channels; y++)
        {
            for (int x = 0; x < m_length; x++)
            {
                data[y * m_length + x] = string.Format(CultureInfo.InvariantCulture, "{0:0.##}", m_channelsData[y][x].Frequence);
            }
        }

        track.d = data;
        
        string trackJson = JsonUtility.ToJson(track);
        File.WriteAllText(Application.dataPath + "/Audio/" + track.n + ".json", trackJson);
    }

    private void LoadTrack(string path)
    {
        string json = File.ReadAllText(path);
        
        Track_Jann track = JsonUtility.FromJson<Track_Jann>(json);
        m_title = track.n;
        m_bpm = track.b;
        m_length = track.c;
        m_channels = track.d.Length / m_length;

        ResetNotes();
        
        // float[] data = new float[m_channels * m_length];
        for (int y = 0; y < m_channels; y++)
        {
            for (int x = 0; x < m_length; x++)
            {
                float frequency = float.Parse(track.d[y * m_length + x], CultureInfo.InvariantCulture);
                m_channelsData[y][x].Frequence = frequency;
                m_channelsData[y][x].MNoteName = m_noteCreator.GetNote(frequency);
            }
        }
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