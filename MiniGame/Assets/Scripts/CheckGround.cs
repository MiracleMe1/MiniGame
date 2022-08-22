using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public GameObject character;
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Enter");
        if (collider.CompareTag("Ground"))
        {
            character.GetComponent<InputActions>().isFalling = false;
        } 
    }
    

    private void OnTriggerExit2D(Collider2D collider)
    {
       // Debug.Log("leave");
        if (collider.CompareTag("Ground"))
        {
           // Debug.Log("leavecorrect");
            character.GetComponent<InputActions>().isFalling = true;  
        }  
    }
    
}
