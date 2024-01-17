using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text text;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (MarbleGameController.Win == true)
        {
            SpawnTextBox(0, 3, $"Congrats You Win");
        }
        else
        {
            SpawnTextBox(0, 3, $"You Loser");
        }
        SpawnTextBox(0, 1.84f, $"money earned: ${MarbleGameController.MoneyEarned}");
        SpawnTextBox(0, 0.92f, $"xp earned: {MarbleGameController.xpEarned}");
        SpawnTextBox(0, -1.45f, $"money owned: {player.GetComponent<Player>().Money}");
        SpawnTextBox(0, -2.45f, $"xp owned: {player.GetComponent<Player>().Xp}");
        SpawnTextBox(0, -3.45f, $"current level: {player.GetComponent<Player>().Level}");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("world map");
        }
    }

    void SpawnTextBox(float x, float y, string content)
    {
        TMP_Text newtext = (TMP_Text)Instantiate(text, new Vector3(x, y, 0), Quaternion.identity);
        newtext.GetComponent<TMP_Text>().text = content;
        newtext.transform.SetParent(canvas.transform);
    }
}