using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public GameObject character;

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Enter");
        if (collider.CompareTag("Ground")||collider.CompareTag("Box"))
        {
            character.GetComponent<InputActions>().isFalling = false;
        } 
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground")||other.CompareTag("Box"))
        {
            character.GetComponent<InputActions>().isFalling = false;
        } 
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
       // Debug.Log("leave");
        if (collider.CompareTag("Ground")||collider.CompareTag("Box"))
        {
           // Debug.Log("leavecorrect");
            character.GetComponent<InputActions>().isFalling = true;  
        }  
    }
    
}
