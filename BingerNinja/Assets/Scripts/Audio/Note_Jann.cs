// Jann

/// Model class for notes

// Jann 21/10/20 - Implementation

public class Note_Jann
{
    public enum NoteName
    {
        None = 0, 
        C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3, 
        C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4, 
        C5, Db5, D5, Eb5, E5, F5, Gb5, G5, Ab5, A5, Bb5, B5, 
        C6
    };

    private NoteName m_noteName;
    private float m_frequence;

    public Note_Jann(NoteName noteName, float frequence)
    {
        m_noteName = noteName;
        m_frequence = frequence;
    }

    public NoteName MNoteName
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
