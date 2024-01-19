using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileController : MonoBehaviour
{
    private Player player;
    public Canvas canvas;
    public Button buttonprefab;
    private SceneController SceneController;
    private float[] _buttonX = { -5.75f, 0, 5.96f };
    private float[] _buttonY = { -0.28f, -0.28f, -0.28f, -3.39f, -3.39f, -3.39f };
    public List<Button> ButtonList = new List<Button>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SceneController = GameObject.FindWithTag("Scene Controller").GetComponent<SceneController>();
        SpawnButton();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SpawnButton()
    {
        for (int i = 0; i < player.SaveFiles.Count + 1; i++)
        {
            Button newButton = (Button)Instantiate(buttonprefab, new Vector3(_buttonX[i % 3], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(canvas.transform);
            ButtonList.Add(newButton);
            if (i == 0)
            {
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "new game";
                ButtonList[i].onClick.AddListener(SceneController.ToWorldMap);
            }
            else
            {
                int num = i - 1;
                newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Save File{player.SaveFiles[num]._id}";
                ButtonList[i].onClick.AddListener(delegate { player.LoadSaveFile(player.SaveFiles[num]); });
            }


        }
    }
}