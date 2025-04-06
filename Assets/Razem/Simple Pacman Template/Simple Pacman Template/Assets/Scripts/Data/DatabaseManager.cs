using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DatabaseManager
{
    public static void Save(string dataName, object data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = $"{Application.persistentDataPath}/{dataName}.dat";

        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            bf.Serialize(file, data);
            Debug.Log("Data saved");
            file.Close();
        }
        else
        {
            FileStream file = File.Create(path);
            Debug.Log("Data saved");

            bf.Serialize(file, data);
        }
    }

    public static object Load(string dataName)
    {
        string path = $"{Application.persistentDataPath}/{dataName}.dat";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            object dataObject = bf.Deserialize(file);
            file.Close();
            Debug.Log("Successfull Load Data");
            return dataObject;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteSave(string dataName)
    {
        string path = $"{Application.persistentDataPath}/{dataName}.dat";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Data deleted");
        }
    }
}
