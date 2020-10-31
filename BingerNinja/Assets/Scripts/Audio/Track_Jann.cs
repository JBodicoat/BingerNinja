// Jann

/// Model class for tracks
/// Member variable name do not use 'm_' prefix because the name needs
/// to be the same as the key in the json file.
/// Favouring readability over code convention for this

// Jann 25/10/20 - Implementation

[System.Serializable]
public class Track_Jann
{
    public string name;
    public int bpm;
    public int channelLength;
    public float[] data;
    public float[][] frequencies;

    // This is used because JsonUtility doesn't support two-dimensional arrays
    public void GenerateFrequencies()
    {
        int channels = data.Length / channelLength;
        frequencies = new float[channels][];
        
        for (int i = 0; i < channels; i++)
        {
            frequencies[i] = new float[channelLength];
            
            for (int j = 0; j < channelLength; j++)
            {
                frequencies[i][j] = data[i * channelLength + j];
            }
        }
    }
}
