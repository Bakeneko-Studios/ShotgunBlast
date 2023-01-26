using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    public static string path = Application.persistentDataPath + "/user.data";//insert file name in quotes
    public static void SaveUser()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);
        SavedData data = new SavedData();

        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static void LoadUser()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SavedData data = formatter.Deserialize(stream) as SavedData;

            UserSettings.gQualityInt = data.gQualityInt;
            UserSettings.FOVFlt = data.FOVFlt;
            UserSettings.healthBarBool = data.healthBarBool;
            UserSettings.masterVolumeFlt = data.masterVolumeFlt;
            UserSettings.musicVolumeFlt = data.musicVolumeFlt;
            UserSettings.effectsVolumeFlt = data.effectsVolumeFlt;
            UserSettings.sensitivityFlt = data.sensitivityFlt;
            UserSettings.keybinds = data.inputKeyBinds;

            stream.Close();
        }
        else
        {
            Debug.Log("Save file not found in "+ path);
        }
    }
}
