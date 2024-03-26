using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPrefab;
    
    [SerializeField] 
    private TextMeshProUGUI scoreText;

    private Player player;
    private int score;
    private int life = 3;

    private const int SCORE_TO_ADD = 10;
    private const int LIFE_TO_REMOVE = 1;

    private void Start()
    {
        SetPlayer();
    }

    private void SetPlayer()
    {
        player = FindObjectOfType<Player>();
        player.OnPlayerGoal += IncrementScore;
        player.OnPlayerGoal += RespawnPlayer;
        player.OnPlayerDeath += RespawnPlayer;
    }

    private void IncrementScore()
    {
        score += SCORE_TO_ADD;
        scoreText.text = "Score: " + score;
    }

    private void RespawnPlayer()
    { 
        Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        SetPlayer();
    }

    private void DecreaseLife()
    {
        life -= LIFE_TO_REMOVE;
    }
}
