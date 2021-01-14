using UnityEngine;
using UnityEngine.SceneManagement;
// Elliott 05/12/2020 - made it so the game pauses when veningmachine is up 

//07/12/20 alanna & elliot, added Ninja points when you finished level

public class ChangeLevels_CW : MonoBehaviour
{
    public GameObject vendingMachine;
    protected Inventory_JoaoBeijinho m_Inventory;

     void Awake()
    {
        m_Inventory = FindObjectOfType<Inventory_JoaoBeijinho>();
    }
     void OnTriggerEnter2D(Collider2D a)
    {
        if(a.tag == "Player")
        {
            m_Inventory.RG(ItemType.NinjaPoints, 15);

            int b = SceneManager.GetActiveScene().buildIndex;
            
            // Assuming the boss is on every third level
            if (b % 3 == 0 || b == 20)
            {
                SaveLoadSystem_JamieG.QY(b + 1);                
            }

            if (b > 4 && b != 20)
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
        SceneManager_JamieG.Instance.F();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ElliottDesouza_MainMenu");
        SaveLoadSystem_JamieG.QQ();  
    }
}
