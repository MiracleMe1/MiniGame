using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))//在此添加胜利后事件
        {
            Debug.Log("Win");
        }
    }
}
