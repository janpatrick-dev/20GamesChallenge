using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;

    [SerializeField] 
    private GameObject asteroidBitsPrefab;

    [SerializeField] 
    private int splitCount = 2;

    private Vector3 direction;

    private void Start()
    {
        direction = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            HandleSplit();
            Destroy(gameObject);
        }
    }

    private void HandleSplit()
    {
        if (!asteroidBitsPrefab)
        {
            return;
        }
        var asteroidTf = transform;
        var asteroidPos = asteroidTf.position;

        for (int i = 0; i < splitCount; i++)
        {
            Instantiate(asteroidBitsPrefab, asteroidPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}
