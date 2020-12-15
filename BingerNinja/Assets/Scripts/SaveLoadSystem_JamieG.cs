//Jamie - This class will save and load game information like settings, upgrades and current checkpoint. Uses alot of temp values until the scripts with the data is actually created

//Jamie - 26/10/20 - First implemented
//Jann  - 04/11/20 - Saving and loading implemented as far as possible with the current dependencies
//Jann  - 08/11/20 - QA improvements
//Jann  - 20/11/20 - Hooked up the settingsmenu
//Jann  - 23/11/20 - QA improvements
//Jann  - 25/11/20 - Caching implemented
//Jann  - 28/11/20 - Added new gameplay data saving

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem_JamieG
{
    private const string SettingsFile = "/Settings.save";
    private const string InventoryFile = "/Inventory.save";
    private const string GameplayFile = "/Gameplay.save";
    private const string CheckpointFile = "/Checkpoint.save";

    private static SaveSystemCache m_Cache = new SaveSystemCache();

    public static void DeleteSaves()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);

        foreach(FileInfo fi in dir.GetFiles())
        {
            fi.Delete();
        }
    }
    
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
    public static void SaveGameplay(int currentLevel, GameObject[] enemies, GameObject[] doors)
    {
        GameplayData gameplayData = new GameplayData(currentLevel, enemies, doors);
        SaveToFile(GameplayFile, gameplayData);
    }
    
    // Saves the current checkpoint (after a boss level)
    public static void SaveCheckpoint(int lastCheckpointLevel)
    {
        CheckpointData checkpointData = new CheckpointData(lastCheckpointLevel);
        SaveToFile(CheckpointFile, checkpointData);
    }

    #endregion

    #region Loading

    // Returns information from the Settings.save file or an empty struct if the file can't be found
    public static SettingsData LoadSettings()
    {
        if (m_Cache.IsCached(SettingsFile))
        {
            return (SettingsData) m_Cache.GetData(SettingsFile);
        }

        object data = LoadFromFile(SettingsFile);
        if (data is SettingsData settingsData)
        {
            if (settingsData.m_chosenLanguage == null)
            {
                settingsData.m_chosenLanguage = "English";
            }

            return settingsData;
        }

        Debug.Log("Loading settings didn't return object of type SettingsData");
        return new SettingsData(1f, 1f, "English");
    }

    // Returns the items from the Inventory.save file or an empty struct if the file can't be found
    public static InventoryData LoadInventory()
    {
        if (m_Cache.IsCached(InventoryFile))
        {
            return (InventoryData) m_Cache.GetData(InventoryFile);
        }
        
        object data = LoadFromFile(InventoryFile);
        if (data is InventoryData inventoryData)
        {
            return inventoryData;
        }

        Debug.Log("Loading inventory didn't return object of type InventoryData");
        return default;
    }

    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static GameplayData LoadGameplay()
    {
        if (m_Cache.IsCached(GameplayFile))
        {
            return (GameplayData) m_Cache.GetData(GameplayFile);
        }
        
        object data = LoadFromFile(GameplayFile);
        if (data is GameplayData gameplayData)
        {
            return gameplayData;
        }

        Debug.Log("Loading gameplayData didn't return object of type GameplayData");
        return default;
    }
    
    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static CheckpointData LoadCheckpoint()
    {
        if (m_Cache.IsCached(CheckpointFile))
        {
            return (CheckpointData) m_Cache.GetData(CheckpointFile);
        }
        
        object data = LoadFromFile(CheckpointFile);
        if (data is CheckpointData checkpointData)
        {
            return checkpointData;
        }

        Debug.Log("Loading checkpointData didn't return object of type CheckpointData");
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

        m_Cache.Cache(fileName, data);
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

            object data = formatter.Deserialize(stream);
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}

internal class SaveSystemCache
{
    private List<CacheData> m_cacheData = new List<CacheData>();

    public void Cache(string type, object data)
    {
        if (IsCached(type))
        {
            int index = m_cacheData.IndexOf(m_cacheData.Find(c => c.type.Equals(type)));
            m_cacheData[index] = new CacheData(type, data);
        }
        else
        {
            m_cacheData.Add(new CacheData(type, data));
        }
    }

    public object GetData(string type)
    {
        return m_cacheData.Find(data => data.type.Equals(type)).data;
    }

    public bool IsCached(string type)
    {
        return m_cacheData.Find(data => data.type.Equals(type)).data != null;
    }

    private struct CacheData
    {
        public string type;
        public object data;

        public CacheData(string type, object data)
        {
            this.data = data;
            this.type = type;
        }
    }
}


#region Serializable data

[Serializable]
public struct GameplayData
{
    public int m_currentLevel;
    public string[] m_enemyIds;
    public string[] m_doorIds;

    public GameplayData(int currentLevel, GameObject[] enemies, GameObject[] doors)
    {
        m_currentLevel = currentLevel;

        int count = enemies.Count(e => e.activeInHierarchy);
        m_enemyIds = new string[count];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeInHierarchy)
            {
                m_enemyIds[i] = enemies[i].name;   
            }
        }
        
        m_doorIds = new string[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            m_doorIds[i] = doors[i].name;
        }
    }
};

[Serializable]
public struct CheckpointData
{
    public int m_lastCheckpointLevel;

    public CheckpointData(int lastCheckpointLevel)
    {
        m_lastCheckpointLevel = lastCheckpointLevel;
    }
};

[Serializable]
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

    public SettingsData(float mMusicVolume, float mSfxVolume, string mChosenLanguage)
    {
        m_musicVolume = mMusicVolume;
        m_sfxVolume = mSfxVolume;
        m_chosenLanguage = mChosenLanguage;
    }
};

[Serializable]
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

[Serializable]
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