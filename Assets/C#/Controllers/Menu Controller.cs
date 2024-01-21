using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _preferenceButton;
    private SceneController _sceneController;

    void Awake()
    {
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(_sceneController.ToSaveFile);
        _preferenceButton.onClick.AddListener(_sceneController.ToPreference);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
