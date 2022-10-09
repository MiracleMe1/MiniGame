using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject rope;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Arrow"))
        {
            Destroy(rope);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            Destroy(rope);
        }
    }
}
