using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Items
{

    public int Level;
    public string Type;
    public string Name;
    public int Cost;

    public Items(int level, string type, string name, int cost)
    {
        this.Name = name;
        this.Level = level;
        this.Type = type;
        this.Cost = cost;
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}
