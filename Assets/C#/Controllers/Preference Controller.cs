using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PreferenceController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private List<Toggle> _colourOptions;
    [SerializeField] private Button _backButton;
    
    private Player _player;
    private SceneController _sceneController;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
    }

    void Start()
    {
        _backButton.onClick.AddListener(_sceneController.ToPrevious);
        if (_sceneController.Volume == -10000f)
        {
            SetVolume();
        }
        else
        {
            LoadVolume();
        }

    }

    void Update()
    {
        LoadColourOption();
    }

    public void SetVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Background Music", Mathf.Log10(volume) * 20);
        _sceneController.Volume = volume;
    }

    public void LoadVolume()
    {
        _musicSlider.value = _sceneController.Volume;
        SetVolume();
    }

    public void LoadColourOption()
    {
        for (int i = 0; i < 3; i++)
        {
            _colourOptions[i].isOn = _sceneController.ColourOptionsOn[i];
        }
    }

    public void Red()
    {
        SelectedToggle(0);
        _player.SetColour("Red");
    }

    public void Yellow()
    {
        SelectedToggle(1);
        _player.SetColour("Yellow");
    }

    public void Blue()
    {
        SelectedToggle(2);
        _player.SetColour("Blue");
    }

    private void SelectedToggle(int num)
    {
        Debug.Log("Changed");
        for (int i = 0; i < _sceneController.ColourOptionsOn.Count; i++)
        {
            _sceneController.ColourOptionsOn[i] = false;
        }
        _sceneController.ColourOptionsOn[num] = true;
    }


}
