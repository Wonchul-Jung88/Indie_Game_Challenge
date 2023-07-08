using System;

[Serializable]
public class GameData
{
    // Singleton instance
    private static GameData instance;

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Load() ?? new GameData();
            }
            return instance;
        }
    }

    public int CoinCount { get; set; }

    private GameData()
    {
        // Initialize properties here.
        CoinCount = 0;
    }

    public void Save()
    {
        SaveSystem.SaveGameData(this);
    }

    private static GameData Load()
    {
        return SaveSystem.LoadGameData();
    }
}
