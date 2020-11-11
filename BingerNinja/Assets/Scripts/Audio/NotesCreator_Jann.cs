// Jann

/// This script creates notes based on this: https://pages.mtu.edu/~suits/NoteFreqCalcs.html

// Jann 16/10/20 - Implemented note calculation
// Jann 21/10/20 - Adjustments
// Jann 25/10/20 - Refactored parts to PlayTrack_Jann.cs
// Jann 28/10/20 - QA improvements
// Jann 11/11/20 - Added GetNotes(frequency)

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotesCreator_Jann
{
    private const int BaseFrequency = 440;
    private const int From = -21;
    private const int To = 15;
    private const float FrequencyRation = 1.059463094359f;

    private int m_position;
    private int m_audioLength;
    private float m_increment;
    private float m_phase;
    private int m_currentNoteIndex;

    private Dictionary<Note, float> notesFrequencies = new Dictionary<Note, float>();

    public enum Note
    {
        None = 0, 
        C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3, 
        C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4, 
        C5, Db5, D5, Eb5, E5, F5, Gb5, G5, Ab5, A5, Bb5, B5, 
        C6
    };

    public NotesCreator_Jann()
    {
        GenerateNotes();
    }

    public float GetFrequency(Note note)
    {
        return notesFrequencies[note];
    }

    public Note GetNote(float frequency)
    {
        return notesFrequencies.FirstOrDefault(pair => Math.Abs(pair.Value - frequency) < 0.1f).Key;
    }
    
    private void GenerateNotes()
    {
        notesFrequencies.Add(Note.None, 0f);

        int index = 0;
        for (int frequency = From; frequency < To + 1; frequency++)
        {
            index++;
            notesFrequencies.Add((Note) index, BaseFrequency * Mathf.Pow(FrequencyRation, frequency));
        }
    }
}