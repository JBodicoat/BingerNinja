using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Elliott 05/12/2020 - made it so the game pauses when veningmachine is up 

public class ChangeLevels_CW : MonoBehaviour
{
    public GameObject vendingMachine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            
            // Assuming the boss is on every third level
            if (currentLevel % 3 == 0 || currentLevel == 20)
            {
                SaveLoadSystem_JamieG.SaveCheckpoint(currentLevel + 1);                
            }

            if (currentLevel > 4)
            {
                vendingMachine.SetActive(true);
                Time.timeScale = 0f;
                VendingMachineMenu_Elliott.m_gameIsPaused = true;

            }
            else
            {                
                ProgressToNextLevel();
            }
        }
    }

    public void ProgressToNextLevel()
    {
        Time.timeScale = 1f;
        VendingMachineMenu_Elliott.m_gameIsPaused = false;
        SceneManager_JamieG.Instance.LoadNextLevel();
    }
}
