using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PreferenceController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;

    



    // Start is called before the first frame update
    void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Background Music", Mathf.Log10(volume) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
