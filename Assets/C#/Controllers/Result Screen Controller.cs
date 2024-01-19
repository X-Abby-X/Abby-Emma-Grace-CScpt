using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{

    public GameObject Canvas;
    public TMP_Text Text;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (MarbleGameController.Win == true)
        {
            SpawnTextBox(0, 3, $"Congrats You Win");
        }
        else
        {
            SpawnTextBox(0, 3, $"You Loser");
        }
        SpawnTextBox(0, 1.84f, $"Money Earned: ${MarbleGameController.MoneyEarned}");
        SpawnTextBox(0, 0.92f, $"XP Earned: {MarbleGameController.XPEarned}");
        SpawnTextBox(0, -1.45f, $"Money Owned: {Player.GetComponent<Player>().Money}");
        SpawnTextBox(0, -2.45f, $"XP Owned: {Player.GetComponent<Player>().XP}");
        SpawnTextBox(0, -3.45f, $"Current Level: {Player.GetComponent<Player>().Level}");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("World Map");
        }
    }

    void SpawnTextBox(float x, float y, string content)
    {
        TMP_Text newtext = (TMP_Text)Instantiate(Text, new Vector3(x, y, 0), Quaternion.identity);
        newtext.GetComponent<TMP_Text>().text = content;
        newtext.transform.SetParent(Canvas.transform);
    }
}