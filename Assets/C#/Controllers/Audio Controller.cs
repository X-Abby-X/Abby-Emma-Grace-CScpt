using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip BackGroundMusic;



    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Audio Controller");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        MusicSource.clip = BackGroundMusic;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
