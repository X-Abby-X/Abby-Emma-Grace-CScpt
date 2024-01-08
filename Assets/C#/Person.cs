using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : MonoBehaviour
{
    public int Health;
    public int Strength;
    public string Colour;
    public int ColourBonus;

    public Person(string colour, int health, int strength)
    {
        this.Colour = colour;
        this.Health = health;
        this.Strength = strength;
    }

    public abstract void Attack();
    public abstract void TakeDamage(int damage);
    public void IsDefeated() { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
