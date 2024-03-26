using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;

    [SerializeField] 
    private float xBounds = 10f;

    public Direction direction;

    private void Update()
    {
        var moveDirection = direction == Direction.Left ? Vector2.left : Vector2.right;
        transform.Translate(moveDirection * (speed * Time.deltaTime));

        var spawnableWidth = transform.localScale.x;
        var offset = xBounds + spawnableWidth;
        var isOutOfBounds =
            direction == Direction.Left ? transform.position.x < -offset : transform.position.x > offset;
        if (isOutOfBounds)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float amount)
    {
        this.speed = amount;
    }
}

public enum Direction
{
    Left,
    Right
}
