using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public AudioSource MusicSource;
    public AudioClip BackGroundMusic;
    public Vector2 PlayerOnMapPosition;
    public List<bool> StoreButtonActiveList = new List<bool>();
    public Player Player;

    void Awake()
    {
        PlayerOnMapPosition = new Vector2(-6.23f, -2.91f);
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Scene Controller");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        MusicSource.clip = BackGroundMusic;
        MusicSource.Play();
    }

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
                ToStore();
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

    public void ToStore()
    {
        SceneManager.LoadScene("World Map");
    }
}