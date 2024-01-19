using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public MarbleGameController MarbleGameController;

    public Rigidbody2D BallObject;
    public LineRenderer Lr;
    private float _distance;
    public float _powerFactor = 2f;
    private Vector2 InitialMousePosition;
    private Vector2 BallPosition;
    private Vector2 MousePosition;

    public bool _drag = false;
    private bool _hit = false;

    private List<GameObject> _hitMarble = new List<GameObject>();
    public Dictionary<string, List<GameObject>> SortedMarble = new Dictionary<string, List<GameObject>>();

    // Update is called once per frame

    void Update()
    {
        if (MarbleGameController.GameStart && MarbleGameController.GamePaused == false)
        {
            if (_drag == false)
            {
                UserInput();

            }
            if (_hit == true)
            {
                SortedMarble = Sort(_hitMarble);

            }
            BallStop();
        }
    }

    public void BallStop()
    {
        if (_drag && BallObject.velocity.magnitude < 0.2f)
        {
            StartCoroutine(MarbleGameController.AttackGameSequence());
            _drag = false;
        }
    }

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

        if (Input.GetMouseButtonUp(0))
        {
            BallPosition = BallObject.transform.position;
            DragRelease(InitialMousePosition, MousePosition, BallPosition);
            _drag = true;
        }
    }

    void DragRelease(Vector2 InitMousepos, Vector2 Mousepos, Vector2 BallPos)
    {
        Vector2 Direction = BallPos - Mousepos;

        _distance = (float)Math.Sqrt(Math.Pow((Mousepos.x - InitMousepos.x), 2.00f) + Math.Pow((Mousepos.y - InitMousepos.y), 2.00f));

        BallObject.AddForce(Direction * (_distance * _powerFactor), ForceMode2D.Impulse);

        Lr.positionCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Marble")
        {
            _hitMarble.Add(collision.gameObject);
            collision.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-0.96f, -1.13f), -1.15f, 0);
            Rigidbody2D rb = collision.gameObject.AddComponent<Rigidbody2D>();
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 0;
            collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
            _hit = true;
        }
    }

    public Dictionary<string, List<GameObject>> Sort(List<GameObject> list)
    {
        Dictionary<string, List<GameObject>> ColourCount = new Dictionary<string, List<GameObject>>();
        ColourCount.Clear();
        foreach (GameObject marble in list)
        {
            if (ColourCount.ContainsKey(marble.GetComponent<Marble>().Colour))
            {
                ColourCount[marble.GetComponent<Marble>().Colour].Add(marble);
            }
            else
            {
                ColourCount.Add(marble.GetComponent<Marble>().Colour, new List<GameObject> { marble });
            }
        }
        _hit = false;

        return ColourCount;
    }
}

