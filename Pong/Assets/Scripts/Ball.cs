using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody rb;
    private Vector2 direction;
    
    private float yBounds;
    private float ballHeightHalved;

    private bool isDeployed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        ballHeightHalved = transform.localScale.y;
        yBounds = Camera.main.orthographicSize - ballHeightHalved;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDeployed)
        {
            int x = Random.Range(0, 2) == 1 ? 1 : -1;
            int y = Random.Range(0, 2) == 1 ? 1 : -1;
            direction = new Vector2(x, y);
            isDeployed = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isDeployed)
        {
            return;
        }
        
        var ballTf = transform;
        var velocity = direction * speed;
        rb.MovePosition((Vector2) ballTf.position + velocity * Time.deltaTime);

        CheckWallCollision(ballTf);
    }

    public void ResetPosition()
    {
        transform.position = Vector2.zero;
        isDeployed = false;
    }

    private void CheckWallCollision(Transform ballTf)
    {
        if (ballTf.position.y > yBounds)
        {
            ballTf.position = new Vector2(ballTf.position.x, yBounds);
            direction = new Vector2(direction.x, -direction.y);
        } 
        else if (ballTf.position.y < -yBounds)
        {
            ballTf.position = new Vector2(ballTf.position.x, -yBounds);
            direction = new Vector2(direction.x, Math.Abs(direction.y));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            direction = new Vector2(-direction.x, direction.y);
            print(direction);
        }
    }
}
