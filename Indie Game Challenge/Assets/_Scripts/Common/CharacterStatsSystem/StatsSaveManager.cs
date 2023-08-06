using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StatsSaveManager : MonoBehaviour
{
    private const string StatsFileName = "Stats";

    public void LoadStats(CharacterStatsBody character)
    {
        StatsContainerSaveData saveData = ItemSaveIO.LoadStats(StatsFileName);
        if (saveData == null) return;

        saveData.SavedSlots.ForEach(slot =>
        {
            if (slot.Type == StatsType.Speed) {
                character.Speed.AddModifier(new Kryz.CharacterStats.StatModifier(10, Kryz.CharacterStats.StatModType.Flat));
            }
        });
    }

    public void SaveItems(CharacterStatsBody character)
    {
        SaveStats(character, StatsFileName);
    }

    public void SaveStats(CharacterStatsBody character)
    {
        SaveStats(character, StatsFileName);
    }

    public void SaveStats(CharacterStatsBody character, string fileName)
    {
        var totalCount = character.Speed.StatModifiers.Count + character.Stamina.StatModifiers.Count + character.Power.StatModifiers.Count + character.Guts.StatModifiers.Count + character.Intelegence.StatModifiers.Count;
        var saveData = new StatsContainerSaveData();

        character.Speed.StatModifiers.ToList().ForEach(mod =>
        {
            saveData.SavedSlots.Add(new StatsSaveData(StatsType.Speed, 1));
        });

        ItemSaveIO.SaveStats(saveData, fileName);
    }
}
