using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string fileName = "indie_game_challenge.igc";

    public static void SaveGameData(GameData data)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }

            Debug.Log("Game data saved successfully at " + path);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static GameData LoadGameData()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            Debug.Log($"Save file not found in {path}. This could be the first game run.");
            return null;
        }

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                GameData data = (GameData)formatter.Deserialize(stream);
                Debug.Log("Game data loaded successfully from " + path);
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
}
