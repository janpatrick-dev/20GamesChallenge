using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadSpawner : Spawner
{
    [SerializeField] 
    private Ordinal blinkingLilyPadOrdinal;
    
    public override IEnumerator SpawnObjects()
    {
        isSpawning = true;
        yield return new WaitForSeconds(nextSpawnTimeInSeconds);
        
        var spawnerTf = transform;
        var spawnerPos = spawnerTf.position;
        
        for (int i = 0; i < spawnAmount; i++)
        {
            var newObject = Instantiate(prefab, spawnerPos, Quaternion.identity, spawnerTf);
            var spawnable = newObject.GetComponent<Spawnable>();
            spawnable.direction = spawnerPos.x > 0 ? Direction.Left : Direction.Right;
            spawnable.SetSpeed(objectSpeed);
            SetLilyPadAsDisappearing(i, ref newObject);
            yield return new WaitForSeconds(spawnIntervalInSeconds);
        }
        isSpawning = false;
    }

    private void SetLilyPadAsDisappearing(int index, ref GameObject lilyPadObject)
    {
        if (index != 0 && index != spawnAmount - 1)
            return;
        
        if (index == 0 && blinkingLilyPadOrdinal == Ordinal.LAST)
            return;

        if (index == spawnAmount-1 && blinkingLilyPadOrdinal == Ordinal.FIRST)
            return;
        
        var lilyPad = lilyPadObject.GetComponent<LilyPad>();
        lilyPad.Disappearing = true;
    }
}

enum Ordinal
{
    FIRST,
    LAST
}
