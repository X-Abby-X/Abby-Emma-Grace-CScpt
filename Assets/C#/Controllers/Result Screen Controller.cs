using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _toWorldMapButton;

    private Player _player;
    private SceneController _sceneController;
    

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();

    }

    void Start()
    {
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
        SpawnTextBox(0, -1.45f, $"Money Owned: {_player.Money}");
        SpawnTextBox(0, -2.45f, $"XP Owned: {_player.XP}");
        SpawnTextBox(0, -3.45f, $"Current Level: {_player.Level}");
        _toWorldMapButton.onClick.AddListener(_sceneController.ToWorldMap);
    }

    void Update()
    {

    }

    void SpawnTextBox(float x, float y, string content)
    {
        TMP_Text newtext = (TMP_Text)Instantiate(_text, new Vector3(x, y, 0), Quaternion.identity);
        newtext.GetComponent<TMP_Text>().text = content;
        newtext.transform.SetParent(_canvas.transform);
    }
}