using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private SceneController SceneController;
    private Player Player;

    void Start()
    {
        SceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();

    }

    public void ToSaveFile()
    {
        SceneController.ToSaveFile();
    }

    public void SaveFiles()
    {
        Player.Save();
    }

    public void ToPreference()
    {
        SceneController.ToPreference();
    }
}
