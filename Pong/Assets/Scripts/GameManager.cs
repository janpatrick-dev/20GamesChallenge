using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballObject;
    public GameObject player1Object;
    public GameObject player2Object;

    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    private Ball ball;
    private float screenHalfWidthInWorldSpace;
    private int scoreP1;
    private int scoreP2;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        screenHalfWidthInWorldSpace = Camera.main.aspect * Camera.main.orthographicSize;
    }

    private void Update()
    {
        var ballTf = ballObject.transform;
        var player1Tf = player1Object.transform;
        var player2Tf = player2Object.transform;

        if (ballTf.position.x < -screenHalfWidthInWorldSpace - player1Tf.localScale.x)
        {
            // Player 1 lost
            AddScore(true);
            ball.ResetPosition();
        }
        else if (ballTf.position.x > screenHalfWidthInWorldSpace + player2Tf.localScale.x)
        {
            // Playe 2 lost
            AddScore(false);
            ball.ResetPosition();
        }
    }

    private void AddScore(bool isPlayer2)
    {
        if (!isPlayer2)
        {
            scoreP1 += 1;
            scoreTextP1.text = scoreP1.ToString();
        }
        else
        {
            scoreP2 += 1;
            scoreTextP2.text = scoreP2.ToString();
        }
    }
}
