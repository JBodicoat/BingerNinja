using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ElliottDesouza_MainMenu");
    }
}
