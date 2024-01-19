using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class MarbleGameController : MonoBehaviour
{
    // Enemy
    private int _enemyNum = 3;
    private float[] _enemyX = { -7.03f, -4.46f, -2.15f };
    private float _enemyY = 2.9f;
    private int _enemyTotalHp;
    public GameObject Enemyprefab;
    public List<GameObject> EnemyList = new List<GameObject>();
    
    public Dictionary<string, List<GameObject>> ColourEnemylist = new Dictionary<string, List<GameObject>>();

    // Character
    private float _characterX = -7.84f;
    private float _characterY = -1.73f;
    public int CharaterTotalHp;
    public GameObject Characterprefeb;
    public List<GameObject> CharacterList = new List<GameObject>();

    // Text box
    public GameObject canvas;
    public TMP_Text text;
    public List<TMP_Text> TextList = new List<TMP_Text>();

    // Marbles
    public GameObject Marbleprefab;
    public List<GameObject> MarbleList = new List<GameObject>();
    private float _marbleX;
    private float _marbleY;
    private int _marbleNum = 10;
    private bool randomColour = true;
    private int _colourType = 0;

    // Game control
    public Player player;
    public GameObject pauseMenu;
    public bool GameStart = false;
    public static int level = 0;
    public static bool Win;
    public static int xpEarned = 0;
    public static int MoneyEarned = 0;
    public static bool GamePaused = false;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Quit()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Win = false;
        GameStart = false;
        player.GetStats();
        SceneManager.LoadScene("Result");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }


    IEnumerator Start()
    {
        pauseMenu.SetActive(false);
        SpawnCharacter();
        SpawnEnemy(level);
        SortEnemy(EnemyList);
        updateValues();
        ApplyMarbleColourPowerUp();

        GameSequence();
        yield return new WaitForSeconds(0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            
        }
    }

    void GameSequence()
    {
        for (int i = 0; i < MarbleList.Count; i++)
        {
            Destroy(MarbleList[i]);
        }

        MarbleList.Clear();
        SpawnMarble();
        CharacterList[0].GetComponent<Character>().Strength = player.Level;

        GameStart = true;


        if (CharaterTotalHp == 0 || _enemyTotalHp == 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (CharaterTotalHp != 0)
        {
            Win = true;
            if (level == 1)
            {
                xpEarned = 10;
                MoneyEarned = 10;
            }
            else if (level == 2)
            {
                xpEarned = 25;
                MoneyEarned = 25;
            }
            else if (level == 3)
            {
                xpEarned = 40;
                MoneyEarned = 50;
            }
            player.Xp += xpEarned;
            player.Money += MoneyEarned;
            player.Save();
            player.levelUp();
        }
        else
        {
            Win = false;
        }

        GameStart = false;
        player.GetStats();
        SceneManager.LoadScene("Result");
    }

    public IEnumerator AttackGameSequence()
    {
        foreach (GameObject character in CharacterList)
        {
            character.GetComponent<Character>().Attack();
        }
        updateValues();
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject enemy in EnemyList)
        {
            enemy.GetComponent<Enemy>().Attack();
        }
        updateValues();
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"enemy health {_enemyTotalHp}, character health {CharaterTotalHp}");

        GameSequence();
    }

    void ApplyMarbleColourPowerUp()
    {
        Debug.Log("ApplyPowerUp method start");
        foreach (KeyValuePair<Item, int> kvp in player.SortedBackpack)
        {
            if (kvp.Key is MarbleItems)
            {
                Debug.Log("found MarbleItems");
                MarbleItems powerup = (MarbleItems)kvp.Key;
                if (powerup.Type == "colour")
                {
                    if (kvp.Value >= 1)
                    {
                        Debug.Log("found colour");
                        _colourType = powerup.ColourValue;
                        player.SortedInventory[kvp.Key] -= 1;
                        Debug.Log(kvp.Value);
                        randomColour = false;
                    }

                    if (kvp.Value <= 1)
                    {
                        player.Inventory.Remove(kvp.Key);
                        player.SortedInventory.Remove(kvp.Key);
                        player.SortItemList(player.Inventory, player.SortedInventory);

                    }
                    break;
                }
            }

        }
    }

    void SpawnMarble()
    {
        float _localScale = 0.08f;
        float _radius = 1.1f;

        foreach (KeyValuePair<Item, int> kvp in player.SortedBackpack)
        {
            if (kvp.Key is MarbleItems)
            {
                MarbleItems powerup = (MarbleItems)kvp.Key;
                if (powerup.Type == "size")
                {
                    Debug.Log("found size");
                    _localScale = powerup.Ability(_localScale);
                    _radius = 3.14f;
                }

            }
        }

        for (int i = 0; i < _marbleNum; i++)
        {
            if (randomColour)
            {
                _colourType = Random.Range(1, 4);
            }

            _marbleX = Random.Range(0.4f, 8);
            _marbleY = Random.Range(-4, 4);

            GameObject newMarble = (GameObject)Instantiate(Marbleprefab, new Vector3(_marbleX, _marbleY, 0), Quaternion.identity);
            newMarble.transform.localScale = new Vector3(_localScale, _localScale, _localScale);
            newMarble.GetComponent<CircleCollider2D>().radius = _radius;



            newMarble.GetComponent<Marble>()._value = 1;

            if (_colourType == 1)
            {
                newMarble.GetComponent<Marble>().Colour = "Red";
                newMarble.GetComponent<SpriteRenderer>().sprite = newMarble.GetComponent<Marble>().spriteArray[0];
            }
            else if (_colourType == 2)
            {

                newMarble.GetComponent<Marble>().Colour = "Yellow";
                newMarble.GetComponent<SpriteRenderer>().sprite = newMarble.GetComponent<Marble>().spriteArray[1];
            }
            else
            {
                newMarble.GetComponent<Marble>().Colour = "Blue";
                newMarble.GetComponent<SpriteRenderer>().sprite = newMarble.GetComponent<Marble>().spriteArray[2];
            }

            MarbleList.Add(newMarble);
        }
    }

    void SpawnEnemy(int level)
    {
        Debug.Log("ApplyPowerUp method start");
        foreach (KeyValuePair<Item, int> kvp in player.SortedBackpack)
        {
            if (kvp.Key is OtherItems)
            {
                Debug.Log("found OtherItems");
                OtherItems powerup = (OtherItems)kvp.Key;
                if (powerup.Type == "attack")
                {
                    if (kvp.Value >= 1)
                    {
                        _enemyNum -= 1;
                        player.SortedInventory[kvp.Key] -= 1;
                    }

                    if (kvp.Value <= 1)
                    {
                        player.Inventory.Remove(kvp.Key);
                        player.SortedInventory.Remove(kvp.Key);
                        player.SortItemList(player.Inventory, player.SortedInventory);
                    }
                }
            }
        }

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
