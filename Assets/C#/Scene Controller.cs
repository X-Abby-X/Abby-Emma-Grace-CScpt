using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public Vector2 playerOnMapPosition;
    public GameObject player;

    void Awake()
    {
        playerOnMapPosition = new Vector2(-6.23f, -2.91f);
    }

    void Start()
    {
        // idk why it need that but it need it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SceneController");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
