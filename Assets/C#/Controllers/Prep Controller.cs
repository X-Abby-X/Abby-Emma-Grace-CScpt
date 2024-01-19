using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prepcontroller : MonoBehaviour
{
    public TextMeshProUGUI ButtonText;
    public Canvas Canvas;
    public Button ButtonPrefab;
    public List<Button> ButtonList = new List<Button>();
    private float[] _buttonX = { -5.9f, 0, 5.9f, -5.9f, 0, 5.9f, -5.9f, 0, 5.9f };
    private float[] _buttonY = { 1.5f, 1.5f, 1.5f, 0, 0, 0, -3, -3, -3 };

    public Player Player;
    private int _counter;
    public List<string> ItemListName = new List<string>();

    public List<Button> ColourButtonList = new List<Button>();
    private float[] _askForColourbuttonX = { -5.71f, 0, 5.83f, };
    private float _askForColourbuttonY = -3.68f;
    private string[] _askForColourText = { "red", "yellow", "blue" };


    void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        Player.Backpack.Clear();
        Player.SortedBackpack.Clear();
        InventoryToButton();
    }


    void InventoryToButton()
    {
        _counter = 0;
        foreach (KeyValuePair<Item, int> kvp in Player.SortedInventory)
        {
            SpawnButton(_buttonX[_counter], _buttonY[_counter], $"{kvp.Key.Name} X {kvp.Value}", kvp.Key, _counter, kvp.Key.Name);
            _counter++;
        }
    }

    void SpawnButton(float x, float y, string content, Item item, int counter, string name)
    {
        Button newButton = (Button)Instantiate(ButtonPrefab, new Vector3(x, y, 0), Quaternion.identity);
        newButton.transform.SetParent(Canvas.transform);
        newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
        ButtonList.Add(newButton);
        if (name == "Change All Marble to Colour...")
        {
            Debug.Log("Running");
            newButton.onClick.AddListener(delegate { AskForColour(item, counter); });
        }
        else
        {
            newButton.onClick.AddListener(delegate { AddtoBackpack(item, counter); });
        }

    }

    void AddtoBackpack(Item item, int num)
    {
        Player.Backpack.Add(item);
        ButtonList[num].interactable = false;
        Player.SortItemList(Player.Backpack, Player.SortedBackpack);
        foreach (Item i in Player.Backpack)
        {
            Debug.Log(i.Name);
        }
    }


    public void AskForColour(Item item, int num)
    {
        for (int i = 0; i < 3; i++)
        {
            Button newButton = (Button)Instantiate(ButtonPrefab, new Vector3(_askForColourbuttonX[i], _askForColourbuttonY, 0), Quaternion.identity);
            newButton.transform.SetParent(Canvas.transform);
            newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _askForColourText[i];
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
        AddtoBackpack(powerup, num);
    }
}