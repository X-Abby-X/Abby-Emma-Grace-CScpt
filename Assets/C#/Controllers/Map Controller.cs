using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    private SceneController _sceneController;
    private Player _player;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadSaveFileButton;
    [SerializeField] private Button _preferenceButton;
    [SerializeField] private GameObject _coinPrefab;


    private void Awake()
    {
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        AddButtonListener();
        SpawnCoin();
    }


    void SpawnCoin()
    {
        //GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        //if (coins.Length > 0)
        //{
        //    foreach (GameObject coin in coins)
        //    {
        //        Destroy(coin);
        //    }
        //}

            
        for (int i = 0; i < 6; i++)
        {
            float _coinX = Random.Range(-8, 8);
            float _coinY = Random.Range(-4, 2.9f);

            GameObject newCoin = (GameObject)Instantiate(_coinPrefab, new Vector3(_coinX, _coinY, 0), Quaternion.identity);
        }
    }



    void AddButtonListener()
    {
        _saveButton.onClick.AddListener(_player.Save);
        _loadSaveFileButton.onClick.AddListener(_sceneController.ToSaveFile);
        _preferenceButton.onClick.AddListener(_sceneController.ToPreference);

    }


}
