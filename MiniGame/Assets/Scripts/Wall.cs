using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class Wall : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public int height = 1;
    public bool isTriggered = false;
    public int direction = 1;
    public float moveTime = 1; 
    private Vector3 refspeed;

    private void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        if (gameObject.transform.localScale != new Vector3(1, height, 0))
        {
            gameObject.transform.localScale = new Vector3(1, height, 0);
        }

        if (isTriggered)
        {
            WallMove();
        }

        if (!isTriggered)
        {
            WallBack();
        }

    }

    private void WallMove()
    {
        
        transform.position=Vector3.Lerp(transform.position,targetPosition,0.01f);
           
    }

    private void WallBack()
    {
        transform.position=Vector3.Lerp(transform.position,startPosition,0.01f);
    }
    

}
