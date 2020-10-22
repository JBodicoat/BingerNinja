// Jann

/// This script creates notes based this: https://pages.mtu.edu/~suits/NoteFreqCalcs.html

// Jann 16/10/20 - Implemented note calculation
// Jann 21/10/20 - Adjustments

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotesCreator_Jann : MonoBehaviour
{
    private const int BaseFrequency = 440;
    private const int From = -21;
    private const int To = 15;
    private const float FrequencyRation = 1.059463094359f;
    private const int SamplingFrequency = 48000;

    public string m_clipName;
    public int m_channels = 3;
    public float m_currentNote;
    public float m_gain;
    public float m_volume = 0.1f;
    public int m_bpm = 1;

    private int m_position;
    private int m_audioLength;
    private float m_increment;
    private float m_phase;
    private int m_currentNoteIndex;

    private AudioSource m_audioSource;
    private Dictionary<Note, float> notesFrequencies;

    public enum Note
    {
        None = 0, 
        C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3, 
        C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4, 
        C5, Db5, D5, Eb5, E5, F5, Gb5, G5, Ab5, A5, Bb5, B5, 
        C6
    };
    
    public static string[] Notes = new string[]
    {
        "None", 
        "C3", "Db3", "D3", "Eb3", "E3", "F3", "Gb3", "G3", "Ab3", "A3", "Bb3", "B3", 
        "C4", "Db4", "D4", "Eb4", "E4", "F4", "Gb4", "G4", "Ab4", "A4", "Bb4", "B4", 
        "C5", "Db5", "D5", "Eb5", "E5", "F5", "Gb5", "G5", "Ab5", "A5", "Bb5", "B5", 
        "C6"
    };

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        notesFrequencies = new Dictionary<Note, float>();
    }

    void Start()
    {
        List<Note> notes = new List<Note>();
        notes.Add(Note.C3);
        notes.Add(Note.Db3);
        notes.Add(Note.D3);
        foreach (Note note in notes)
        {
            print(note);
        }
        
        m_audioLength = m_bpm / 60;
        if (m_audioLength <= 0)
            m_audioLength = 1;
        
        GenerateNotes();

        AudioClip clip = AudioClip.Create(m_clipName, SamplingFrequency / m_audioLength, m_channels, SamplingFrequency, false, OnAudioRead, OnAudioSetPosition);
        m_audioSource.clip = clip;
        m_audioSource.Play(); 
    }
    
    void Update()
    {
        // TODO: Add volume control
        m_gain = m_volume;

        var curKeyboard = Keyboard.current;
        
        if (curKeyboard.rightArrowKey.wasPressedThisFrame)
        {
            m_currentNoteIndex = (m_currentNoteIndex + 1) % notesFrequencies.Count;
            m_currentNote = notesFrequencies[(Note)m_currentNoteIndex];
            print((Note)m_currentNoteIndex);
        }
        
        if (curKeyboard.leftArrowKey.wasPressedThisFrame)
        {
            m_currentNoteIndex = (m_currentNoteIndex - 1) % notesFrequencies.Count;
        
            if (m_currentNoteIndex < 0)
                m_currentNoteIndex = notesFrequencies.Count - 1;
            
            m_currentNote = notesFrequencies[(Note)m_currentNoteIndex];
            print((Note)m_currentNoteIndex);
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        m_increment = m_currentNote * 2f * Mathf.PI / SamplingFrequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            m_phase += m_increment;
            data[i] = m_gain * Mathf.Sin(m_phase);
            
            if (channels > 1)
            {
                // This copies the data from channel 1 to all following channels
                for(int channel = 1; channel <= channels; channel += channels)
                {
                    data[i + channel] = data[i];
                }
            }

            if (m_phase > Mathf.PI * 2)
            {
                m_phase = 0f;
            }
        }
    }
    
    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * m_currentNote * m_position / SamplingFrequency);
            m_position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)
    {
        m_position = newPosition;
    }

    private void GenerateNotes()
    {
        notesFrequencies.Add(Note.None, 0f);
        
        int numberOfFrequencies = Mathf.Abs(From) + To + 1;
        float[] frequencies = new float[numberOfFrequencies];

        int index = 0;
        for (int frequency = From; frequency < To + 1; frequency++)
        {
            // m_notes[index] = BaseFrequency * Mathf.Pow(FrequencyRation, frequency);
            index++;
            notesFrequencies.Add((Note) index, BaseFrequency * Mathf.Pow(FrequencyRation, frequency));
        }

        m_currentNote = notesFrequencies[(Note)m_currentNoteIndex]; //m_notes[m_currentNoteIndex];
    }
}
