using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] 
    private float delayInSeconds = 2;
    
    void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(gameObject);
    }
}
