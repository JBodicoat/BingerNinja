//Jamie - This class will save and load game information like settings, upgrades and current checkpoint. Uses alot of temp values until the scripts with the data is actually created

//Jamie - 26/10/20 - First implemented
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

///IMPORTANT - As the other classes and structs havent been made yet/finished, this system uses alot of temporary values to ensure the system actually works,
///until these classes and structs are made by other people. When they are done i will revisit this system and change it so it actually saves the information.

///To save the settings in SettingMenu class - SaveLoadSystem.SaveSetting(this); 
///To save the settings in VendingMachineClass class - SaveLoadSystem.SaveUpgrades(this); 

public static class SaveLoadSystem
{
    public static void SaveSettings()
    {
        //Setup formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SettingSave.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        //Get new values from SettingData struct
        SettingsData SD = new SettingsData(0); //Will take in SettingMenu class when done

        //Save to file
        formatter.Serialize(stream, SD);
        stream.Close();

    }

    public static void SaveUpgrades()
    {
        //Setup formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/UpgradeSave.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        //Get new values from SettingData struct
        SettingsData SU = new SettingsData(0); //Will take in SettingMenu class when done

        //Save to file
        formatter.Serialize(stream, SU);
        stream.Close();
    }

    public static void SaveCheckpointNum()
    {
        int checkpointNum = 1; //TEMP until the checkpoint class is fully made

        //Setup formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/CheckpointSave.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        //Get new values from SettingData struct
        SettingsData SC = new SettingsData(0); //Will take in SettingMenu class when done

        //Save to file
        formatter.Serialize(stream, SC);
        stream.Close();
    }

    public static int GetCurCheckpoint()
    {
        string path = Application.persistentDataPath + "CheckpointSave.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //int CheckpointNum = formatter.Deserialize(stream) as int;
            return 1;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 0;
        }
    }

}

[System.Serializable]
//As the SettingsMenu and VendingMachineMenu scripts havent been made, the system will use temp values. These will be used to ensure the save system actually works until they are made properly
struct SettingsData
{
    public int MusicVolume;
    public int SFXVolume;

    public SettingsData(int temp = 0) //Will take in the SettingsMenu class when it is made 
    {
        //Temp values
        MusicVolume = 40;
        SFXVolume = 30;
    }
};

struct UpgradesData
{
    public int UpgradeType;
    public bool Active;
    public bool ActiveNextLevel;

    public UpgradesData(int temp = 0) //Will take in the VendingMachineMenu class when it is made 
    {
        //Temp values
        UpgradeType = 1;
        Active = true;
        ActiveNextLevel = false;
    }
};
