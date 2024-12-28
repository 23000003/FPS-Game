using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements
{
    private float walkSpeed = 3.4f;
    private float runSpeed = 4.7f;
    private float rotationX = 0;
    private bool isOnGround = true; // new
    private readonly float jumpPower = 3.0f;
    private readonly float gravity = 10f;
    private readonly float lookSpeed = 1f;
    private readonly float lookXLimit = 50f;
    private readonly float defaultHeight = 2f;
    private readonly float crouchHeight = 1f;
    private readonly float crouchSpeed = 1.5f;
    private readonly bool canMove = true;
    private float jumpStartHeight = 0f; // Track the starting height when the jump begins
    private float previousHeight = 0f; // Store the previous height to calculate the velocity
    private float handsRotationX = -4f; // Default rotation for hands when grounded

    private Vector3 moveDirection = Vector3.zero;

    private readonly Camera playerCamera;
    private readonly CharacterController characterController;
    private readonly Transform transform;

    public bool GetIsOnGround() {  return isOnGround; }

    public PlayerMovements(CharacterController controller, Camera camera, Transform transform) {
        characterController = controller;
        playerCamera = camera;
        this.transform = transform;
    }

    public void ConfigCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MovementController()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        Jump(movementDirectionY);

        Crouch();

        characterController.Move(moveDirection * Time.deltaTime);

        Move();
    }

    private void Jump(float movementDirectionY)
    {
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            jumpStartHeight = transform.position.y;
            previousHeight = transform.position.y;
            isOnGround = false;
        }
        else
        {
            moveDirection.y = movementDirectionY; // Apply gravity when grounded
        }

        Transform hands;

        if (Player.Instance.GetWeaponSystem().GetWeaponSwitching().GetIsSecondary())
        {
            hands = GameObject.FindGameObjectWithTag("2ndWeapon").GetComponent<Transform>();
        }
        else
        {
            hands = FindActiveObjectWithTag("Weapon").GetComponent<Transform>();
        }

        Quaternion currentRotation = hands.localRotation;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; 

            float currentHeight = transform.position.y - jumpStartHeight;
            float heightDifference = currentHeight - previousHeight;

            if (currentHeight > 0) // Player is going up
            {
                handsRotationX = Mathf.Lerp(handsRotationX, -5f, Time.deltaTime * 6f); 
            }
            else // Player is falling down
            {
                handsRotationX = Mathf.Lerp(handsRotationX, -4f, Time.deltaTime * 6f); 
            }

            hands.localRotation = Quaternion.Euler(handsRotationX, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            previousHeight = currentHeight;
            isOnGround = false;

        }
        else
        {
            handsRotationX = Mathf.Lerp(handsRotationX, 0f, Time.deltaTime * 10f);
            hands.localRotation = Quaternion.Euler(handsRotationX, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);

            jumpStartHeight = 0f;
            previousHeight = 0f;
            isOnGround = true;
        }
    }

    private GameObject FindActiveObjectWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            if (obj.activeSelf) 
            {
                return obj;
            }
        }
        return null;
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 3.4f;
            runSpeed = 4.7f;
        }
    }

    private void Move()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }
}