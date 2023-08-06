using System;
using System.Collections.Generic;

[Serializable]
public class StatsSaveData
{
    public StatsType Type;
    public int Amount;

    public StatsSaveData(StatsType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}

[Serializable]
public class StatsContainerSaveData
{
    public List<StatsSaveData> SavedSlots;

    public StatsContainerSaveData()
    {
        SavedSlots = new List<StatsSaveData>();
    }
}
