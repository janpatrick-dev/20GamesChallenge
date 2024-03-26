using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> objectsToSpawn;
    [SerializeField] private List<Transform> spawnPointsTf;

    [SerializeField] private float spawnDelayInSeconds = 1f;
    
    private void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        while (!gameManager.CanSpawnAsteroid())
        {
            yield return null;
        }
        
        var objectIndex = Random.Range(0, objectsToSpawn.Count);
        var objectToSpawn = objectsToSpawn[objectIndex];
        var spawnPointIndex = Random.Range(0, spawnPointsTf.Count);
        var spawnPoint = spawnPointsTf[spawnPointIndex];

        var asteroid = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        asteroid.transform.parent = spawnPoint;
        yield return new WaitForSeconds(spawnDelayInSeconds);
        StartCoroutine(SpawnAsteroid());
    }
}
