using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float screenWidthHalfInWorldUnits;

    private void Start()
    {
        screenWidthHalfInWorldUnits = ScreenInfo.GetHalfWidth();
    }

    private void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var velocity = xInput * speed;
        var paddleHalfWidth = transform.localScale.x / 2;
        transform.Translate(Vector2.right * (velocity * Time.deltaTime));


        var leftBounds = -screenWidthHalfInWorldUnits + paddleHalfWidth;
        var rightBounds = screenWidthHalfInWorldUnits - paddleHalfWidth;
        if (transform.position.x < leftBounds)
        {
            transform.position = new Vector3(leftBounds, transform.position.y, transform.position.z);
        } else if (transform.position.x > rightBounds)
        {
            transform.position = new Vector3(rightBounds, transform.position.y, transform.position.z);
        }
    }
}
