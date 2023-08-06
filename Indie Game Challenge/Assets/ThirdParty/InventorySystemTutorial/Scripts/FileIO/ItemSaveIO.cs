using UnityEngine;

public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

    public static void SaveItems(ItemContainerSaveData saveData, string path)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + path + ".dat", saveData);
    }

    public static void SaveStats(StatsContainerSaveData saveData, string path)
	{
		FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + path + ".dat", saveData);
	}

	public static ItemContainerSaveData LoadItems(string path)
	{
		string filePath = baseSavePath + "/" + path + ".dat";

		if (System.IO.File.Exists(filePath))
		{
			return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
		}
		return null;
	}

    public static StatsContainerSaveData LoadStats(string path)
    {
        string filePath = baseSavePath + "/" + path + ".dat";

        if (System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<StatsContainerSaveData>(filePath);
        }
        return null;
    }
}
