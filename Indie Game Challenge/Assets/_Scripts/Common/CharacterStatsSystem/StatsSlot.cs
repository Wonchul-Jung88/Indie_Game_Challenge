using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatsSlot : BaseStatsSlot
{
    public override bool CanAddStack(Stats stats, int amount = 1)
    {
        return base.CanAddStack(stats, amount) && Amount + amount <= stats.MaximumStacks;
    }

    public override bool CanReceiveStats(Stats stats)
    {
        return true;
    }
}
