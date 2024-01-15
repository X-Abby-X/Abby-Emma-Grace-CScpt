using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public Rigidbody2D BallObject;
    public LineRenderer Lr;
    private float _distance;
    public float _powerFactor = 2f;
    private Vector2 InitialMousePosition;
    private Vector2 BallPosition;
    private Vector2 MousePosition;

    public bool _drag = false;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {

    }
    public void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_hitMarble.Clear();
            //SortedMarble.Clear();
            InitialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Lr.positionCount = 2;
            Lr.SetPosition(0, transform.position);
            Lr.SetPosition(1, MousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            BallPosition = BallObject.transform.position;
            DragRelease(InitialMousePosition, MousePosition, BallPosition);
            _drag = true;
        }

        void DragRelease(Vector2 InitMousepos, Vector2 Mousepos, Vector2 BallPos)
        {
            Vector2 Direction = BallPos - Mousepos;

            _distance = (float)Math.Sqrt(Math.Pow((Mousepos.x - InitMousepos.x), 2.00f) + Math.Pow((Mousepos.y - InitMousepos.y), 2.00f));

            BallObject.AddForce(Direction * (_distance * _powerFactor), ForceMode2D.Impulse);

            Lr.positionCount = 0;
        }

    }
}
