using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : Spawnable
{
    public Material greenMat;
    public Material redMat;
    
    [SerializeField]
    private bool disappearing;

    [SerializeField] private float blinkDelay = 1;

    private BoxCollider _boxCollider;

    private void Start()
    {
        if (disappearing)
        {
            _boxCollider = GetComponent<BoxCollider>();
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        GetComponent<MeshRenderer>().material = redMat;
        yield return new WaitForSeconds(blinkDelay);
        GetComponent<MeshRenderer>().material = greenMat;
        yield return new WaitForSeconds(blinkDelay);
        yield return StartCoroutine(Blink());
    }

    public bool Disappearing
    {
        get => disappearing;
        set => disappearing = value;
    }
}
