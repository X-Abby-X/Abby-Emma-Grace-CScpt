using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileController : MonoBehaviour
{
    private Player Player;
    public Canvas Canvas;
    public Button ButtonPrefab;
    private SceneController _sceneController;
    private float[] _buttonX = { -5.75f, 0, 5.96f };
    private float[] _buttonY = { -0.28f, -0.28f, -0.28f, -3.39f, -3.39f, -3.39f };
    public List<Button> ButtonList = new List<Button>();


    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        SpawnButton();
    }

    void SpawnButton()
    {
        for (int i = 0; i < Player.SaveFiles.Count + 1; i++)
        {
            Button newButton = (Button)Instantiate(ButtonPrefab, new Vector3(_buttonX[i % 3], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(Canvas.transform);
            ButtonList.Add(newButton);
            if (i == 0)
            {
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
                ButtonList[i].onClick.AddListener(_sceneController.ToWorldMap);
            }
            else
            {
                int num = i - 1;
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Save File{Player.SaveFiles[num]._id}";
                ButtonList[i].onClick.AddListener(delegate { Player.LoadSaveFile(Player.SaveFiles[num]); });
            }
        }
    }
}