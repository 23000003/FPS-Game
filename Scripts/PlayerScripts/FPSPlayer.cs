using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    private float walkSpeed;
    private float runSpeed;
    private float jumpPower;
    private float gravity;
    private float lookSpeed;
    private float lookXLimit;
    private float defaultHeight;
    private float crouchHeight;
    private float crouchSpeed;

    private float rotationX = 0;
    private bool canMove = true;

    private CharacterController characterController;
    private Camera playerCamera;
    private Vector3 moveDirection = Vector3.zero;

    public float GetRotationX() { return rotationX; }
    public bool GetCanMove() { return canMove; }
    public CharacterController GetCharController() { return characterController; }
    public Camera GetPlayerCamera() { return playerCamera; }
    public Vector3 GetMoveDirection() { return moveDirection; }

    public FPSPlayer(float walkSpeed, float runSpeed, float jumpPower, float gravity, float lookSpeed, float lookXLimit,
                    float defaultHeight, float crouchHeight, float crouchSpeed, CharacterController controller, Camera playerCamera)
    {
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.jumpPower = jumpPower;
        this.gravity = gravity;
        this.lookSpeed = lookSpeed;
        this.lookXLimit = lookXLimit;
        this.defaultHeight = defaultHeight;
        this.crouchHeight = crouchHeight;
        this.crouchSpeed = crouchSpeed;
        this.characterController = controller;
        this.playerCamera = playerCamera;
    }

    public void SetWalkSpeed(float walkSpeed) { this.walkSpeed = walkSpeed; }
    public void SetRunSpeed(float runSpeed) { this.runSpeed = runSpeed; }
    public void SetJumpPower(float jumpPower) { this.jumpPower = jumpPower; }
    public void SetGravity(float gravity) { this.gravity = gravity; }
    public void SetLookSpeed(float lookSpeed) { this.lookSpeed = lookSpeed; }
    public void SetLookXLimit(float lookXLimit) { this.lookXLimit = lookXLimit; }
    public void SetDefaultHeight(float defaultHeight) { this.defaultHeight = defaultHeight; }
    public void SetCrouchHeight(float crouchHeight) { this.crouchHeight = crouchHeight; }
    public void SetCrouchSpeed(float crouchSpeed) { this.crouchSpeed = crouchSpeed; }


    public float GetWalkSpeed() { return this.walkSpeed; }
    public float GetRunSpeed() { return this.runSpeed; }
    public float GetJumpPower() { return this.jumpPower; }
    public float GetGravity() { return this.gravity; }
    public float GetLookSpeed() { return this.lookSpeed; }
    public float GetLookXLimit() { return this.lookXLimit; }
    public float GetDefaultHeight() { return this.defaultHeight; }
    public float GetCrouchHeight() { return this.crouchHeight; }
    public float GetCrouchSpeed() { return this.crouchSpeed; }
}
