//Joao Beijinho

//Joao Beijinho 06/11/2020 - Created script, GetCurrentClue() and NextClue() functions

using UnityEngine;

/// <summary>
/// This class sets an array of clues and attributes a member of that array as the current clue
/// </summary>
public class ClueSystem_JoaoBeijinho : MonoBehaviour
{
    public string[] m_currentClue = new string[1];
    private string[] m_clues = new string[]{ "Banana", "Apple", "GeT OfF ThE PhoNe" };

    private int m_nextClue = 0;
    private int m_numberOfClues = 2;

    public string GetCurrentClue()
    {
        NextClue();
        print(m_currentClue[0]);
        return m_currentClue[0];
    }

    public void NextClue()
    {
        print(m_nextClue);
        if (m_nextClue <= m_numberOfClues)
        {
            m_currentClue[0] = m_clues[m_nextClue];
            m_nextClue++;
        }
        else if (m_nextClue > m_numberOfClues)
        {
            m_nextClue = 0;
            m_currentClue[0] = m_clues[m_nextClue];
            m_nextClue++;
        }
    }
}
