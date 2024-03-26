using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public Action OnBrickHit;
    
    [SerializeField] 
    private float speed = 5f;

    private GameObject _paddle;

    private Rigidbody _rb;
    private bool _isDeployed;
    
    private float _screenHalfWidthInWorldUnits;
    private float _screenHeightInWorldUnits;
    
    private Vector2 _direction;
    private Vector2 _ballHalfWidthHeight;

    private void Start()
    {
        _paddle = GameObject.FindGameObjectWithTag("Paddle");
        _rb = GetComponent<Rigidbody>();
        _direction = new Vector2(Random.Range(-1f, 1f), 1).normalized;
        _screenHalfWidthInWorldUnits = ScreenInfo.GetHalfWidth();
        _screenHeightInWorldUnits = ScreenInfo.GetHeight();
        _ballHalfWidthHeight = new Vector2(transform.localScale.x / 2, transform.localScale.y / 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isDeployed)
        {
            _isDeployed = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isDeployed)
        {
            transform.position = new Vector2(_paddle.transform.position.x, transform.position.y);
            return;
        }
        
        _rb.velocity = _direction.normalized * speed;
        CheckBounds();
    }

    private void CheckBounds()
    {
        var ballTf = transform;
        var leftBounds = -_screenHalfWidthInWorldUnits + _ballHalfWidthHeight.x;
        var rightBounds = _screenHalfWidthInWorldUnits - _ballHalfWidthHeight.x;
        var topBounds = _screenHeightInWorldUnits - _ballHalfWidthHeight.y;
        
        if (ballTf.position.x < leftBounds)
        {
            var offset = leftBounds + _ballHalfWidthHeight.x;
            ballTf.position = new Vector2(offset, ballTf.position.y);
            _direction = new Vector2(Mathf.Abs(_direction.x), _direction.y);
        } 
        if (transform.position.x > rightBounds)
        {
            var offset = rightBounds - _ballHalfWidthHeight.x;
            ballTf.position = new Vector2(offset, ballTf.position.y);
            _direction = new Vector2(-(Mathf.Abs(_direction.x)), _direction.y);
        }
        if (transform.position.y > topBounds)
        {
            var offset = topBounds - _ballHalfWidthHeight.y;
            ballTf.position = new Vector3(ballTf.position.x, offset);
            _direction = new Vector2(_direction.x, -(Mathf.Abs(_direction.y)));
        } 
    }

    public void ResetPosition()
    {
        var paddlePos = _paddle.transform.position;
        transform.position = new Vector2(paddlePos.x, paddlePos.y + transform.localScale.y);
        _isDeployed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            var ballTf = transform;
            var paddleTf = other.gameObject.transform;
            var ballDistanceToCenter = ballTf.position.x - paddleTf.position.x;
            var isBelowPaddle = ballTf.position.y < paddleTf.position.y;

            var x = Mathf.Clamp(ballDistanceToCenter, -1, 1);

            _direction = new Vector2(x, isBelowPaddle ? -Mathf.Abs(_direction.y) : Mathf.Abs(_direction.y));
        } 
        else if (other.gameObject.CompareTag("Brick"))
        {
            var ballTf = transform;
            var brick = other.gameObject;

            var isBelowBrick = ballTf.position.y < brick.transform.position.y;
            _direction = new Vector2(_direction.x, isBelowBrick ? -Math.Abs(_direction.y) : Math.Abs(_direction.y));
            
            OnBrickHit?.Invoke();
            Destroy(brick);
        }
    }
}
