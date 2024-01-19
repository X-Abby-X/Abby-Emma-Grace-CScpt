using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item
{

    public int Level;
    public string Type;
    public string Name;
    public int Cost;

    public Item(int level, string type, string name, int cost)
    {
        this.Name = name;
        this.Level = level;
        this.Type = type;
        this.Cost = cost;
    }
}