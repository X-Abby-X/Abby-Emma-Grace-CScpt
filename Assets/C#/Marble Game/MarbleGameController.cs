using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using UnityEngine.TextCore.Text;
using static System.Net.Mime.MediaTypeNames;

public class MarbleGameController : MonoBehaviour
{
    // Enemy
    public List<GameObject> EnemyList = new List<GameObject>();
    public GameObject Enemyprefab;
    private int _enemyNum = 3;
    private float[] _enemyX = { -7.03f, -4.46f, -2.15f };
    private float _enemyY = 2.9f;
    private int _enemyTotalHp;
    public Dictionary<string, List<GameObject>> ColourEnemylist = new Dictionary<string, List<GameObject>>();

    // Character
    public GameObject Characterprefeb;
    private float _characterX = -7.84f;
    private float _characterY = -1.73f;
    public int CharaterTotalHp;
    public List<GameObject> CharacterList = new List<GameObject>();

    // Text box
    public GameObject canvas;
    public TMP_Text text;
    public List<TMP_Text> TextList = new List<TMP_Text>();

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

    public int CheckHealth(List<GameObject> List)
    {
        int _totalHp = 0;
        foreach (GameObject person in List)
        {
            _totalHp += person.GetComponent<Person>().Health;
        }
        return _totalHp;
    }

    public void SortEnemy(List<GameObject> EnemyList)
    {
        // put enemy into their colour
        foreach (GameObject enemy in EnemyList)
        {
            if (ColourEnemylist.ContainsKey(enemy.GetComponent<Enemy>().Colour))
            {
                ColourEnemylist[enemy.GetComponent<Enemy>().Colour].Add(enemy);
            }
            else
            {
                ColourEnemylist.Add(enemy.GetComponent<Enemy>().Colour, new List<GameObject> { enemy });
            }
        }

        foreach (KeyValuePair<string, List<GameObject>> kvp in ColourEnemylist)
        {
            SortEnemyByHp(kvp.Value);
        }
    }

    public void SortEnemyByHp(List<GameObject> Enemylist)
    {
        int numLoops = Enemylist.Count;
        for (int j = 0; j < Enemylist.Count - 1; j++)
        {
            bool sorted = true;
            for (int i = 0; i < numLoops - 1; i++)
            {
                if (Enemylist[i].GetComponent<Enemy>().Health > Enemylist[i + 1].GetComponent<Enemy>().Health)
                {
                    GameObject temp = Enemylist[i];
                    Enemylist[i] = Enemylist[i + 1];
                    Enemylist[i + 1] = temp;
                    sorted = false;
                }
            }
            numLoops -= 1;
            if (sorted)
            {
                break;
            }
        }
    }

    void SpawnTextBox(float x, float y, string content)
    {
        TMP_Text newtext = (TMP_Text)Instantiate(text, new Vector3(x, y, 0), Quaternion.identity);
        newtext.GetComponent<TMP_Text>().text = content;
        newtext.transform.SetParent(canvas.transform);
        TextList.Add(newtext);
    }

    public void updateValues()
    {
        CharaterTotalHp = CheckHealth(CharacterList);
        _enemyTotalHp = CheckHealth(EnemyList);
        TextList[0].GetComponent<TMP_Text>().text = CharaterTotalHp.ToString();

        for (int i = 0; i < EnemyList.Count; i++)
        {
            TextList[i + 1].GetComponent<TMP_Text>().text = EnemyList[i].GetComponent<Enemy>().Health.ToString();
        }
    }
}
