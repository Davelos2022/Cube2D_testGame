using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadSave
{
    public static string path = Application.persistentDataPath + ".json";
    public static JsonData jsonData;

    public static void LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            jsonData = JsonUtility.FromJson<JsonData>(json);
        }
        else
            return;
    }

    public static void SaveGame(int level, float volume, List<int> indexPosition)
    {
        jsonData = new JsonData { Level = level, Volume = volume, IndexPosition = indexPosition };

        string json = JsonUtility.ToJson(jsonData);
        File.WriteAllText(path, json);
    }
}
