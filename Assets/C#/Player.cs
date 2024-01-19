using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public int Money;
    public int Xp;
    public int Level;
    public SceneController SceneController;
    public MarbleGameController MarbleGameController;
    public List<Item> Inventory = new List<Item>();
    public Dictionary<Item, int> SortedInventory = new Dictionary<Item, int>();

    public List<Item> Backpack = new List<Item>();
    public Dictionary<Item, int> SortedBackpack = new Dictionary<Item, int>();

    public List<Stats> SaveFiles = new List<Stats>();
    public int Counter;

    public class Stats
    {
        public int _id;
        public int _money;
        public int _xp;
        public int _level;
        public List<Item> _inventory = new List<Item>();
        public List<bool> _storeButtonActiveList = new List<bool>();

        public Stats(int id, int money, int xp, int level, List<Item> inventory, List<bool> storeButtonActiveList)
        {
            Debug.Log("make save file");
            this._id = id;
            this._money = money;
            this._xp = xp;
            this._level = level;
            this._inventory = inventory;
            this._storeButtonActiveList = storeButtonActiveList;
        }

        public void GetStats()
        {
            Debug.Log("print save file");
            Debug.Log(this._id);
            Debug.Log(this._money);
            Debug.Log(this._xp);
            Debug.Log(this._level);
            foreach (Item i in _inventory)
            {
                Debug.Log(i.Name);
            }
        }

    }

    public void Save()
    {
        Debug.Log("save");
        List<Item> InventoryCopy = new List<Item>(Inventory);
        List<bool> ButtonActivateCopy = new List<bool>(SceneController.StoreButtonActiveList);
        Stats newSaveFile = new Stats(SaveFiles.Count + 1, this.Money, this.Xp, this.Level, InventoryCopy, ButtonActivateCopy);
        SaveFiles.Add(newSaveFile);
        if (SaveFiles.Count >= 5)
        {
            SaveFiles[Counter % 5] = newSaveFile;
        }
        Counter++;
    }


    public void LoadSaveFile(Stats stats)
    {
        this.Money = stats._money;
        this.Xp = stats._xp;
        this.Level = stats._level;
        this.Inventory.Clear();
        this.Inventory.AddRange(stats._inventory);
        this.SortItemList(Inventory, SortedInventory);
        SceneController.StoreButtonActiveList.Clear();
        SceneController.StoreButtonActiveList.AddRange(stats._storeButtonActiveList);
        SceneController.ToWorldMap();
    }


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = SceneController.playerOnMapPosition;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Playermovement();
    }

    public void levelUp()
    {
        this.Level = (this.Xp - this.Level * 10) / 10;
    }


    public void GetStats()
    {
        Debug.Log(this.Money);
        Debug.Log(this.Xp);
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

    void Playermovement()
    {
        if (SceneManager.GetActiveScene().name == "World Map")
        {
            //arrow key input
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Vector2 pos = SceneController.playerOnMapPosition;

            pos.y += v * 3 * Time.deltaTime;
            pos.x += h * 3 * Time.deltaTime;

            transform.position = pos;

            SceneController.playerOnMapPosition = transform.position;
        }
        else
        {
            // put it out of scene
            this.gameObject.transform.position = new Vector3(1.5f, 7f, 0);
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
                    MarbleGameController.level = 1;
                }
                else if (collision.tag == "Fighthouse2")
                {
                    MarbleGameController.level = 2;
                }
                else if (collision.tag == "Fighthouse3")
                {
                    MarbleGameController.level = 3;
                }
                SceneManager.LoadScene("Prep");
            }

            
        }

    }

    // pigeon sort
    public void SortItemList(List<Item> list, Dictionary<Item, int> Dictionary)
    {
        Dictionary.Clear();

        foreach (Item items in list)
        {
            if (Dictionary.ContainsKey(items))
            {
                Dictionary[items] += 1;
                Debug.Log("Found");
            }
            else
            {
                Dictionary.Add(items, 1);
                Debug.Log("new");
            }
        }
    }
}
