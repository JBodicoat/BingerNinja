// Jann


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
    public int[] d;
    public int[][] f;

    // This is used because JsonUtility doesn't support two-dimensional arrays
    public void j()
    {
        int ch = d.Length / c;
        f = new int[ch][];
        
        for (int i = 0; i < ch; i++)
        {
            f[i] = new int[c];
            
            for (int j = 0; j < c; j++)
            {
                f[i][j] = d[i * c + j];
            }
        }
    }
}
