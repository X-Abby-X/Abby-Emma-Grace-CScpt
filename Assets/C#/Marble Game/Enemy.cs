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

    private void OnDisable()
    {
        if (MarbleGameController.TextList.Count <= 1)
        {
            MarbleGameController.TextList[0].enabled = false;
            MarbleGameController.TextList.RemoveAt(0);
        }
        else
        {
            int index = 0;
            for (int i = 0; i < MarbleGameController.EnemyList.Count; i++)
            {
                if (MarbleGameController.EnemyList[i] == this.gameObject)
                {
                    index = i;
                    break;
                }
            }
            MarbleGameController.TextList[index + 1].enabled = false;
            MarbleGameController.TextList.RemoveAt(index + 1);
        }

        MarbleGameController.EnemyList.Remove(this.gameObject);
        MarbleGameController.ColourEnemylist.Clear();
        MarbleGameController.SortEnemy(MarbleGameController.EnemyList);
    }
}
