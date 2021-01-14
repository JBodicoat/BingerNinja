// Jann

/// Model class for notes

// Jann 21/10/20 - Implementation

public class Note_Jann
{
    private NotesCreator_Jann.Note nn;
    private int f;

    public Note_Jann(NotesCreator_Jann.Note noteName, int frequence)
    {
        nn = noteName;
        f = frequence;
    }

    public NotesCreator_Jann.Note MNoteName
    {
        get => nn;
        set => nn = value;
    }

    public int Frequence
    {
        get => f;
        set => f = value;
    }
}
