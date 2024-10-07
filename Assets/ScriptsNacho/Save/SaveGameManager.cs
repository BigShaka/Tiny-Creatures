using System.IO;
using UnityEngine;

public static class SaveGameManager
{
    public static SaveData CurrentSaveData = new SaveData();

    public const string DirectoryName = "SaveData";
    public const string FileName = "SaveData.json";

    public static void SaveGame()
    {
        string dir = Path.Combine(Application.persistentDataPath, DirectoryName);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(CurrentSaveData);

        string pathWithFileName = Path.Combine(dir, FileName);
        File.WriteAllText(pathWithFileName, json);

        Debug.Log("Guardado en " + dir);
    }

    public static void LoadGame()
    {
        string pathWithFileName = Path.Combine(Application.persistentDataPath,DirectoryName,FileName);

        if (File.Exists(pathWithFileName))
        {
            string json = File.ReadAllText(pathWithFileName);
            CurrentSaveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("No se encontro  el archivo de guardado");
            CurrentSaveData = new SaveData();
        }
    }
}
