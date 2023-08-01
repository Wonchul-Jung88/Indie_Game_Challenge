using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSaveManager : MonoBehaviour
{
    [SerializeField] StatsDatabase statsDatabase;

    //private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";

    //public void LoadInventory(CharacterStatsBody character)
    //{
    //    ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
    //    if (savedSlots == null) return;
    //    //character.Inventory.Clear();

    //    for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
    //    {
    //        ItemSlot itemSlot = character.Inventory.ItemSlots[i];
    //        ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

    //        if (savedSlot == null)
    //        {
    //            itemSlot.Item = null;
    //            itemSlot.Amount = 0;
    //        }
    //        else
    //        {
    //            itemSlot.Item = statsDatabase.GetStatCopy(savedSlot.ItemID);
    //            itemSlot.Amount = savedSlot.Amount;
    //        }
    //    }
    //}

    public void LoadEquipment(CharacterStatsBody character)
    {
        StatsContainerSaveData savedSlots = ItemSaveIO.LoadStats(EquipmentFileName);
        if (savedSlots == null) return;

        foreach (StatsSlotSaveData savedSlot in savedSlots.SavedSlots)
        {
            if (savedSlot == null)
            {
                continue;
            }

            Stats item = statsDatabase.GetStatCopy(savedSlot.StatID);
            //character.Inventory.AddItem(item);
            character.Equip((EquippableStats)item);
        }
    }

    public void SaveInventory(CharacterStatsBody character)
    {
        //SaveItems(character.Inventory.ItemSlots, InventoryFileName);
    }

    public void SaveEquipment(CharacterStatsBody character)
    {
        SaveStats(character.StatsEquipmentSlots, EquipmentFileName);
    }

    private void SaveStats(IList<StatsSlot> statsSlots, string fileName)
    {
        var saveData = new ItemContainerSaveData(statsSlots.Count);

        for (int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            StatsSlot statsSlot = statsSlots[i];

            if (statsSlot.Stats == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemSlotSaveData(statsSlot.Stats.ID, statsSlot.Amount);
            }
        }

        ItemSaveIO.SaveItems(saveData, fileName);
    }
}
