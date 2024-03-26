using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float destroyDelayInSeconds = 1f;

    private Vector3 _direction;

    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }
    
    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyDelayInSeconds);
        Destroy(gameObject);
    }

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
