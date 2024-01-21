using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PreferenceController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    private SceneController _sceneController;
    public Button BackButton;

    void Awake()
    {
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(_sceneController.ToPrevious);

        if (_sceneController.Volume == -10000f)
        {
            SetVolume();
        }
        else
        {
            LoadVolume();
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
