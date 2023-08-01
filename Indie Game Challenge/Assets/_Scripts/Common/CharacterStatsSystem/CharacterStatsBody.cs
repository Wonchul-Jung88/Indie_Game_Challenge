using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsBody : MonoBehaviour
{
    public StatsEquipmentSlot[] StatsEquipmentSlots;

    [Header("Stats")]
    public CharacterStat Speed;
    public CharacterStat Stamina;
    public CharacterStat Power;
    public CharacterStat Guts;
    public CharacterStat Intelegence;

    [SerializeField] StatsSaveManager itemSaveManager;

    public void LoadStats()
    {
        if (itemSaveManager != null)
        {
            itemSaveManager.LoadEquipment(this);
        }
    }

    public void SaveStats()
    {
        if (itemSaveManager != null)
        {
            itemSaveManager.SaveEquipment(this);
        }
    }

    public void Equip(EquippableStats stats)
    {
        EquippableStats previousItem;
        if (this.AddStats(stats, out previousItem))
        {
            if (previousItem != null)
            {
                //Inventory.AddItem(previousItem);
                previousItem.Unequip(this);
                //statPanel.UpdateStatValues();
            }
            stats.Equip(this);
            //statPanel.UpdateStatValues();
        }
    }

    public void Unequip(EquippableStats stats)
    {
        if (this.RemoveStats(stats))
        {
            stats.Unequip(this);
            //statPanel.UpdateStatValues();
            //Inventory.AddItem(item);
        }
    }

    public bool AddStats(EquippableStats stats, out EquippableStats previousItem)
    {
        for (int i = 0; i < StatsEquipmentSlots.Length; i++)
        {
            if (StatsEquipmentSlots[i].StatsType == stats.EquipmentType)
            {
                previousItem = (EquippableStats)StatsEquipmentSlots[i].Stats;
                StatsEquipmentSlots[i].Stats = stats;
                StatsEquipmentSlots[i].Amount = 1;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveStats(EquippableStats item)
    {
        for (int i = 0; i < StatsEquipmentSlots.Length; i++)
        {
            if (StatsEquipmentSlots[i].Stats == item)
            {
                StatsEquipmentSlots[i].Stats = null;
                StatsEquipmentSlots[i].Amount = 0;
                return true;
            }
        }
        return false;
    }
}
