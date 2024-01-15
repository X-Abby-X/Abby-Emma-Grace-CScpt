using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using UnityEngine.TextCore.Text;

public class MarbleGameController : MonoBehaviour
{
    // Enemy
    public List<GameObject> EnemyList = new List<GameObject>();
    public GameObject Enemyprefab;
    private int _enemyNum = 3;
    private float[] _enemyX = { -7.03f, -4.46f, -2.15f };
    private float _enemyY = 2.9f;

    //Character
    public GameObject Characterprefeb;
    private float _characterX = -7.84f;
    private float _characterY = -1.73f;
    public int CharaterTotalHp;
    public List<GameObject> CharacterList = new List<GameObject>();

    void SpawnEnemy(int level)
    {
        for (int i = 0; i < _enemyNum; i++)
        {
            int _type = Random.Range(1, 4);

            GameObject newEnemy = (GameObject)Instantiate(Enemyprefab, new Vector3(_enemyX[i], _enemyY, 0), Quaternion.identity);
            if (level == 1)
            {
                newEnemy.GetComponent<Enemy>().Health = 10;
                newEnemy.GetComponent<Enemy>().Strength = 1;
            }
            else if (level == 2)
            {
                newEnemy.GetComponent<Enemy>().Health = 20;
                newEnemy.GetComponent<Enemy>().Strength = 2;
            }
            else if (level == 3)
            {
                newEnemy.GetComponent<Enemy>().Health = 100;
                newEnemy.GetComponent<Enemy>().Strength = 3;
            }

            SpawnTextBox((_enemyX[i] + 1), (_enemyY - 1.79f), newEnemy.GetComponent<Enemy>().Health.ToString());


            if (_type == 1)
            {
                newEnemy.GetComponent<Enemy>().Colour = "Red";
                newEnemy.GetComponent<SpriteRenderer>().sprite = newEnemy.GetComponent<Enemy>().spriteArray[0];
            }
            else if (_type == 2)
            {
                newEnemy.GetComponent<Enemy>().Colour = "Yellow";
                newEnemy.GetComponent<SpriteRenderer>().sprite = newEnemy.GetComponent<Enemy>().spriteArray[1];

            }
            else
            {
                newEnemy.GetComponent<Enemy>().Colour = "Blue";
                newEnemy.GetComponent<SpriteRenderer>().sprite = newEnemy.GetComponent<Enemy>().spriteArray[2];
            }

            EnemyList.Add(newEnemy);
        }
    }

    void SpawnCharacter()
    {
        GameObject newCharacter = (GameObject)Instantiate(Characterprefeb, new Vector3(_characterX, _characterY, 0), Quaternion.identity);

        newCharacter.GetComponent<Character>().Health = 10;
        newCharacter.GetComponent<Character>().Colour = "Red";
        newCharacter.GetComponent<Character>().ColourBonus = 2;
        newCharacter.GetComponent<Character>().MaxHealth = newCharacter.GetComponent<Character>().Health;
        SpawnTextBox((_characterX + 2.82f), (_characterY - 2.1f), CharaterTotalHp.ToString());
        CharacterList.Add(newCharacter);
    }
}
