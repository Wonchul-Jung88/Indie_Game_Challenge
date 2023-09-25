using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatsBody : MonoBehaviour
{
    [Header("Stats")]
    public CharacterStat Speed;
    public CharacterStat Stamina;
    public CharacterStat Power;
    public CharacterStat Guts;
    public CharacterStat Intelligence;

    [SerializeField] StatsSaveManager StatsSaveManager;

    public void LoadStats()
    {
        if (StatsSaveManager != null)
        {
            StatsSaveManager.LoadStats(this);
        }
    }

    public void SaveStats()
    {
        if (StatsSaveManager != null)
        {
            StatsSaveManager.SaveStats(this);
        }
    }

    public void AddSpeed()
    {
        Speed.AddModifier(new StatModifier(10, StatModType.Flat));
    }

    public void AddStamina()
    {
        Stamina.AddModifier(new StatModifier(10, StatModType.Flat));
    }

    public void AddPower()
    {
        Power.AddModifier(new StatModifier(10, StatModType.Flat));
    }

    public void AddGuts()
    {
        Guts.AddModifier(new StatModifier(10, StatModType.Flat));
    }

    public void AddIntelegence()
    {
        Intelligence.AddModifier(new StatModifier(10, StatModType.Flat));
    }
}
