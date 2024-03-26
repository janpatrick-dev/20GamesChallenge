using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject brickPrefab;

    [SerializeField]
    private int rows = 3;
    
    [SerializeField]
    private int columns = 3;

    private void Start()
    {
        for (int r = 0; r < rows; r++)
        {
            var y = 1 + (r * 0.25f);
            for (int c = 0; c < columns; c++)
            {
                var x = c * 1.05f;
                var newBrick = Instantiate(brickPrefab, new Vector2(x, y), Quaternion.identity);
                newBrick.transform.parent = transform;
            }
        }
    }
}
