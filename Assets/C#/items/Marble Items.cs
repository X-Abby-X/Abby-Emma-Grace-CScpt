using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleItems : Item
{
    public int ColourValue;
    public MarbleItems(int level, string type, string name, int cost, string colour) : base(level, type, name, cost)
    {
        if (colour == "red")
        {
            this.ColourValue = 1;
        }
        else if (colour == "yellow")
        {
            this.ColourValue = 2;
        }
        if (colour == "blue")
        {
            this.ColourValue = 3;
        }

    }

    public float Ability(float stats)
    {
        return stats * 2;
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
