using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;

    public Sprite[] SpriteArray;
    public int Money;
    public int XP;
    public int Level;
    public string Colour = "Red";
  
    public List<Item> Inventory = new List<Item>();
    public Dictionary<Item, int> SortedInventory = new Dictionary<Item, int>();

    public List<Item> Backpack = new List<Item>();
    public Dictionary<Item, int> SortedBackpack = new Dictionary<Item, int>();

    public List<Stats> SaveFiles = new List<Stats>();
    private int _saveFileCounter;

    public class Stats
    {
        public int ID;
        public int Money;
        public int XP;
        public int Level;
        public string Colour;
        public List<Item> Inventory = new List<Item>();
        public List<bool> StoreButtonActiveList = new List<bool>();

        public Stats(int id, int money, int xp, int level, string colour, List<Item> inventory, List<bool> storeButtonActiveList)
        {
            Debug.Log("Make Save File");
            this.ID = id;
            this.Money = money;
            this.XP = xp;
            this.Level = level;
            this.Colour = colour;
            this.Inventory = inventory;
            this.StoreButtonActiveList = storeButtonActiveList;
        }

    }

    public void Save()
    {
        Debug.Log("Save");
        List<Item> InventoryCopy = new List<Item>(Inventory);
        List<bool> ButtonActivateCopy = new List<bool>(_sceneController.StoreButtonActiveList);
        Stats newSaveFile = new Stats(SaveFiles.Count + 1, this.Money, this.XP, this.Level, this.Colour, InventoryCopy, ButtonActivateCopy);
        SaveFiles.Add(newSaveFile);
        if (SaveFiles.Count >= 5)
        {
            SaveFiles[_saveFileCounter % 5] = newSaveFile;
        }
        _saveFileCounter++;
    }

    public void LoadSaveFile(Stats stats)
    {
        this.Money = stats.Money;
        this.XP = stats.XP;
        this.Level = stats.Level;
        SetColour(stats.Colour);
        this.Inventory.Clear();
        this.Inventory.AddRange(stats.Inventory);
        this.SortItemList(Inventory, SortedInventory);
        _sceneController.StoreButtonActiveList.Clear();
        _sceneController.StoreButtonActiveList.AddRange(stats.StoreButtonActiveList);
        _sceneController.ToWorldMap();
    }

    void Start()
    {
        this.gameObject.transform.position = _sceneController.PlayerOnMapPosition;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        PlayerMovement();
    }

    public void SetColour(string colour)
    {
        this.Colour = colour;
        if (this.Colour == "Red")
        {
            this.GetComponent<SpriteRenderer>().sprite = SpriteArray[0];
        }
        else if (this.Colour == "Yellow")
        {

            this.GetComponent<SpriteRenderer>().sprite = SpriteArray[1];
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = SpriteArray[2];
        }

    }

    public void levelUp()
    {
        this.Level = (this.XP - this.Level * 10) / 10;
    }

    public void GetStats()
    {
        Debug.Log(this.Money);
        Debug.Log(this.XP);
        Debug.Log(this.Level);

        foreach (Item item in Inventory)
        {
            Debug.Log(item);
        }

        foreach (Item item in Backpack)
        {
            Debug.Log(item);
        }
    }

    void PlayerMovement()
    {
        if (SceneManager.GetActiveScene().name == "World Map")
        {
            //arrow key input
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Vector2 pos = _sceneController.PlayerOnMapPosition;

            pos.y += v * 3 * Time.deltaTime;
            pos.x += h * 3 * Time.deltaTime;

            transform.position = pos;

            _sceneController.PlayerOnMapPosition = transform.position;
        }
        else
        {
            this.gameObject.transform.position = new Vector3(1.5f, 7f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            this.Money += 1;
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (collision.tag == "Store")
            {
                SceneManager.LoadScene("Store");
            }
            else
            {
                if (collision.tag == "Fighthouse1")
                {
                    MarbleGameController.Level = 1;
                }
                else if (collision.tag == "Fighthouse2")
                {
                    MarbleGameController.Level = 2;
                }
                else if (collision.tag == "Fighthouse3")
                {
                    MarbleGameController.Level = 3;
                }
                SceneManager.LoadScene("Prep");
            }

            
        }

    }

    public void SortItemList(List<Item> list, Dictionary<Item, int> Dictionary)
    {
        Dictionary.Clear();

        foreach (Item item in list)
        {
            bool Added = false;
            foreach (Item key in Dictionary.Keys)
            {
                if (key.Name == item.Name)
                {
                    Dictionary[item] += 1;
                    Added = true;
                    break;
                }
            }
            if (Added == false)
            {
                Dictionary.Add(item, 1);
            }

        }
    }
}
