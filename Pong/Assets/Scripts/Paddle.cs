using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private float yBounds;

    private void Start()
    {
        float paddleHeightHalf = transform.localScale.y / 2;
        yBounds = Camera.main.orthographicSize - paddleHeightHalf;
    }

    private void Update()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * yInput * speed * Time.deltaTime);

        if (transform.position.y > yBounds)
        {
            transform.position = new Vector2(transform.position.x, yBounds);
        } else if (transform.position.y < -yBounds)
        {
            transform.position = new Vector2(transform.position.x, -yBounds);
        }
    }
}
