using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static UnityEditor.Progress;

public class StoreController : MonoBehaviour
{
    public Player player;
    public List<Item> StoreInventory = new List<Item>();
    public SceneController SceneController;
    public TextMeshProUGUI MoneyText;
    public Canvas canvas;
    public Button buttonprefab;
    public List<Button> ButtonList = new List<Button>();

    private float[] _buttonX = { -5.75f, 0, 5.96f, -5.75f, 0, 5.96f };
    private float[] _buttonY = { -0.28f, -0.28f, -0.28f, -3.39f, -3.39f, -3.39f };

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        CreateStoreInventory();
        SpawnButton();
    }

    void Start()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].interactable = SceneController.StoreButtonActiveList[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = $"You have ${player.Money}";
    }

    void SpawnButton()
    {
        for (int i = 0; i < StoreInventory.Count; i++)
        {
            Button newButton = (Button)Instantiate(buttonprefab, new Vector3(_buttonX[i], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(canvas.transform);
            ButtonList.Add(newButton);
            SceneController.StoreButtonActiveList.Add(true);
            newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy, ${StoreInventory[i].Cost}";
        }

        for (int i = 0; i < ButtonList.Count; i++)
        {
            int num = i;
            if (i < 3)
            {
                ButtonList[i].onClick.AddListener(delegate { OneTimePowerUp(num); });
            }
            else if (i >= 3)
            {
                ButtonList[i].onClick.AddListener(delegate { MutiplePowerUp(num); });
            }
        }
    }

    void CreateStoreInventory()
    {
        StatsItems attack_power_up = new StatsItems(1, "strength", "Attack Power Up", 0);
        StatsItems Hp_power_up = new StatsItems(1, "hp", "Hp Power Up", 0);
        MarbleItems MarbleSize = new MarbleItems(1, "size", "Marble Size Up", 0, "red");
        MarbleItems MarbleColour = new MarbleItems(1, "colour", "Change All Marble to colour...", 0, "red");
        OtherItems KillEnemy = new OtherItems(1, "attack", "Kill An Enemy", 0);
        OtherItems MaxHealth = new OtherItems(1, "Hp", "MaxHealth", 0);

        StoreInventory.Add(attack_power_up);
        StoreInventory.Add(Hp_power_up);
        StoreInventory.Add(MarbleSize);
        StoreInventory.Add(MarbleColour);
        StoreInventory.Add(KillEnemy);
        StoreInventory.Add(MaxHealth);
    }

    void Buy(Item item)
    {
        Debug.Log("stuff bought");
        player.Money -= item.Cost;
        Debug.Log(player.Money);
        player.Inventory.Add(item);
        player.SortItemList(player.Inventory, player.SortedInventory);
    }

    public void OneTimePowerUp(int i)
    {
        Debug.Log("button pressed");
        if (player.Money >= StoreInventory[i].Cost)
        {
            Buy(StoreInventory[i]);
            SceneController.StoreButtonActiveList[i] = false;
            ButtonList[i].interactable = false;
        }
        else
        {
            Debug.Log("you're broke");
        }


    }

    public void MutiplePowerUp(int i)
    {
        Debug.Log("button pressed");
        if (player.Money >= StoreInventory[i].Cost)
        {
            Buy(StoreInventory[i]);
        }
        else
        {
            Debug.Log("you're broke");
        }
    }

    public void Inventory()
    {
        SceneManager.LoadScene("Inventory");
    }
}
