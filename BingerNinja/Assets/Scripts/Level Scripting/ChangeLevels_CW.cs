using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevels_CW : MonoBehaviour
{
    public GameObject vendingMachine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player" && SceneManager.GetActiveScene().buildIndex > 4)
        {
            vendingMachine.SetActive(true);
        }
        else
        {
            ProgressToNextLevel();
        }
    }

    public void ProgressToNextLevel()
    {
        SceneManager_JamieG.Instance.LoadNextLevel();
    }
}
