using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;

    [SerializeField] 
    protected int spawnAmount = 1;

    [SerializeField] 
    protected float spawnIntervalInSeconds = 1;

    [SerializeField] 
    protected float nextSpawnTimeInSeconds = 3;

    [SerializeField] 
    protected float objectSpeed = 5f;

    protected bool isSpawning;

    private void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnObjects());
        }
    }

    public virtual IEnumerator SpawnObjects()
    {
        isSpawning = true;
        yield return new WaitForSeconds(nextSpawnTimeInSeconds);
        
        var spawnerTf = transform;
        var spawnerPos = spawnerTf.position;
        
        for (int i = 0; i < spawnAmount; i++)
        {
            // var prefabWidth = prefab.transform.localScale.x;
            // var isSpawnerPositionedLeft = spawnerPos.x < 0;

            // var offset = isSpawnerPositionedLeft ? -(prefabWidth * i) : prefabWidth * i;
            // var spawnPosition = new Vector2(spawnerPos.x + offset, spawnerPos.y);
            var newObject = Instantiate(prefab, spawnerPos, Quaternion.identity, spawnerTf);
            var spawnable = newObject.GetComponent<Spawnable>();
            spawnable.direction = spawnerPos.x > 0 ? Direction.Left : Direction.Right;
            spawnable.SetSpeed(objectSpeed);
            yield return new WaitForSeconds(spawnIntervalInSeconds);
        }
        isSpawning = false;
    }
}
