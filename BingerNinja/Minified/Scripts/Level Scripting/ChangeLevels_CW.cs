using UnityEngine;using UnityEngine.SceneManagement;public class ChangeLevels_CW : M{protected Inventory_JoaoBeijinho m_Inventory;void Awake(){m_Inventory = FOT<Inventory_JoaoBeijinho>();}void OnTriggerEnter2D(Collider2D a){if(a.tag == "Player"){m_Inventory.RG(ItemType.NinjaPoints, 15);int b = SceneManager.GetActiveScene().buildIndex;if (b % 3 == 0 || b == 20){SaveLoadSystem_JamieG.QY(b + 1);}ProgressToNextLevel();}}public void ProgressToNextLevel(){Time.timeScale = 1f;SceneManager_JamieG.Instance.F();}public void ReturnToMenu(){SceneManager.LoadScene("ElliottDesouza_MainMenu");SaveLoadSystem_JamieG.QQ();}}