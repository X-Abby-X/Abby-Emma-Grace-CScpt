using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StoreController : MonoBehaviour
{
    private Player _player;
    private SceneController _sceneController;
    private List<Item> _storeInventory = new List<Item>();
    private List<Button> _buttonList = new List<Button>();

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Button _toWorldMapButton;


    private float[] _buttonX = { -5.75f, 0, 5.96f, -5.75f, 0, 5.96f };
    private float[] _buttonY = { -0.28f, -0.28f, -0.28f, -3.39f, -3.39f, -3.39f };

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        _toWorldMapButton.onClick.AddListener(_sceneController.ToWorldMap);
        CreateStoreInventory();
        SpawnButton();
    }

    void Start()
    {
        for (int i = 0; i < _buttonList.Count; i++)
        {
            _buttonList[i].interactable = _sceneController.StoreButtonActiveList[i];
        }

    }

    void Update()
    {
        _moneyText.text = $"You have ${_player.Money}";
    }

    void SpawnButton()
    {
        for (int i = 0; i < _storeInventory.Count; i++)
        {
            Button newButton = (Button)Instantiate(_buttonPrefab, new Vector3(_buttonX[i], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(_canvas.transform);
            _buttonList.Add(newButton);
            _sceneController.StoreButtonActiveList.Add(true);
            newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy, ${_storeInventory[i].Cost}";
        }

        for (int i = 0; i < _buttonList.Count; i++)
        {
            int num = i;
            if (i < 3)
            {
                _buttonList[i].onClick.AddListener(delegate { OneTimePowerUp(num); });
            }
            else if (i >= 3)
            {
                _buttonList[i].onClick.AddListener(delegate { MultiplePowerUp(num); });
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

        _storeInventory.Add(attack_power_up);
        _storeInventory.Add(Hp_power_up);
        _storeInventory.Add(MarbleSize);
        _storeInventory.Add(MarbleColour);
        _storeInventory.Add(KillEnemy);
        _storeInventory.Add(MaxHealth);
    }

    void Buy(Item item)
    {
        Debug.Log("Stuff Bought");
        _player.Money -= item.Cost;
        Debug.Log(_player.Money);
        _player.Inventory.Add(item);
        foreach (Item i in _player.Inventory  )
        {
            Debug.Log(i.Name);
        }
        _player.SortItemList(_player.Inventory, _player.SortedInventory);
    }

    public void OneTimePowerUp(int i)
    {
        Debug.Log("Button Pressed");
        if (_player.Money >= _storeInventory[i].Cost)
        {
            Buy(_storeInventory[i]);
            _sceneController.StoreButtonActiveList[i] = false;
            _buttonList[i].interactable = false;
        }
        else
        {
            Debug.Log("You're Broke");
        }


    }

    public void MultiplePowerUp(int i)
    {
        Debug.Log("Button Pressed");
        if (_player.Money >= _storeInventory[i].Cost)
        {
            Buy(_storeInventory[i]);
        }
        else
        {
            Debug.Log("You're Broke");
        }
    }

    public void Inventory()
    {
        SceneManager.LoadScene("Inventory");
    }
}
