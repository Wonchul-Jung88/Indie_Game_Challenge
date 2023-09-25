using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsType
{
    Speed,
    Stamina,
    Power,
    Guts,
    Intelegence,
}

[CreateAssetMenu(menuName = "Stats/Equippable Stat")]
public class EquippableStats : Stats
{
    public int SpeedBonus;
    public int StaminaBonus;
    public int PowerBonus;
    public int GutsBonus;
    public int IntelegenceBonus;
    [Space]
    public float SpeedPercentBonus;
    public float StaminaPercentBonus;
    public float PowerPercentBonus;
    public float GutsPercentBonus;
    public float IntelegencePercentBonus;
    [Space]
    public StatsType EquipmentType;

    public override Stats GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(CharacterStatsBody c)
    {
        if (SpeedBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        if (StaminaBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaBonus, StatModType.Flat, this));
        if (PowerBonus != 0)
            c.Power.AddModifier(new StatModifier(PowerBonus, StatModType.Flat, this));
        if (GutsBonus != 0)
            c.Guts.AddModifier(new StatModifier(GutsBonus, StatModType.Flat, this));
        if (IntelegenceBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelegenceBonus, StatModType.Flat, this));

        if (SpeedPercentBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        if (StaminaPercentBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaPercentBonus, StatModType.PercentMult, this));
        if (PowerPercentBonus != 0)
            c.Power.AddModifier(new StatModifier(PowerPercentBonus, StatModType.PercentMult, this));
        if (GutsPercentBonus != 0)
            c.Guts.AddModifier(new StatModifier(GutsPercentBonus, StatModType.PercentMult, this));
        if (IntelegencePercentBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelegencePercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(CharacterStatsBody c)
    {
        c.Speed.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Power.RemoveAllModifiersFromSource(this);
        c.Guts.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(SpeedBonus, "Speed");
        AddStat(StaminaBonus, "Stamina");
        AddStat(PowerBonus, "Power");
        AddStat(GutsBonus, "Guts");
        AddStat(IntelegenceBonus, "Intelegence");

        AddStat(SpeedPercentBonus, "Speed", isPercent: true);
        AddStat(StaminaPercentBonus, "Speed", isPercent: true);
        AddStat(PowerPercentBonus, "Power", isPercent: true);
        AddStat(GutsPercentBonus, "Guts", isPercent: true);
        AddStat(IntelegencePercentBonus, "Intelegence", isPercent: true);

        return sb.ToString();
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
