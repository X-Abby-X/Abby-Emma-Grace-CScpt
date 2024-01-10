using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Character : Person
{
    public int MaxHealth;
    public Ball ball;
    private bool _bonus = false;

    public Character(string colour, int health, int strength) : base(colour, health, strength)
    {
        this.MaxHealth = health;
    }

    public override void Attack()
    {
        int finalDamage = 0;
        GameObject targetEnemy = ChooseEnemy(MarbleGameController.ColourEnemylist);
        Debug.Log($"character colour : {this.Colour} target enemy colour : {targetEnemy.GetComponent<Enemy>().Colour} targetEnemy health : {targetEnemy.GetComponent<Enemy>().Health}");
        if (_bonus)
        {
            finalDamage = (DamageCalculation(ball.SortedMarble) + this.Strength) * base.ColourBonus;
        }
        else
        {

            finalDamage = DamageCalculation(ball.SortedMarble) + this.Strength;
        }

        targetEnemy.GetComponent<Enemy>().TakeDamage(finalDamage);
        Debug.Log($"targetEnemy health : {targetEnemy.GetComponent<Enemy>().Health}");
    }
}
