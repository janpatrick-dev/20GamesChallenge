using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    
    private int _asteroidCount;
    private const int AsteroidCountLimit = 10;

    public void IncrementAsteroidCount()
    {
        _asteroidCount += 1;
    }

    public void DecrementAsteroidCount()
    {
        _asteroidCount -= 1;
    }

    public bool CanSpawnAsteroid()
    {
        return _asteroidCount < AsteroidCountLimit;
    }
}
