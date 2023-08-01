using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEquipmentSlot : StatsSlot
{
    public StatsType StatsType;

    //protected override void OnValidate()
    //{
    //    base.OnValidate();
    //    gameObject.name = StatsType.ToString() + " Slot";
    //}

    public override bool CanReceiveStats(Stats stats)
    {
        if (stats == null)
            return true;

        EquippableStats equippableStats = stats as EquippableStats;
        return equippableStats != null && equippableStats.EquipmentType == StatsType;
    }
}
