using System;

[Serializable]
public class StatsSlotSaveData
{
    public string StatID;
    public int Amount;

    public StatsSlotSaveData(string id, int amount)
    {
        StatID = id;
        Amount = amount;
    }
}

[Serializable]
public class StatsContainerSaveData
{
    public StatsSlotSaveData[] SavedSlots;

    public StatsContainerSaveData(int numStats)
    {
        SavedSlots = new StatsSlotSaveData[numStats];
    }
}
