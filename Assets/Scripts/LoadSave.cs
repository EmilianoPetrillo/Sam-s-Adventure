using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public const string SAVE_LOCATION = "/Game.data";

    /// <summary>
    /// BINARY SAVE LOAD
    /// </summary>
    /// <param name="_value"></param>
    public static void SaveData(int _value)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_LOCATION;

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(_value);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + SAVE_LOCATION;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }

    /*
    /// <summary>
    /// JSON SAVE LOAD
    /// </summary>
    /// <param name="_data"></param>
    public static void SaveData(GameData _data)
    {
        string value = JsonUtility.ToJson(_data);
        File.WriteAllText(Application.persistentDataPath + SAVE_LOCATION, value);
    }
    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + SAVE_LOCATION;
        if (File.Exists(path))
        {
            string dataAsJSON = File.ReadAllText(path);
            
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = (GameData)JsonUtility.FromJson(dataAsJSON, typeof(GameData));
            return data;
        }

        Debug.LogError("Error: Save File Not Found in " + path);
        return null;
    }
    */
}
