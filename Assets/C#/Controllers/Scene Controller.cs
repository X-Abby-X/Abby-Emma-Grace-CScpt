using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public Vector2 playerOnMapPosition;
    public List<bool> StoreButtonActiveList = new List<bool>();
    public Player player;

    void Awake()
    {
        playerOnMapPosition = new Vector2(-6.23f, -2.91f);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        // idk why it need that but it need it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Scene Controller");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Store")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                ToWorldMap();
            }
        }

        if (SceneManager.GetActiveScene().name == "Prep")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Marble Game");
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                ToWorldMap();
            }
        }

        if (SceneManager.GetActiveScene().name == "Inventory")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Store");
            }
        }
    }

    public void ToSaveFile()
    {
        SceneManager.LoadScene("Save File");
    }

    public void ToWorldMap()
    {
        SceneManager.LoadScene("World Map");
    }

}
