using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    public static string path = Application.persistentDataPath + "/user.data";//insert file name in quotes
    public static void SaveUser (UserSettings user)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);
        SavedData data = new SavedData(user);

        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static SavedData LoadUser ()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;
            stream.Close();
            return data;
        }

        else
        {
            Debug.Log("Save file not found in "+ path);
            return null;
        }
    }
}