using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class DoorButton : MonoBehaviour
{
    public int numOfControllingItem = 1;
    public GameObject[] items;
    public Vector2 colliderOffset = new Vector2(0f, -1.5f);

    private bool isExit = false;
    private float exitTime = 0.5f;
    private void Awake()
    {
        numOfControllingItem = items.Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isExit = false;
        exitTime = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().offset+=colliderOffset;
        if(other.CompareTag("Player")||other.CompareTag("Arrow")||other.CompareTag("Box"))
            for (int i = 0; i < numOfControllingItem; i++)
            {
                Debug.Log(i);
                items[i].GetComponent<Wall>().isTriggered = true;
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isExit = true;
        for (int i = 0; i < numOfControllingItem; i++)
        {
                items[i].GetComponent<Wall>().isTriggered= false;
        }
    }
    private void Update()
            {
                if (isExit==true)
                {
                    exitTime -= Time.deltaTime;
                    if (exitTime<=0)
                    {
                        isExit = false;
                        gameObject.GetComponent<SpriteRenderer>().enabled = true;
                        gameObject.GetComponent<BoxCollider2D>().offset-=colliderOffset; 
                    } 
                }
            }
}
