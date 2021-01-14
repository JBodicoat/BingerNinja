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

        string[] files = new[] {SettingsFile, InventoryFile, GameplayFile, CheckpointFile};
        foreach(FileInfo fi in dir.GetFiles())
        {
            if (files.Contains("/" + fi.Name))
            {
                fi.Delete();   
            }
        }
    }

    // Saves the configurations of the settings menu into the Settings.save file 
    public static void SaveSettings(SettingsMenu_ElliottDesouza settingsMenu)
    {
        o settingsData = new o(settingsMenu);
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

    // Returns information from the Settings.save file or an empty struct if the file can't be found
    public static o i()
    {
        if (m_Cache.IsCached(SettingsFile))
        {
            return (o) m_Cache.GetData(SettingsFile);
        }

        object data = LoadFromFile(SettingsFile);
        if (data is o settingsData)
        {
            if (settingsData.s == null)
            {
                settingsData.s = "English";
            }

            return settingsData;
        }

        return new o(1f, 1f, "English");
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
        return default;
    }



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
public struct o
{
    public float a;
    public float d;
    public string s;

    public o(SettingsMenu_ElliottDesouza settingsMenu)
    {
        a = settingsMenu.m_musicSlider.normalizedValue;
        d = settingsMenu.m_SFXSlider.normalizedValue;
        s = settingsMenu.m_selectedLanguage;
    }

    public o(float mMusicVolume, float mSfxVolume, string mChosenLanguage)
    {
        a = mMusicVolume;
        d = mSfxVolume;
        s = mChosenLanguage;
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
