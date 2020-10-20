using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotesCreator_Jann : MonoBehaviour
{
    private const int BaseFrequency = 440;
    private const int From = -57;
    private const int To = 50;
    private const float FrequencyRation = 1.059463094359f;
    private const float SamplingFrequency = 48000f;

    public float m_currentNote;
    public float m_gain;
    public float m_volume = 0.1f;
    public float[] m_notes;

    private float m_increment;
    private float m_phase;
    private int m_currentNoteIndex;
    
    void Start()
    {
        int numberOfNotes = Mathf.Abs(From) + To + 1;
        m_notes = new float[numberOfNotes];

        int index = 0;
        for (int frequency = From; frequency < To + 1; frequency++)
        {
            m_notes[index] = BaseFrequency * Mathf.Pow(FrequencyRation, frequency); 
            index++;
        }

        m_currentNote = m_notes[m_currentNoteIndex];
    }
    
    void Update()
    {
        // TODO: Add volume control
        m_gain = m_volume;

        var curKeyboard= Keyboard.current;
        
        if (curKeyboard.rightArrowKey.wasPressedThisFrame)
        {
            m_currentNoteIndex = (m_currentNoteIndex + 1) % m_notes.Length;
            m_currentNote = m_notes[m_currentNoteIndex];
            print(m_currentNoteIndex);
        }
        
        if (curKeyboard.leftArrowKey.wasPressedThisFrame)
        {
            m_currentNoteIndex = (m_currentNoteIndex - 1) % m_notes.Length;

            if (m_currentNoteIndex < 0)
                m_currentNoteIndex = m_notes.Length - 1;
            
            m_currentNote = m_notes[m_currentNoteIndex];
            print(m_currentNoteIndex);
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        m_increment = m_currentNote * 2f * Mathf.PI / SamplingFrequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            m_phase += m_increment;
            data[i] = m_gain * Mathf.Sin(m_phase);

            // TODO: Make it channel independent
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (m_phase > Mathf.PI * 2)
            {
                m_phase = 0f;
            }
        }
    }
}
