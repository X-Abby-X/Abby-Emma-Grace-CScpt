using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private Player _player;
    private SceneController _sceneController;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private Button _backButton;

    private float[] _buttonX = { -5.9f, 0, 5.9f, -5.9f, 0, 5.9f, -5.9f, 0, 5.9f };
    private float[] _buttonY = { 1.5f, 1.5f, 1.5f, 0, 0, 0, -3, -3, -3 };

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        _backButton.onClick.AddListener(_sceneController.ToStore);
    }

    void Start()
    {
        _player.SortItemList(_player.Inventory, _player.SortedInventory);
        InventorytoButton();
    }

    void InventorytoButton()
    {
        int counter = 0;
        foreach (KeyValuePair<Item, int> kvp in _player.SortedInventory)
        {
            SpawnButton(_buttonX[counter], _buttonY[counter], $"{kvp.Key.Name} X {kvp.Value}");
            counter++;
        }
    }

    void SpawnButton(float x, float y, string content)
    {
        Button newButton = (Button)Instantiate(_buttonPrefab, new Vector3(x, y, 0), Quaternion.identity);
        newButton.transform.SetParent(_canvas.transform);
        newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
    }
}
