﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float normalSpeed=500f;
    [SerializeField] float sprintSpeed=2f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 500f;
    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask ground;

    [Header ("FOV")]
    [SerializeField] Camera playerCamera;
    private float baseFOV;
    private float sprintFOVModifier = 2f;

    [Header("Weapon Headbob")]
    [SerializeField] Transform weaponParent;
    private Vector3 weaponParentOrigin;
    private float idleCounter;
    private float movementCounter;
    private Vector3 targetWeaponHeadbobPosition;

    private Rigidbody rig;
    
    void Start()
    {
        baseFOV = playerCamera.fieldOfView;
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
        weaponParentOrigin = weaponParent.localPosition;
    }

    private void Update()
    {
        //movement along axes
        float xPos = Input.GetAxisRaw("Horizontal");
        float yPos = Input.GetAxisRaw("Vertical");



        //sprinting 
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKey(KeyCode.Space);

        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && yPos > 0 && !isJumping && isGrounded;  //to avoid sprinting backwards

        //weapon headbob
        if (xPos == 0 && yPos == 0)  //idle
        {
            Headbob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition,
                                                    targetWeaponHeadbobPosition,
                                                    Time.deltaTime * 2f);
        }
        else if (!isSprinting)  //normal running
        {
            Headbob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 3f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition,
                                                    targetWeaponHeadbobPosition,
                                                    Time.deltaTime * 6f);
        }
        else  //sprinting
        {
            Headbob(movementCounter, 0.15f, 0.075f);
            movementCounter += Time.deltaTime * 7f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition,
                                                    targetWeaponHeadbobPosition,
                                                    Time.deltaTime * 10f);
        }
    }

    //if movement isnt proper move few statements to update()
    void FixedUpdate()
    {
        //movement along axes
        float xPos = Input.GetAxisRaw("Horizontal");
        float yPos = Input.GetAxisRaw("Vertical");

        

        //sprinting 
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKey(KeyCode.Space);

        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump&&isGrounded;
        bool isSprinting = sprint && yPos > 0 && !isJumping&&isGrounded;  //to avoid sprinting backwards

        if(isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }

        

        //speed modifier for while sprinting
        float modifiedSpeed = normalSpeed;
        if(isSprinting)
        {
            modifiedSpeed *= sprintSpeed;
        }

        Vector3 movementDirection = new Vector3(xPos, 0f, yPos).normalized;
        Vector3 targetVelocity = transform.TransformDirection(movementDirection) * modifiedSpeed * Time.deltaTime;
        targetVelocity.y = rig.velocity.y;
        rig.velocity = targetVelocity;

        //FOV change while sprinting
        if(isSprinting)
        {
            playerCamera.fieldOfView = Mathf.Lerp(baseFOV, baseFOV * sprintFOVModifier, Time.deltaTime* 10f);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, baseFOV,Time.deltaTime* 10f);
        }
    }

    void Headbob(float x,float x_intensity,float y_intensity)
    {
        targetWeaponHeadbobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(x) * x_intensity,
                                                 Mathf.Sin(x*2) * y_intensity,
                                                 0f);
    }
}
