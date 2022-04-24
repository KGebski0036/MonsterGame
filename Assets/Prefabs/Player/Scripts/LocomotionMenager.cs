using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMenager : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField] public float playerSprintSpeed;

    [SerializeField] float walkingSmoothnes;
    [SerializeField] float playerWalkSpeed;

    [Header("Rotating")]
    [SerializeField] float      turnSmoothTime;

    [Header("FallingAndLanding")]
    [SerializeField] float      fallingSpeed;
    [SerializeField] float      groundRaycastOffset;
    [SerializeField] float      groundraycastLength;
    [SerializeField] LayerMask  groundLayer;
    [SerializeField] float      slipOfEdgeForce;

    [Header("Jumping")]
    [SerializeField] int        maxNumberOfJumps;
    [SerializeField] float      jumpForce;
    [SerializeField] float      jumpSmoothnes;

    [Header("Objects")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] TouchingWall wallDetector;

    [HideInInspector]
    public float    currentPlayerSpeed = 0;
    public bool     isOnGround;
    public bool     justLand;
    public bool     justStartJump;
    public bool     isJumping;
    public float    timeInAir = 0;

    private InputMenager        inputMenager;
    private AnimationMenager    animationMenager;
    private Rigidbody           playerRigidbody;
    private Vector3             moveDirection = Vector3.zero;
    private Vector3             moveVelocity;
    private Vector3             verticalVelocity;
    private float               turnSmoothVelocity;
    private float               targetAngle;
    private float               targetSpeed;
    private float               jumpMultipler = 0;  
    private float               currentFallingSpeed = 0;  
    private bool                isMoving;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        playerRigidbody = GetComponent<Rigidbody>();
        animationMenager = GetComponent<AnimationMenager>();

        wallDetector.detectWall += HandleTouchingWall;
    }

    public void HandleAllLocomotion()
    {
        HandleLocomotionStatus();

        HandleRotation();

        HandleMovment();

        HandleFallingAndLanding();
    }

    public void HandleJumping()
    {
        if (isOnGround)
        {
            inputMenager.jumpingInput = false;
            verticalVelocity = Vector3.up * jumpForce;
            jumpMultipler = 0;
        }
    }

    private void HandleLocomotionStatus()
    {
        isMoving = moveDirection.magnitude >= 0.1f;

        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += groundRaycastOffset;
        isOnGround = Physics.SphereCast(rayCastOrigin, groundraycastLength, Vector3.down, out hit, groundLayer);
    }

    private void HandleRotation()
    {
        if (isMoving)
        {
            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            targetAngle += playerCamera.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void HandleMovment()
    {
        moveDirection.x = inputMenager.movmentInput.x;
        moveDirection.z = inputMenager.movmentInput.y;
        moveDirection.Normalize();

        CalculateCurrentSpeed();

        moveVelocity = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        playerRigidbody.velocity = moveVelocity * currentPlayerSpeed;
    }

    private void CalculateCurrentSpeed()
    {
        targetSpeed = 0;

        if (isMoving)
        {
            targetSpeed = playerWalkSpeed;

            if (inputMenager.isSprinting)
            {
                targetSpeed = playerSprintSpeed;
            }
        }

        if (isOnGround)
            currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, targetSpeed, walkingSmoothnes);

    }

    private void HandleFallingAndLanding()
    {
        HandleStatusOfFalling();

        if (verticalVelocity.y > 0)
        {
            Jumping();
            currentFallingSpeed = fallingSpeed / 2;
        }
        else
        {
            Falling();   
        }

    }

    private void Falling()
    {
        isJumping = false;
        currentFallingSpeed = Mathf.Lerp(currentFallingSpeed, fallingSpeed, jumpSmoothnes);
        playerRigidbody.AddForce(Vector3.down * currentFallingSpeed * (timeInAir + 1));
    }

    private void Jumping()
    {
        isJumping = true;

        jumpMultipler = Mathf.Lerp(jumpMultipler, 1.1f, jumpSmoothnes);

        if (jumpMultipler < 1)
        {
            playerRigidbody.AddForce(verticalVelocity * jumpMultipler);
        }
        else
        {
            verticalVelocity += Vector3.down * fallingSpeed / 2;
            playerRigidbody.AddForce(verticalVelocity);
        }
    }

    private void HandleStatusOfFalling()
    {
        if (!isOnGround && !isJumping)
        {
            timeInAir += Time.deltaTime;
        }
        else if (timeInAir > 0)
        {
            Land(timeInAir);
            timeInAir = 0;
        }
    }

    private void Land(float timeInAir)
    {
        animationMenager.PlayLandAnimation(timeInAir);
    }

    private void HandleTouchingWall(object sender, EventArgs args)
    {
        Debug.Log("detect");
    }
}
