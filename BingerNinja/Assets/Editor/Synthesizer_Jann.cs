// Jann

/// Window for the Synthesizer in Tools/Synthesizer

// Jann 21/10/20 - GUI Layout implemented
// Jann 25/10/20 - Added frequency generation
// Jann 28/10/20 - QA improvements
// Jann 11/11/20 - Added audio file loading and changed max bpm
// Jann 25/11/20 - Adapt to smaller .json files

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

class Synthesizer_Jann : EditorWindow
{
    private static Vector2 ws = new Vector2(600, 300);
    private Rect sbs;
    private Rect nbs;
    private Vector2 spos;
    
    private string tit;
    private int bpm = 60;
    private int len = 10;
    private int ch = 3;

    private NotesCreator_Jann nc;
    private List<Note_Jann>[] chd = {new List<Note_Jann>()};

    [MenuItem ("Tools/Synthesizer")]
    public static void ShowWindow () 
    {
        EditorWindow w = GetWindow<Synthesizer_Jann>();
        
        w.titleContent = new GUIContent("Synthesizer");
        w.position = new Rect(w.position.x, w.position.y, ws.x, ws.y);
        
        w.Show();
    }

    void Awake()
    {
        nc = new NotesCreator_Jann();
    }

    void OnGUI () 
    {
        if (nc == null)
        {
            nc = new NotesCreator_Jann();
        }
        
        sbs = new Rect(0, 0, position.width, 120);
        nbs = new Rect(0, sbs.height, position.width, position.height - sbs.height);

        // Create settings menu
        GUILayout.BeginArea(sbs);
        OCSS();
        GUILayout.EndArea();
        
        GUILayout.Space(100);
        
        // Generate notes based on settings menu
        if(chd.Length * chd[0].Count != len * ch)
            SN();

        // Create notes UI
        GUILayout.BeginArea(nbs);
        OCNI();
        GUILayout.EndArea();
    }

    // Creates the settings UI: Title, bpm, notes per channel, number of channels and buttons for reset and save
    private void OCSS()
    {
        GUILayout.Label("Audio Settings", EditorStyles.boldLabel);
        tit = EditorGUILayout.TextField ("Title", tit);

        // Clamp values
        bpm = Mathf.Clamp(EditorGUILayout.IntField("BPM", bpm), 40, 4000);
        len = Mathf.Clamp(EditorGUILayout.IntField("Length", len), 1, 500);
        ch = Mathf.Clamp(EditorGUILayout.IntField("Channels", ch), 1, 3);

        #region Generate and handle buttons
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(20)))
        {
            RN();
        }
        
        EditorGUILayout.Space(10);
        
        if(GUILayout.Button("Load Track", GUILayout.Width(100), GUILayout.Height(20)))
        {
            string path = EditorUtility.OpenFilePanel("Load audio file", "", "json");
            if (path.Length != 0)
            {
                LT(path);
            }
        }
        
        EditorGUILayout.Space(10);
        
        if(GUILayout.Button("Save Track", GUILayout.Width(100), GUILayout.Height(20)))
        {
            ST();
        }
        
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    // Creates the notes for each channel
    private void OCNI()
    {
        GUILayout.Label("Create Notes", EditorStyles.boldLabel);
        
        spos = GUILayout.BeginScrollView(spos);

        for (int y = 0; y < ch; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < len; x++)
            {
                chd[y][x].MNoteName = (NotesCreator_Jann.Note) EditorGUILayout.EnumPopup(
                    "", chd[y][x].MNoteName,
                    GUILayout.Width(50));
                chd[y][x].Frequence = nc.GetFrequency(chd[y][x].MNoteName);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.EndScrollView();
    }

    // Adjusts the channelsData array to fit given length * channels
    // Keeps old data in the process
    private void SN()
    {
        List<Note_Jann>[] temp = new List<Note_Jann>[ch];
        
        for (int y = 0; y < ch; y++)
        {
            temp[y] = new List<Note_Jann>();
            
            for (int x = 0; x < len; x++)
            {
                if (y >= chd.Length || x >= chd[y].Count)
                {
                    temp[y].Add(
                        new Note_Jann(NotesCreator_Jann.Note.None, 
                        0)
                        );
                }
                else
                {
                    temp[y].Add(chd[y][x]);   
                }
            }
        }

        chd = temp;
    }

    private void ST()
    {
        Track_Jann t = new Track_Jann();
        t.n = tit;
        t.b = bpm;
        t.c = len;

        int[] data = new int[ch * len];
        for (int y = 0; y < ch; y++)
        {
            for (int x = 0; x < len; x++)
            {
                data[y * len + x] = chd[y][x].Frequence;
            }
        }

        t.d = data;
        
        string trackJson = JsonUtility.ToJson(t);
        File.WriteAllText(Application.dataPath + "/Audio/" + t.n + ".json", trackJson);
    }

    private void LT(string path)
    {
        string json = File.ReadAllText(path);
        
        Track_Jann t = JsonUtility.FromJson<Track_Jann>(json);
        tit = t.n;
        bpm = t.b;
        len = t.c;
        ch = t.d.Length / len;

        RN();
        
        // float[] data = new float[m_channels * m_length];
        for (int y = 0; y < ch; y++)
        {
            for (int x = 0; x < len; x++)
            {
                int frequency = t.d[y * len + x];
                chd[y][x].Frequence = frequency;
                chd[y][x].MNoteName = nc.GetNote(frequency);
            }
        }
    }
    
    // Resets all notes to None/0f in every channel
    private void RN()
    {
        chd = new List<Note_Jann>[ch];
        
        for (int y = 0; y < ch; y++)
        {
            chd[y] = new List<Note_Jann>();
            
            for (int x = 0; x < len; x++)
            {
                chd[y].Add(new Note_Jann(NotesCreator_Jann.Note.None, 0));
            }
        }
    }
}