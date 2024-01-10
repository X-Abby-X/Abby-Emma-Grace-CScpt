using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    private Vector2 InitialMousePosition;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
    public void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hitMarble.Clear();
            SortedMarble.Clear();
            InitialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
