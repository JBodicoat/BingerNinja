using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Elliott 05/12/2020 - made it so the game pauses when veningmachine is up 

public class ChangeLevels_CW : MonoBehaviour
{
    public GameObject vendingMachine;
    protected Inventory_JoaoBeijinho m_Inventory;

    private void Awake()
    {
        m_Inventory = FindObjectOfType<Inventory_JoaoBeijinho>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex > 4)
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ElliottDesouza_MainMenu");
    }
}
