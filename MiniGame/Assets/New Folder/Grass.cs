using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public int mass;


    private void Start()
    {
        gameObject.layer = 0;
    }

    void Update()
    {
        if (mass == 2)
        {
            gameObject.layer = 11;
           // gameObject.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
          //  GetComponent<Rigidbody2D>().mass = 100;
        }

        if (mass == 1)
        {
            gameObject.layer = 10;
            
          //  gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            //GetComponent<Rigidbody2D>().mass = 1;
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > 5f)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //GetComponent<Rigidbody2D>().mass = 100;

    }

    void Mass1()
    {
        
    }
}
