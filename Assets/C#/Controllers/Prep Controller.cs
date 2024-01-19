using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prepcontroller : MonoBehaviour
{
    public TextMeshProUGUI ButtonText;
    public Canvas canvas;
    public Button buttonprefab;
    public List<Button> ButtonList = new List<Button>();
    private float[] _buttonX = { -5.9f, 0, 5.9f, -5.9f, 0, 5.9f, -5.9f, 0, 5.9f };
    private float[] _buttonY = { 1.5f, 1.5f, 1.5f, 0, 0, 0, -3, -3, -3 };

    public Player player;
    private int _counter;
    public List<string> ItemListName = new List<string>();

    public List<Button> ColourButtonList = new List<Button>();
    private float[] _AskForColourbuttonX = { -5.71f, 0, 5.83f, };
    private float _AskForColourbuttonY = -3.68f;
    private string[] _AskForColourText = { "red", "yellow", "blue" };


    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.Backpack.Clear();
        player.SortedBackpack.Clear();
        InventorytoButton();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void InventorytoButton()
    {
        _counter = 0;
        foreach (KeyValuePair<Item, int> kvp in player.SortedInventory)
        {
            SpawnButton(_buttonX[_counter], _buttonY[_counter], $"{kvp.Key.Name} X {kvp.Value}", kvp.Key, _counter, kvp.Key.Name);
            _counter++;
        }
    }

    void SpawnButton(float x, float y, string content, Item item, int counter, string name)
    {
        Button newButton = (Button)Instantiate(buttonprefab, new Vector3(x, y, 0), Quaternion.identity);
        newButton.transform.SetParent(canvas.transform);
        newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
        ButtonList.Add(newButton);
        if (name == "Change All Marble to colour...")
        {
            Debug.Log("running");
            newButton.onClick.AddListener(delegate { AskForColour(item, counter); });
        }
        else
        {
            newButton.onClick.AddListener(delegate { AddtoBackPack(item, counter); });
        }

    }

    void AddtoBackPack(Item item, int num)
    {
        player.Backpack.Add(item);
        ButtonList[num].interactable = false;
        player.SortItemList(player.Backpack, player.SortedBackpack);
        foreach (Item i in player.Backpack)
        {
            Debug.Log(i.Name);
        }
    }


    public void AskForColour(Item item, int num)
    {
        for (int i = 0; i < 3; i++)
        {
            Button newButton = (Button)Instantiate(buttonprefab, new Vector3(_AskForColourbuttonX[i], _AskForColourbuttonY, 0), Quaternion.identity);
            newButton.transform.SetParent(canvas.transform);
            newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _AskForColourText[i];
            ColourButtonList.Add(newButton);
            newButton.onClick.AddListener(delegate { SetItemColour(newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text, item, num); });
        }

    }

    public void SetItemColour(string colour, Item item, int num)
    {
        MarbleItems powerup = (MarbleItems)item;

        if (colour == "red")
        {
            powerup.ColourValue = 1;
        }
        else if (colour == "yellow")
        {
            powerup.ColourValue = 2;
        }
        if (colour == "blue")
        {
            powerup.ColourValue = 3;
        }

        for (int i = 0; i < ColourButtonList.Count; i++)
        {
            ColourButtonList[i].interactable = false;
        }
        AddtoBackPack(powerup, num);
    }
}