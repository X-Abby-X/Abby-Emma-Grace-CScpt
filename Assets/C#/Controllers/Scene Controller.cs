using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{


    public Vector2 PlayerOnMapPosition;
    public List<bool> StoreButtonActiveList = new List<bool>();
    public float Volume = -10000f;
    [SerializeField] private Player _player;
    private int _currentSceneIndex;
    private int _previousSceneIndex;
    public List<bool> ColourOptionsOn = new List<bool> { false, false, false };

    void Awake()
    {
        PlayerOnMapPosition = new Vector2(-6.23f, -2.91f);

    }

    void Start()
    {
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Scene Controller");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

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

    public void ToPrevious()
    {
        if (_previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(_previousSceneIndex);
        }
        else
        {
            Debug.Log("No previous scene available.");
        }
    }

    public void ToSaveFile()
    {
        _previousSceneIndex = _currentSceneIndex;
        SceneManager.LoadScene("Save File");
    }

    public void ToWorldMap()
    {
        SceneManager.LoadScene("World Map");
    }

    public void ToStore()
    {
        _previousSceneIndex = _currentSceneIndex;
        SceneManager.LoadScene("Store");
    }

    public void ToPreference()
    {
        _previousSceneIndex = _currentSceneIndex;
        SceneManager.LoadScene("Preference");
    }
}