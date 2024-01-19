using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Canvas Canvas;
    public Button ButtonPrefab;
    private float[] _buttonX = { -5.9f, 0, 5.9f, -5.9f, 0, 5.9f, -5.9f, 0, 5.9f };
    private float[] _buttonY = { 1.5f, 1.5f, 1.5f, 0, 0, 0, -3, -3, -3 };

    public Player Player;
    private int _counter;


    void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        InventorytoButton();
    }

    void InventorytoButton()
    {
        _counter = 0;
        foreach (KeyValuePair<Item, int> kvp in Player.SortedInventory)
        {
            SpawnButton(_buttonX[_counter], _buttonY[_counter], $"{kvp.Key.Name} X {kvp.Value}");
            _counter++;
        }
    }

    void SpawnButton(float x, float y, string content)
    {
        Button newButton = (Button)Instantiate(ButtonPrefab, new Vector3(x, y, 0), Quaternion.identity);
        newButton.transform.SetParent(Canvas.transform);
        newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
    }
}
