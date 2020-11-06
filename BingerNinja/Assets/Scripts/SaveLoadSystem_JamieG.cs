//Jamie - This class will save and load game information like settings, upgrades and current checkpoint. Uses alot of temp values until the scripts with the data is actually created

//Jamie - 26/10/20 - First implemented
//Jann  - 04/11/20 - Saving and loading implemented as far as possible with the current dependencies

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem_JamieG
{
    private const string SettingsFile  = "/Settings.save";
    private const string InventoryFile = "/Inventory.save";
    private const string GameplayFile  = "/Gameplay.save";

    #region Saving
    public static void SaveSettings(SettingsMenu_ElliottDesouza settingsMenu)
    {
        SettingsData settingsData = new SettingsData(settingsMenu);
        SaveToFile(SettingsFile, settingsData);
    }

    public static void SaveInventory(Inventory_JoaoBeijinho inventory)
    {
        InventoryData inventoryData = new InventoryData(inventory);
        SaveToFile(InventoryFile, inventoryData);
    }
    
    public static void SaveGameplay(Vector3 checkpointPosition)
    {
        GameplayData gameplayData = new GameplayData(checkpointPosition);
        SaveToFile(GameplayFile, gameplayData);
    }
    #endregion
    
    #region Loading
    public static SettingsData LoadSettings()
    {
        object data = LoadFromFile(SettingsFile);
        if (data is SettingsData settingsData)
        {
            return settingsData;
        }

        Debug.Log("Loading settings didn't return object of type SettingsData");
        return default;
    }

    public static InventoryData LoadInventory()
    {
        object data = LoadFromFile(InventoryFile);
        if (data is InventoryData inventoryData)
        {
            return inventoryData;
        }
        
        Debug.Log("Loading inventory didn't return object of type InventoryData");
        return default;
    }
    
    public static GameplayData LoadGameplay()
    {
        object data = LoadFromFile(GameplayFile);
        if (data is GameplayData gameplayData)
        {
            return gameplayData;
        }
        
        Debug.Log("Loading gameplayData didn't return object of type GameplayData");
        return default;
    }
    #endregion
    
    private static void SaveToFile(string fileName, object data)
    {
        //Setup formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);
        
        //Save to file
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static object LoadFromFile(string filename)
    {
        string path = Application.persistentDataPath + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            return formatter.Deserialize(stream);
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}

#region Serializable data
[System.Serializable]
public struct GameplayData
{
    public float[] m_checkpointPosition;

    public GameplayData(Vector3 checkpointPosition)
    {
        m_checkpointPosition = new[] {checkpointPosition.x, checkpointPosition.y, checkpointPosition.z};
    }
};

[System.Serializable]
public struct SettingsData
{
    public float m_musicVolume;
    public float m_sfxVolume;
    public string m_chosenLanguage;

    public SettingsData(SettingsMenu_ElliottDesouza settingsMenu)
    {
        m_musicVolume = settingsMenu.m_musicSlider.normalizedValue;
        m_sfxVolume = settingsMenu.m_SFXSlider.normalizedValue;
        m_chosenLanguage = "English";  //TODO: Get value from settings menu when implemented settingsMenu.m_language.value;
    }
};

[System.Serializable]
public struct InventoryData
{
    public ItemData[] m_items;

    public InventoryData(Inventory_JoaoBeijinho inventory) 
    {
        m_items = new ItemData[inventory.m_inventoryItems.Count];

        int index = 0;
        foreach (KeyValuePair<ItemType, int> pair in inventory.m_inventoryItems)
        {
            m_items[index] = new ItemData(pair.Key, pair.Value);
            index++;
        }
    }
};

[System.Serializable]
public struct ItemData
{
    public ItemType m_type;
    public int m_amount;

    public ItemData(ItemType type, int amount)
    {
        m_type = type;
        m_amount = amount;
    }
}

// Removed for now ///////////////////////////
//
// [System.Serializable]
// public struct EffectsData
// {
//     public Effect[] m_effects;
//
//     public EffectsData(EffectManager_MarioFernandes effectManager)
//     {
//         m_effects = new Effect[effectManager.Effects.Count];
//
//         int index = 0;
//         foreach (StatusEffect_MarioFernandes effect in effectManager.Effects)
//         {
//             m_effects[index] = new Effect(effect.GetType().Name, true);
//             index++;
//         }
//     }
// };

// [System.Serializable]
// public struct Effect
// {
//     public string m_effectType;
//     public bool m_active;
//     // public float m_duration;
//     // public float m_speedMultiplier;
//
//
//     public Effect(string effectType, bool isActive)//, float duration, float speedMultiplier)
//     {
//         m_effectType = effectType;
//         m_active = isActive;
//         // m_duration = duration;
//         // m_speedMultiplier = speedMultiplier;
//     }
// };
#endregion