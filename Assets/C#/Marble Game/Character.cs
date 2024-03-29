using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class Character : Person
{
    private Player _player;
    [SerializeField] private Ball _ball;

    private bool _bonus = false;
    private bool _applied = false;

    public int MaxHealth;

    public Character(string colour, int health, int strength) : base(colour, health, strength)
    {
        this.MaxHealth = health;
    }

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        ApplyPowerUp();
    }

    void ApplyPowerUp()
    {
        foreach (Item item in _player.SortedBackpack.Keys)
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
                    MarbleGameController.UpdateValues();
                }
            }
        }
    }

    public int CalculateDamage(Dictionary<string, List<GameObject>> SortedMarble)
    {
        int totalMarbleValue = 0;
        foreach (KeyValuePair<string, List<GameObject>> kvp in SortedMarble)
        {
            if (this.Colour == kvp.Key)
            {
                foreach (GameObject marble in kvp.Value)
                {
                    totalMarbleValue += marble.GetComponent<Marble>().Value * 2;
                }
            }
            else
            {
                foreach (GameObject marble in kvp.Value)
                {
                    totalMarbleValue += marble.GetComponent<Marble>().Value;
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
            finalDamage = (CalculateDamage(_ball.SortedMarble) + this.Strength) * base.ColourBonus;
        }
        else
        {

            finalDamage = CalculateDamage(_ball.SortedMarble) + this.Strength;
        }

        targetEnemy.GetComponent<Enemy>().TakeDamage(finalDamage);
        Debug.Log($"targetEnemy health : {targetEnemy.GetComponent<Enemy>().Health}");
    }

    public GameObject ChooseEnemy(Dictionary<string, List<GameObject>> ColourEnemylist)
    {
        foreach (KeyValuePair<string, List<GameObject>> kvp in ColourEnemylist)
        {
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
        MarbleGameController.UpdateValues();
    }

    void ApplyHealthPowerUp()
    {
        foreach (KeyValuePair<Item, int> kvp in _player.SortedBackpack)
        {
            if (kvp.Key.Name == "MaxHealth")
            {
                this.Health = this.MaxHealth;


                _player.Inventory.Remove(kvp.Key);
                if (kvp.Value <= 1)
                {
                    _player.SortedInventory.Remove(kvp.Key);
                }
                else
                {
                    _player.SortedInventory[kvp.Key] -= 1;
                }
               
                _player.Backpack.Remove(kvp.Key);
                if (kvp.Value <= 1)
                {
                    _player.SortedBackpack.Remove(kvp.Key);
                }
                else
                {
                    _player.SortedBackpack[kvp.Key] -= 1;
                }

                _applied = true;
                break;
            }

        }

        MarbleGameController.UpdateValues();
    }

}
