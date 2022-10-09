using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActions : MonoBehaviour
{
    private Rigidbody2D characterRigidbody2D;
    private float direction = 0f;
    private bool lPressed = false; //向左走是否被按下
    private bool rPressed = false; //向右走是否被按下
    private Ray2D checkGround;
    private Animator characterAnimator;
    private int preState;

    public float speed = 1000f;
    public Transform tf;
    public bool isFalling = false;
    public PhysicsMaterial2D p1;
    public PhysicsMaterial2D p2;
    public GameObject Bow;

    private void Awake()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        characterRigidbody2D.velocity =
            new Vector2((speed * direction * Time.deltaTime), characterRigidbody2D.velocity.y);

    }

    private void Update()
    {
        if (characterRigidbody2D.velocity == Vector2.zero)
        {
            /* Debug.Log("0");
              if(characterAnimator.GetInteger("AnimationState")!=0)
             characterAnimator.SetInteger("AnimationState",0);*/
        }
    }

    //跳跃
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            preState = characterAnimator.GetInteger("AnimationState");
            if (isFalling == false)
            {
                characterRigidbody2D.AddForce(Vector2.up * 5.1f, ForceMode2D.Impulse);
                if (characterAnimator.GetInteger("AnimationState") != 3)
                {
                    characterAnimator.SetInteger("AnimationState", 3);
                }

            }


        }

        if (context.canceled)
        {
            characterAnimator.SetInteger("AnimationState", preState);
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
            if (characterAnimator.GetInteger("AnimationState") != 1)
            {
                characterAnimator.SetInteger("AnimationState", 1);
            }


        }

        //处理左右一起按住时的情况
        if (context.canceled)
        {
            rPressed = false;
            if (lPressed == true)
                direction = -1f;
            else
            {
                characterAnimator.SetInteger("AnimationState", 0);
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
            if (characterAnimator.GetInteger("AnimationState") != 2)
            {
                characterAnimator.SetInteger("AnimationState", 2);
                // Bow.transform.position -= new Vector3(0.4f, 0, 0);
                // Bow.transform.Rotate(0,180,0);
            }


        }

        //处理左右一起按住时的情况
        if (context.canceled)
        {
            //Bow.transform.position += new Vector3(0.4f, 0, 0);
            lPressed = false;
            if (rPressed == true)
                direction = 1f;
            else
            {
                characterAnimator.SetInteger("AnimationState", 0);
                direction = 0f;
            }


        }
    }

    public void SwitchArrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponentInChildren<Bow>().onFire = !GetComponentInChildren<Bow>().onFire;
            Debug.Log("Switch!");
        }

    }
    
}

