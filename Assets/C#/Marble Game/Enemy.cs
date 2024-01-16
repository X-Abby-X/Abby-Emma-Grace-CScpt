using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    public Sprite[] spriteArray;

    public Enemy(string colour, int health, int strength) : base(colour, health, strength)
    {

    }

    public override void Attack()
    {
        GameObject character = MarbleGameController.CharacterList[0];
        int damage = this.Strength;
        character.GetComponent<Character>().TakeDamage(damage);
    }

}
