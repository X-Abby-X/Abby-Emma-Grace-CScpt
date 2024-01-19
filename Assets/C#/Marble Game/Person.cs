using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : MonoBehaviour
{
    public MarbleGameController MarbleGameController;
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
    public virtual void TakeDamage(int damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
        {
            this.Health = 0;
            this.IsDefeated();
        }
        MarbleGameController.UpdateValues();
    }

    public void IsDefeated() {
       
        this.gameObject.SetActive(false);
    }

}
