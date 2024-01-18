using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public TextMeshProUGUI ButtonText;
    public Canvas canvas;
    public Button buttonprefab;
    private float[] _buttonX = { -5.9f, 0, 5.9f, -5.9f, 0, 5.9f, -5.9f, 0, 5.9f };
    private float[] _buttonY = { 1.5f, 1.5f, 1.5f, 0, 0, 0, -3, -3, -3 };

    public Player player;
    private int _counter;


    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InventorytoButton();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InventorytoButton()
    {
        _counter = 0;
        foreach (KeyValuePair<Items, int> kvp in player.SortedInventory)
        {
            SpawnButton(_buttonX[_counter], _buttonY[_counter], $"{kvp.Key.Name} X {kvp.Value}");
            _counter++;
        }
    }

    void SpawnButton(float x, float y, string content)
    {
        Button newButton = (Button)Instantiate(buttonprefab, new Vector3(x, y, 0), Quaternion.identity);
        newButton.transform.SetParent(canvas.transform);
        newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
    }
}
