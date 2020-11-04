//Jann - This class tests the saving and loading functionality

//Jack - 28/10/20 - First implementation
//Jann - 04/11/20 - Feature update / Effects almost implemented but commented out for now

using System;
using System.Collections.Generic;
using UnityEngine;

public class TestingSaveSystem : MonoBehaviour
{
    // Testing for scene manager
    public bool loadMainMenu = false;
    public bool loadNextLevel = false;
    public bool loadCurrentLevel = false;

    // Testing for save system
    // public bool saveEffects = false;
    public bool saveInventory = false;
    public bool saveGameplay = false;
    
    // Testing for load system
    public bool loadSettings = true;
    // public bool loadEffects = false;
    public bool loadInventory = false;
    public bool loadGameplay = false;

    public SettingsMenu_ElliottDesouza settingsMenu;
    // public EffectManager_MarioFernandes effectManager;
    public Inventory_JoaoBeijinho inventory;
    public Transform checkPoint;
    
    void Update()
    {
        #region Scene manager
        // Testing for scene manager
        if (loadMainMenu)
        {
            loadMainMenu = false;
            SceneManager_JamieG.LoadMainMenu();
        }
        else if(loadNextLevel)
        {
            loadNextLevel = false;
            SceneManager_JamieG.LoadNextLevel();
		}
        else if(loadCurrentLevel)
        {
            loadCurrentLevel = false;
            SceneManager_JamieG.LoadCurrentLevel();
		}
        #endregion

        #region Save system
        // Testing for save system
        if(saveGameplay)
        {
            saveGameplay = false;
            SaveLoadSystem_JamieG.SaveGameplay(checkPoint.position);
            Debug.Log("Gameplay saved to " + Application.persistentDataPath);
		}
        else if(saveInventory)
        {
            saveInventory = false;
            inventory.GiveItem(ItemType.LiftKey, 1);
            SaveLoadSystem_JamieG.SaveInventory(inventory);
            Debug.Log("Inventory saved to " + Application.persistentDataPath);
        }
        // else if (saveEffects)
        // {
        //     saveEffects = false;
        //     PoisionDefuff_MarioFernandes poison = new PoisionDefuff_MarioFernandes(10,5,0.5f);
        //     effectManager.AddEffect(poison);
        //     SaveLoadSystem_JamieG.SaveEffects(effectManager);
        //     Debug.Log("Settings saved to " + Application.persistentDataPath);
        // }
        #endregion

        #region Load system
        // Testing for load system
        if (loadSettings)
        {
            loadSettings = false;
            SettingsData settings = SaveLoadSystem_JamieG.LoadSettings();
            settingsMenu.m_musicSlider.value = settings.m_musicVolume;
            settingsMenu.m_SFXSlider.value = settings.m_sfxVolume;
        }
        else if(loadGameplay)
        {
            loadGameplay = false;
            GameplayData gameplayData = SaveLoadSystem_JamieG.LoadGameplay();
            checkPoint.position = new Vector3(
                gameplayData.m_checkpointPosition[0],
                gameplayData.m_checkpointPosition[1],
                gameplayData.m_checkpointPosition[2]);
        }
        else if(loadInventory)
        {
            loadInventory = false;
            InventoryData inventoryData = SaveLoadSystem_JamieG.LoadInventory();
            foreach (ItemData itemData in inventoryData.m_items)
            {
                inventory.GiveItem(itemData.m_type, itemData.m_amount);
            }

            foreach (KeyValuePair<ItemType, int> pair in inventory.m_inventoryItems)
            {
                print(pair.Key.ToString() + " (" + pair.Value + "x) loaded from save system");
            }
        }
        // else if (loadEffects)
        // {
        //     loadEffects = false;
        //     EffectsData effectsData = SaveLoadSystem_JamieG.LoadEffects();
        //     foreach (Effect effect in effectsData.m_effects)
        //     {
        //         if (effect.m_effectType.Equals(nameof(PoisionDefuff_MarioFernandes)))
        //         {
        //             effectManager.AddEffect(new PoisionDefuff_MarioFernandes(1f, 2f));
        //         }
        //     }
        // }
        #endregion
    }

    public void OnSaveSettings() => SaveLoadSystem_JamieG.SaveSettings(settingsMenu);
}
