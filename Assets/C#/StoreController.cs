using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static UnityEditor.Progress;

public class StoreController : MonoBehaviour
{
    public List<GameObject> StoreInventory = new List<GameObject>();
    public Canvas canvas;
    public Button buttonprefab;
    public List<Button> ButtonList = new List<Button>();

    private float[] _buttonX = { -5.75f, 0, 5.96f, -5.75f, 0, 5.96f };
    private float[] _buttonY = { -0.28f, -0.28f, -0.28f, -3.39f, -3.39f, -3.39f };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnButton()
    {
        for (int i = 0; i < StoreInventory.Count; i++)
        {
            Button newButton = (Button)Instantiate(buttonprefab, new Vector3(_buttonX[i], _buttonY[i], 0), Quaternion.identity);
            newButton.transform.SetParent(canvas.transform);
            ButtonList.Add(newButton);
            newButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy";
        }

        for (int i = 0; i < ButtonList.Count; i++)
        {
            if (i == 0)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(0); });
            }
            else if (i == 1)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(1); });
            }
            else if (i == 2)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(2); });
            }
            else if (i == 3)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(3); });
            }
            else if (i == 4)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(4); });
            }
            else if (i == 5)
            {
                ButtonList[i].onClick.AddListener(delegate { NotDoneYet(5); });
            }
        }
    }
    public void NotDoneYet(int i)
    {
        
    }
}
