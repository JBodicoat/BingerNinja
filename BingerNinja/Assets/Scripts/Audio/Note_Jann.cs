﻿// Jann

/// Model class for notes

// Jann 21/10/20 - Implementation

public class Note_Jann
{
    private NotesCreator_Jann.Note nn;
    private int f;

    public Note_Jann(NotesCreator_Jann.Note non, int fr)
    {
        nn = non;
        f = fr;
    }

    public NotesCreator_Jann.Note NM
    {
        get => nn;
        set => nn = value;
    }

    public int F
    {
        get => f;
        set => f = value;
    }
}
