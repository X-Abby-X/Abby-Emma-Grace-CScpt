using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Character : Person
{
    public int MaxHealth;
    public Player player;
    public Ball ball;
    private bool _bonus = false;
    public bool _applied = false;

    public Character(string colour, int health, int strength) : base(colour, health, strength)
    {
        this.MaxHealth = health;
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        ApplyPowerUp();
    }

    void ApplyPowerUp()
    {
        foreach (Item item in player.SortedBackpack.Keys)
        {
            if (item is StatsItems)
            {
                StatsItems powerup = (StatsItems)item;
                if (powerup.Type == "strength")
                {
                    this.Strength = powerup.Ability(this.Strength);
                    Debug.Log(this.Strength);
                }
                else if (powerup.Type == "hp")
                {
                    this.Health = powerup.Ability(this.Health);
                    MaxHealth = this.Health;
                    MarbleGameController.updateValues();
                }
            }
        }
    }

    public int DamageCalculation(Dictionary<string, List<GameObject>> SortedMarble)
    {
        int totalMarbleValue = 0;
        foreach (KeyValuePair<string, List<GameObject>> kvp in SortedMarble)
        {
            if (this.Colour == kvp.Key)
            {
                foreach (GameObject marble in kvp.Value)
                {
                    totalMarbleValue += marble.GetComponent<Marble>()._value * 2;
                }
            }
            else
            {
                foreach (GameObject marble in kvp.Value)
                {
                    totalMarbleValue += marble.GetComponent<Marble>()._value;
                }
            }
        }
        return totalMarbleValue;
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

    void ApplyHealthPowerUp()
    {
        Debug.Log("ApplyPowerUp method start");
        foreach (KeyValuePair<Item, int> kvp in player.SortedBackpack)
        {
            if (kvp.Key is OtherItems)
            {
                Debug.Log("found OtherItems");
                OtherItems powerup = (OtherItems)kvp.Key;
                if (powerup.Type == "Hp")
                {
                    Debug.Log("found Hp");
                    if (kvp.Value >= 1)
                    {
                        Debug.Log(this.MaxHealth);
                        this.Health = this.MaxHealth;
                        _applied = true;
                        player.SortedInventory[kvp.Key] -= 1;
                    }

                    if (kvp.Value <= 1)
                    {
                        player.Inventory.Remove(kvp.Key);
                        player.SortedInventory.Remove(kvp.Key);
                        player.SortItemList(player.Inventory, player.SortedInventory);

                        player.Backpack.Remove(kvp.Key);
                        player.SortedBackpack.Remove(kvp.Key);
                        player.SortItemList(player.Inventory, player.SortedBackpack);
                    }
                    break;
                }
            }
        }

        MarbleGameController.updateValues();
    }
}
