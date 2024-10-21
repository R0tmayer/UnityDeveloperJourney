using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        var stream = new FileStream(path, FileMode.Create);

        var data = new PlayerProgressProvider();

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save file in " + path);
    }

    public static PlayerProgressProvider LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as PlayerProgressProvider;
            stream.Close();

            return data;
        }

        Debug.LogError("Save file non found in " + path);
        return null;
    }
}