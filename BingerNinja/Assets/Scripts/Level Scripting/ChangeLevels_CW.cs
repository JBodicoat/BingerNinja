using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//07/12/20 alanna & elliot, added Ninja points when you finished level

public class ChangeLevels_CW : MonoBehaviour
{
    public GameObject vendingMachine;
    protected Inventory_JoaoBeijinho m_Inventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_Inventory.GiveItem(ItemType.NinjaPoints, 15);

            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            
            // Assuming the boss is on every third level
            if (currentLevel % 3 == 0 || currentLevel == 20)
            {
                SaveLoadSystem_JamieG.SaveCheckpoint(currentLevel + 1);
            }

            if (currentLevel > 4)
            {
                vendingMachine.SetActive(true);
            }
            else
            {
                ProgressToNextLevel();
            }
        }
    }

    public void ProgressToNextLevel()
    {
        SceneManager_JamieG.Instance.LoadNextLevel();
    }

    private void Awake()
    {
        m_Inventory = FindObjectOfType<Inventory_JoaoBeijinho>();
    }
}
