using System;using System.Collections.Generic;using System.IO;using System.Linq;using System.Runtime.Serialization.Formatters.Binary;using UnityEngine;public static class SaveLoadSystem_JamieG{private const string SettingsFile = "/Settings.save";private const string InventoryFile = "/Inventory.save";private const string GameplayFile = "/Gameplay.save";private const string CheckpointFile = "/Checkpoint.save";private static SaveSystemCache m_Cache = new SaveSystemCache();public static void DeleteSaves(){DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);string[] files = new[] {SettingsFile, InventoryFile, GameplayFile, CheckpointFile};foreach(FileInfo fi in dir.GetFiles()){if (files.Contains("/" + fi.Name)){fi.Delete();}}}public static void SaveSettings(SettingsMenu_ElliottDesouza settingsMenu){SettingsData settingsData = new SettingsData(settingsMenu);SaveToFile(SettingsFile, settingsData);}public static void SaveInventory(Inventory_JoaoBeijinho inventory){InventoryData inventoryData = new InventoryData(inventory);SaveToFile(InventoryFile, inventoryData);}public static void SaveGameplay(int currentLevel, GameObject[] enemies, GameObject[] doors){GameplayData gameplayData = new GameplayData(currentLevel, enemies, doors);SaveToFile(GameplayFile, gameplayData);}public static void SaveCheckpoint(int lastCheckpointLevel){CheckpointData checkpointData = new CheckpointData(lastCheckpointLevel);SaveToFile(CheckpointFile, checkpointData);}public static SettingsData LoadSettings(){if (m_Cache.IsCached(SettingsFile)){return (SettingsData) m_Cache.GetData(SettingsFile);}object data = LoadFromFile(SettingsFile);if (data is SettingsData settingsData){if (settingsData.m_chosenLanguage == null){settingsData.m_chosenLanguage = "English";}return settingsData;}return new SettingsData(1f, 1f, "English");}public static InventoryData LoadInventory(){if (m_Cache.IsCached(InventoryFile)){return (InventoryData) m_Cache.GetData(InventoryFile);}object data = LoadFromFile(InventoryFile);if (data is InventoryData inventoryData){return inventoryData;}return default;}public static GameplayData LoadGameplay(){if (m_Cache.IsCached(GameplayFile)){return (GameplayData) m_Cache.GetData(GameplayFile);}object data = LoadFromFile(GameplayFile);if (data is GameplayData gameplayData){return gameplayData;}return default;}public static CheckpointData LoadCheckpoint(){if (m_Cache.IsCached(CheckpointFile)){return (CheckpointData) m_Cache.GetData(CheckpointFile);}object data = LoadFromFile(CheckpointFile);if (data is CheckpointData checkpointData){return checkpointData;}return default;}private static void SaveToFile(string fileName, object data){BinaryFormatter formatter = new BinaryFormatter();string path = Application.persistentDataPath + fileName;FileStream stream = new FileStream(path, FileMode.Create);formatter.Serialize(stream, data);stream.Close();m_Cache.Cache(fileName, data);}public static object LoadFromFile(string filename){string path = Application.persistentDataPath + filename;if (File.Exists(path)){BinaryFormatter formatter = new BinaryFormatter();FileStream stream = new FileStream(path, FileMode.Open);object data = formatter.Deserialize(stream);stream.Close();return data;}else {return null;}}}internal class SaveSystemCache{private List<CacheData> m_cacheData = new List<CacheData>();public void Cache(string type, object data){if (IsCached(type)){int index = m_cacheData.IndexOf(m_cacheData.Find(c => c.type.Equals(type)));m_cacheData[index] = new CacheData(type, data);}else {m_cacheData.Add(new CacheData(type, data));}}public object GetData(string type){return m_cacheData.Find(data => data.type.Equals(type)).data;}public bool IsCached(string type){return m_cacheData.Find(data => data.type.Equals(type)).data != null;}private struct CacheData{public string type;public object data;public CacheData(string type, object data){this.data = data;this.type = type;}}}[Serializable]public struct GameplayData{public int m_currentLevel;public string[] m_enemyIds;public string[] m_doorIds;public GameplayData(int currentLevel, GameObject[] enemies, GameObject[] doors){m_currentLevel = currentLevel;int count = enemies.Count(e => e.activeInHierarchy);m_enemyIds = new string[count];for (int i = 0; i < enemies.Length; i++){if (enemies[i].activeInHierarchy){m_enemyIds[i] = enemies[i].name;}}m_doorIds = new string[doors.Length];for (int i = 0; i < doors.Length; i++){m_doorIds[i] = doors[i].name;}}};[Serializable]public struct CheckpointData{public int m_lastCheckpointLevel;public CheckpointData(int lastCheckpointLevel){m_lastCheckpointLevel = lastCheckpointLevel;}};[Serializable]public struct SettingsData{public float m_musicVolume;public float m_sfxVolume;public string m_chosenLanguage;public SettingsData(SettingsMenu_ElliottDesouza settingsMenu){m_musicVolume = settingsMenu.m_musicSlider.normalizedValue;m_sfxVolume = settingsMenu.m_SFXSlider.normalizedValue;m_chosenLanguage = settingsMenu.m_selectedLanguage;}public SettingsData(float mMusicVolume, float mSfxVolume, string mChosenLanguage){m_musicVolume = mMusicVolume;m_sfxVolume = mSfxVolume;m_chosenLanguage = mChosenLanguage;}};[Serializable]public struct InventoryData{public ItemData[] m_items;public InventoryData(Inventory_JoaoBeijinho inventory){m_items = new ItemData[inventory.m_inventoryItems.Count];int index = 0;foreach (KeyValuePair<ItemType, int> pair in inventory.m_inventoryItems){m_items[index] = new ItemData(pair.Key, pair.Value);index++;}}};[Serializable]public struct ItemData{public ItemType m_type;public int m_amount;public ItemData(ItemType type, int amount){m_type = type;m_amount = amount;}}