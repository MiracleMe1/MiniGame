using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public ButtonStick buttonStick;
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 20f;        // Travel speed

    float horizontalMove = 0f;          // horizontal movement
    bool jump = false;                  // if you click jump
    bool attack = false;                // if clicked attack


    void Update()
    {
        if (!attack)
        {
            horizontalMove = buttonStick.GetAxisRaw(AxisRawStick.Horizontal) * runSpeed;    // get the movement from the stick and multiply by the speed of movement

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));      // set the movement in the animation

            animator.SetFloat("Gy", controller.getGy());                // set the fall speed in the animation
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    // attack call function
    public void onDownButton_Attack()
    {
        if (!attack && controller.IsGround())
        {
            attack = true;
            animator.SetBool("IsAttack", true);
        }
    }

    // jump call function
    public void onDownButton_Jump()
    {
        OnEndAttack();
        jump = true;
        animator.SetBool("IsJumping", true);
    }

    // stop animation jump
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    // stop attack animation
    public void OnEndAttack()
    {
        attack = false;
        animator.SetBool("IsAttack", false);
    }
}
