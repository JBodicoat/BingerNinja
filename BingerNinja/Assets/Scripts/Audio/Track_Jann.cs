// Jann

using System.Globalization;

/// Model class for tracks
/// Member variable name do not use 'm_' prefix because the name needs
/// to be the same as the key in the json file.
/// Favouring readability over code convention for this

// Jann 25/10/20 - Implementation
// Jann 25/11/20 - Made .json files smaller

[System.Serializable]
public class Track_Jann
{
    public string n;
    public int b;
    public int c;
    public string[] d;
    public float[][] f;

    // This is used because JsonUtility doesn't support two-dimensional arrays
    public void GenerateFrequencies()
    {
        int channels = d.Length / c;
        f = new float[channels][];
        
        for (int i = 0; i < channels; i++)
        {
            f[i] = new float[c];
            
            for (int j = 0; j < c; j++)
            {
                f[i][j] = float.Parse(d[i * c + j], CultureInfo.InvariantCulture);
            }
        }
    }
}
