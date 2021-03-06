﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public float runSpeed = 40f;
    public Animator animator;
    public FixedJoystick joystick;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        joystick.AxisOptions = AxisOptions.Horizontal;
    }

    // Update is called once per frame
    void Update()
    {

        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = joystick.Horizontal * runSpeed;
        // Speed is the name of the parameter in the animator object
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)) ;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } 
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    // Use FixedUpdate for physics stuff
    private void FixedUpdate()
    {
        // Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false; 
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
}
