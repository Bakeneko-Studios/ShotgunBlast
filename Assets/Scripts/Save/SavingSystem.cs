using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class SavingSystem
{
    public static string path = Application.persistentDataPath + "/user.data";//insert file name in quotes
    public static void SaveUser()
    {
        if(File.Exists(path)) File.Delete(path);

        BinaryFormatter formatter = new BinaryFormatter();
        UTF8Encoding encoder = new UTF8Encoding();

        using (MemoryStream memStream = new MemoryStream())
        {
            SavedData data = new SavedData();
            formatter.Serialize(memStream,data);
            byte[] serialized = memStream.ToArray();
            byte[] encoded = Encoding.UTF8.GetBytes(Convert.ToBase64String(serialized));

            using (FileStream stream = File.Create(path))
            {
                stream.Write(encoded,0,encoded.Length);
            }
        }
    }
    public static void LoadUser()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.OpenRead(path))
            {
                string base64 = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                {
                    base64 = reader.ReadToEnd();
                }
                byte[] decoded = Convert.FromBase64String(base64);

                using(MemoryStream memStream = new MemoryStream(decoded))
                {
                    SavedData data = formatter.Deserialize(memStream) as SavedData;
                    Debug.Log(data);
                    UserSettings.gQualityInt = data.gQualityInt;
                    UserSettings.FOVFlt = data.FOVFlt;
                    UserSettings.healthBarBool = data.healthBarBool;
                    UserSettings.masterVolumeFlt = data.masterVolumeFlt;
                    UserSettings.musicVolumeFlt = data.musicVolumeFlt;
                    UserSettings.effectsVolumeFlt = data.effectsVolumeFlt;
                    UserSettings.sensitivityFlt = data.sensitivityFlt;
                    UserSettings.keybinds = data.inputKeyBinds;

                    Player.ballz = data.ballz;
                }
            }
        }
        else
        {
            Debug.Log("Save file not found in "+ path);
        }
    }
}
