using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public LineRenderer Lr;
    private Vector2 InitialMousePosition;
    private Vector2 MousePosition;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    { }
    public void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hitMarble.Clear();
            SortedMarble.Clear();
            InitialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Lr.positionCount = 2;
            Lr.SetPosition(0, transform.position);
            Lr.SetPosition(1, MousePosition);
        }
    }
}
