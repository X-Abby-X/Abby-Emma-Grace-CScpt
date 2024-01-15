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
    public bool _applied = false;

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

    public GameObject ChooseEnemy(Dictionary<string, List<GameObject>> ColourEnemylist)
    {
        foreach (KeyValuePair<string, List<GameObject>> kvp in ColourEnemylist)
        {
            //Debug.Log($"character colour  {this.Colour}, enemy colour {kvp.Key}");
            if (this.Colour == "Red" && kvp.Key == "Yellow")
            {
                _bonus = true;
                return kvp.Value[0];
            }
            else if (this.Colour == "Yellow" && kvp.Key == "Blue")
            {
                _bonus = true;
                return kvp.Value[0];
            }
            else if (this.Colour == "Blue" && kvp.Key == "Red")
            {
                _bonus = true;
                return kvp.Value[0];
            }
            else
            {
                _bonus = false;
            }
        }
        return MarbleGameController.EnemyList[0];
    }

    public override void TakeDamage(int damage)
    {
        this.Health -= damage;
        if (this.Health <= 0)
        {
            ApplyHealthPowerUp();
            if (this._applied == false)
            {
                this.Health = 0;
                this.IsDefeated();
            }
        }
        _applied = false;
        MarbleGameController.updateValues();
    }
}
