// Jann

/// Model class for notes

// Jann 21/10/20 - Implementation

public class Note_Jann
{
    private NotesCreator_Jann.Note m_noteName;
    private float m_frequence;

    public Note_Jann(NotesCreator_Jann.Note noteName, float frequence)
    {
        m_noteName = noteName;
        m_frequence = frequence;
    }

    public NotesCreator_Jann.Note MNoteName
    {
        get => m_noteName;
        set => m_noteName = value;
    }

    public float Frequence
    {
        get => m_frequence;
        set => m_frequence = value;
    }
}
