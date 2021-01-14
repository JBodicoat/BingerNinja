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
     const string a = "/Settings.save";
     const string b = "/Inventory.save";
     const string c = "/Gameplay.save";
     const string d = "/Checkpoint.save";

     static SaveSystemCache e = new SaveSystemCache();

    public static void DeleteSaves()
    {
        DirectoryInfo f = new DirectoryInfo(Application.persistentDataPath);

        string[] h = new[] {a, b, c, d};
        foreach(FileInfo fi in f.GetFiles())
        {
            if (h.Contains("/" + fi.Name))
            {
                fi.Delete();   
            }
        }
    }

    // Saves the configurations of the settings menu into the Settings.save file 
    public static void SaveSettings(SettingsMenu_ElliottDesouza g)
    {
        SettingsData l = new SettingsData(g);
        SaveToFile(a, l);
    }

    // Saves the current items in the inventory into the Inventory.save file
    public static void SaveInventory(Inventory_JoaoBeijinho i)
    {
        InventoryData j = new InventoryData(i);
        SaveToFile(b, j);
    }

    // Saves the current state of the game into the Gameplay.save file
    public static void SaveGameplay(int k, GameObject[] m, GameObject[] n)
    {
        GameplayData o = new GameplayData(k, m, n);
        SaveToFile(c, o);
    }
    
    // Saves the current checkpoint (after a boss level)
    public static void SaveCheckpoint(int p)
    {
        CheckpointData q = new CheckpointData(p);
        SaveToFile(d, q);
    }

    // Returns information from the Settings.save file or an empty struct if the file can't be found
    public static SettingsData LoadSettings()
    {
        if (e.IsCached(a))
        {
            return (SettingsData) e.GetData(a);
        }

        object r = LoadFromFile(a);
        if (r is SettingsData s)
        {
            if (s.m_chosenLanguage == null)
            {
                s.m_chosenLanguage = "English";
            }

            return s;
        }

        return new SettingsData(1f, 1f, "English");
    }

    // Returns the items from the Inventory.save file or an empty struct if the file can't be found
    public static InventoryData LoadInventory()
    {
        if (e.IsCached(b))
        {
            return (InventoryData) e.GetData(b);
        }
        
        object t = LoadFromFile(b);
        if (t is InventoryData inventoryData)
        {
            return inventoryData;
        }

        return default;
    }

    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static GameplayData LoadGameplay()
    {
        if (e.IsCached(c))
        {
            return (GameplayData) e.GetData(c);
        }
        
        object u = LoadFromFile(c);
        if (u is GameplayData gameplayData)
        {
            return gameplayData;
        }

        return default;
    }
    
    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static CheckpointData LoadCheckpoint()
    {
        if (e.IsCached(d))
        {
            return (CheckpointData) e.GetData(d);
        }
        
        object v = LoadFromFile(d);
        if (v is CheckpointData checkpointData)
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
     static void SaveToFile(string fileName, object data)
    {
        //Setup formatter
        BinaryFormatter w = new BinaryFormatter();
        string x = Application.persistentDataPath + fileName;
        FileStream y = new FileStream(x, FileMode.Create);

        //Save to file
        w.Serialize(y, data);
        y.Close();

        e.Cache(fileName, data);
    }

    /// <summary>
    /// Loads data from a file from C:\Users\{user}\AppData\LocalLow\DefaultCompany\BingerNinja
    /// </summary>
    /// <param name="C">Name of the file that should be loaded</param>
    /// <returns>A struct (defined at the end of this file)</returns>
    public static object LoadFromFile(string C)
    {
        string z = Application.persistentDataPath + C;
        if (File.Exists(z))
        {
            BinaryFormatter A = new BinaryFormatter();
            FileStream B = new FileStream(z, FileMode.Open);

            object D = A.Deserialize(B);
            B.Close();
            
            return D;
        }
        else
        {
            return null;
        }
    }
}

internal class SaveSystemCache
{
     List<CacheData> E = new List<CacheData>();

    public void Cache(string F, object G)
    {
        if (IsCached(F))
        {
            int H = E.IndexOf(E.Find(c => c.type.Equals(F)));
            E[H] = new CacheData(F, G);
        }
        else
        {
            E.Add(new CacheData(F, G));
        }
    }

    public object GetData(string I)
    {
        return E.Find(data => data.type.Equals(I)).data;
    }

    public bool IsCached(string J)
    {
        return E.Find(data => data.type.Equals(J)).data != null;
    }

     struct CacheData
    {
        public string type;
        public object data;

        public CacheData(string K, object L)
        {
            this.data = L;
            this.type = K;
        }
    }
}


[Serializable]
public struct GameplayData
{
    public int m_currentLevel;
    public string[] m_enemyIds;
    public string[] m_doorIds;

    public GameplayData(int M, GameObject[] N, GameObject[] O)
    {
        m_currentLevel = M;

        int P = N.Count(e => e.activeInHierarchy);
        m_enemyIds = new string[P];
        for (int i = 0; i < N.Length; i++)
        {
            if (N[i].activeInHierarchy)
            {
                m_enemyIds[i] = N[i].name;   
            }
        }
        
        m_doorIds = new string[O.Length];
        for (int i = 0; i < O.Length; i++)
        {
            m_doorIds[i] = O[i].name;
        }
    }
};

[Serializable]
public struct CheckpointData
{
    public int m_lastCheckpointLevel;

    public CheckpointData(int P)
    {
        m_lastCheckpointLevel = P;
    }
};

[Serializable]
public struct SettingsData
{
    public float m_musicVolume;
    public float m_sfxVolume;
    public string m_chosenLanguage;

    public SettingsData(SettingsMenu_ElliottDesouza Q)
    {
        m_musicVolume = Q.m_musicSlider.normalizedValue;
        m_sfxVolume = Q.m_SFXSlider.normalizedValue;
        m_chosenLanguage = Q.m_selectedLanguage;
    }

    public SettingsData(float R, float S, string T)
    {
        m_musicVolume = R;
        m_sfxVolume = S;
        m_chosenLanguage = T;
    }
};

[Serializable]
public struct InventoryData
{
    public ItemData[] m_items;

    public InventoryData(Inventory_JoaoBeijinho U)
    {
        m_items = new ItemData[U.m_inventoryItems.Count];

        int V = 0;
        foreach (KeyValuePair<ItemType, int> pair in U.m_inventoryItems)
        {
            m_items[V] = new ItemData(pair.Key, pair.Value);
            V++;
        }
    }
};

[Serializable]
public struct ItemData
{
    public ItemType m_type;
    public int m_amount;

    public ItemData(ItemType W, int U)
    {
        m_type = W;
        m_amount = U;
    }
}
