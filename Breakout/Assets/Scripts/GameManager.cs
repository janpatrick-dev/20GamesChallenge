using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action OnBrickDestroy;

    [SerializeField]
    private int score;
    
    [SerializeField]
    private Transform ballTf;
    
    [SerializeField]
    private Transform paddle;

    private Ball ball;
    
    private float screenHeight;
    private float ballHalfHeight;
    
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        ball.OnBrickHit += IncrementScore;
        screenHeight = ScreenInfo.GetHeight();
        ballHalfHeight = transform.localScale.y / 2;
    }

    private void Update()
    {
        var ballPosY = ballTf.position.y;
        var bottomBounds = screenHeight + ballHalfHeight;
        
        if (ballPosY < -bottomBounds)
        {
            ball.ResetPosition();
        }
    }

    private void IncrementScore()
    {
        score += 1;
        print(score);
    }
}
