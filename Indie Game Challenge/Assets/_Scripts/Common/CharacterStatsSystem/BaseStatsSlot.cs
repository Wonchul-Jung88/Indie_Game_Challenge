using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseStatsSlot : MonoBehaviour
{
    protected Stats _stats;
    public Stats Stats
    {
        get { return _stats; }
        set
        {
            _stats = value;
            if (_stats == null && Amount != 0) Amount = 0;
        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && Stats != null) Stats = null;
        }
    }

    public virtual bool CanAddStack(Stats stats, int amount = 1)
    {
        return Stats != null && Stats.ID == stats.ID;
    }

    public virtual bool CanReceiveStats(Stats stats)
    {
        return false;
    }
}
