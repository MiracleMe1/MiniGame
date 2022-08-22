using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActions : MonoBehaviour
{
    private Rigidbody2D characterRigidbody2D;
    private float direction = 0f;
    private bool lPressed = false;//向左走是否被按下
    private bool rPressed = false;//向右走是否被按下
    private Ray2D checkGround;

    public float speed = 1000f;
    public Transform tf;
    public bool isFalling = false;
    public PhysicsMaterial2D p1;
    public PhysicsMaterial2D p2;
    
    private void Awake() {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
        
        
    }

    private void FixedUpdate()
    {
        characterRigidbody2D.velocity=new Vector2((speed*direction*Time.deltaTime),characterRigidbody2D.velocity.y);
        
    }
    //跳跃
    public void Jump(InputAction.CallbackContext context) {
        if(context.performed)
        {
            
            if (isFalling == false)
            {
               characterRigidbody2D.AddForce(Vector2.up*5f, ForceMode2D.Impulse);
               
            }
             
            
         }
        
       
    }
    //向右走
    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rPressed = true;
        }
        if (context.performed)
        {
            direction = 1f;
            
        }
        //处理左右一起按住时的情况
        if (context.canceled)
        {
            rPressed = false;
            if(lPressed==true)
                direction = -1f;
            else
            {
                direction = 0f;
            }
            
            
        }
    }
    //向左走
    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lPressed = true;
        }
        if (context.performed)
        {
            direction = -1f;
           
        }
        //处理左右一起按住时的情况
        if (context.canceled)
        {
            lPressed = false;
            if(rPressed==true)
                direction = 1f;
            else
            {
                direction = 0f;
            }
            
            
        }
    }
    
}

