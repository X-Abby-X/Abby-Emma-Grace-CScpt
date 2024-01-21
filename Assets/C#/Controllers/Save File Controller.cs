using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileController : MonoBehaviour
{
    private Player _player;
    private SceneController _sceneController;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private Button _backButton;
    private float[] _buttonX = { -5.75f, 0, 5.96f };
    private float[] _buttonY = { 1.31f, 1.31f, 1.31f, -1.31f, -1.31f, -1.31f };
    public List<Button> ButtonList = new List<Button>();
    


    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();

    }

    void Start()
    {
        _backButton.onClick.AddListener(_sceneController.ToPrevious);

        SpawnButton();
    }

    void SpawnButton()
    {
        for (int i = 0; i < _player.SaveFiles.Count + 1; i++)
        {
            Button newButton = (Button)Instantiate(_buttonPrefab, new Vector3(_buttonX[i % 3], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(_canvas.transform);
            ButtonList.Add(newButton);
            if (i == 0)
            {
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
                ButtonList[i].onClick.AddListener(_sceneController.ToWorldMap);
            }
            else
            {
                int num = i - 1;
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Save File{_player.SaveFiles[num]._id}";
                ButtonList[i].onClick.AddListener(delegate { _player.LoadSaveFile(_player.SaveFiles[num]); });
            }
        }
    }
}