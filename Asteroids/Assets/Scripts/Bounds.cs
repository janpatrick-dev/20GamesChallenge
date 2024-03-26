using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField]
    private Vector2 boundsXY;
    
    // Start is called before the first frame update
    void Start()
    {
        var aspect = Camera.main.aspect;
        var orthographicSize = Camera.main.orthographicSize;
        boundsXY.x = aspect * orthographicSize;
        boundsXY.y = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleBounds();
    }
    
    private void HandleBounds()
    {
        var myTf = transform;
        var myPos = myTf.position;
        var myScale = myTf.localScale;
        var myHeight = myScale.y;
        var myWidth = myScale.x;

        var offsetX = boundsXY.x + myWidth;
        var offsetY = boundsXY.y + myHeight;
        
        if (myPos.y > offsetY)
        {
            myTf.position = new Vector2(myPos.x, -offsetY);
        }
        else if (myPos.y < -offsetY)
        {
            myTf.position = new Vector2(myPos.x, offsetY);
        }
        else if (myPos.x > offsetX)
        {
            myTf.position = new Vector2(-offsetX, myPos.y);
        }
        else if (myPos.x < -offsetX)
        {
            myTf.position = new Vector2(offsetX, myPos.y);
        }
    }
}
