using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private SceneController SceneController;
    private Player Player;

    // Start is called before the first frame update
    void Start()
    {
        SceneController = GameObject.FindWithTag("SceneController").GetComponent<SceneController>();
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToSaveFile()
    {
        SceneController.ToSaveFile();
    }

    public void SaveFiles()
    {
        Player.Save();
    }
}
