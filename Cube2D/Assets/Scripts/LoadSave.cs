using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadSave
{
    public static string path = Application.persistentDataPath;
    public static string nameFile = "Save.json";
    public static JsonData jsonData;

    public static bool CheckFileAndLoad()
    {
        string filePath = path + nameFile;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            jsonData = JsonUtility.FromJson<JsonData>(json);
            return true;
        }
        else
            return false;
    }

    public static void SaveGame(int level, float volume, List<int> indexPosition)
    {
        jsonData = new JsonData { Level = level, Volume = volume, IndexPosition = indexPosition };

        string json = JsonUtility.ToJson(jsonData);


        File.WriteAllText(path + nameFile, json);
    }
}
