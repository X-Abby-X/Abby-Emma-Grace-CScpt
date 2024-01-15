using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsItems : Items
{
    public StatsItems(int level, string type, string name, int cost) : base(level, type, name, cost)
    {

    }

    public int Ability(int stats)
    {
        return stats * 2;
    }


}
