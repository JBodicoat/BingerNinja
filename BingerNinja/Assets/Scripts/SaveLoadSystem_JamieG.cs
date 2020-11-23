//Jamie - This class will save and load game information like settings, upgrades and current checkpoint. Uses alot of temp values until the scripts with the data is actually created

//Jamie - 26/10/20 - First implemented
//Jann  - 04/11/20 - Saving and loading implemented as far as possible with the current dependencies
//Jann  - 08/11/20 - QA improvements
//Jann  - 20/11/20 - Hooked up the settingsmenu
//Jann  - 23/11/20 - QA improvements

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
    // Saves the configurations of the settings menu into the Settings.save file 
    public static void SaveSettings(SettingsMenu_ElliottDesouza settingsMenu)
    {
        SettingsData settingsData = new SettingsData(settingsMenu);
        SaveToFile(SettingsFile, settingsData);
    }

    // Saves the current items in the inventory into the Inventory.save file
    public static void SaveInventory(Inventory_JoaoBeijinho inventory)
    {
        InventoryData inventoryData = new InventoryData(inventory);
        SaveToFile(InventoryFile, inventoryData);
    }
    
    // Saves the current state of the game into the Gameplay.save file
    // Only saves the last checkpoint position at the moment
    public static void SaveGameplay(Vector3 checkpointPosition)
    {
        GameplayData gameplayData = new GameplayData(checkpointPosition);
        SaveToFile(GameplayFile, gameplayData);
    }
    #endregion
    
    #region Loading
    // Returns information from the Settings.save file or an empty struct if the file can't be found
    public static SettingsData LoadSettings()
    {
        object data = LoadFromFile(SettingsFile);
        if (data is SettingsData settingsData)
        {
            return settingsData;
        }

        Debug.LogError("Loading settings didn't return object of type SettingsData");
        return default;
    }

    // Returns the items from the Inventory.save file or an empty struct if the file can't be found
    public static InventoryData LoadInventory()
    {
        object data = LoadFromFile(InventoryFile);
        if (data is InventoryData inventoryData)
        {
            return inventoryData;
        }
        
        Debug.LogError("Loading inventory didn't return object of type InventoryData");
        return default;
    }
    
    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static GameplayData LoadGameplay()
    {
        object data = LoadFromFile(GameplayFile);
        if (data is GameplayData gameplayData)
        {
            return gameplayData;
        }
        
        Debug.LogError("Loading gameplayData didn't return object of type GameplayData");
        return default;
    }
    #endregion
    
    /// <summary>
    /// Saves data as file at C:\Users\{user}\AppData\LocalLow\DefaultCompany\BingerNinja
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <param name="data">Data that should be saved (struct from the end of this file)</param>
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
    
    /// <summary>
    /// Loads data from a file from C:\Users\{user}\AppData\LocalLow\DefaultCompany\BingerNinja
    /// </summary>
    /// <param name="filename">Name of the file that should be loaded</param>
    /// <returns>A struct (defined at the end of this file)</returns>
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
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
    public static bool IsFileLocked(string filePath)
    {
        try
        {
            using (File.Open(filePath, FileMode.Open)){}
        }
        catch (IOException e)
        {
            return true;
        }

        return false;
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
        m_chosenLanguage = settingsMenu.m_selectedLanguage;
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
#endregion