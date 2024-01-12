using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public int Money;
    public int Xp;
    public int Level;
    public SceneController SceneController;
    public List<GameObject> Inventory = new List<GameObject>();
    public Dictionary<GameObject, int> SortedInventory = new Dictionary<GameObject, int>();

    public List<GameObject> Backpack = new List<GameObject>();
    public Dictionary<GameObject, int> SortedBackpack = new Dictionary<GameObject, int>();


    // Start is called before the first frame update
    void Start()
    {
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

        foreach (GameObject item in Inventory)
        {
            Debug.Log(item);
        }

        foreach (GameObject item in Backpack)
        {
            Debug.Log(item);
        }
    }

    private void Playermovement()
    {

        if (SceneManager.GetActiveScene().name == "world map")
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

    // pigeon sort
    public void SortItemList(List<GameObject> list, Dictionary<GameObject, int> Dictionary)
    {
        Dictionary.Clear();

        foreach (GameObject items in list)
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
