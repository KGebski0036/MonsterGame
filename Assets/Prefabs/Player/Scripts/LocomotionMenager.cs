using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMenager : MonoBehaviour
{
    [SerializeField] public float playerSprintSpeed;

    [SerializeField] GameObject playerCamera;
    [SerializeField] float      playerWalkSpeed;
    [SerializeField] float      turnSmoothTime;
    [SerializeField] float      walkingSmoothness;
    [SerializeField] float      fallingSpeed;
    [SerializeField] float      groundRaycastOffset;
    [SerializeField] float      groundraycastLength;
    [SerializeField] LayerMask  groundLayer;

    public float    currentPlayerSpeed = 0;
    public bool     isOnGround;
    public bool     justLand;

    private CharacterController characterController;
    private InputMenager        inputMenager;
    private Rigidbody           playerRigidbody;
    private Vector3             moveDirection = Vector3.zero;
    private float               turnSmoothVelocity;
    private float               targetAngle;
    private float               targetSpeed;
    private float               timeInAir = 0;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void HandleAllLocomotion()
    {
        HandleRotation();
        HandleMovment();

        HandleFallingAndLanding();
    }

    private void HandleRotation()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            targetAngle += playerCamera.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void HandleMovment()
    {
        targetSpeed = 0;

        moveDirection.x = inputMenager.movmentInput.x;
        moveDirection.z = inputMenager.movmentInput.y;
        moveDirection.Normalize();

        if (moveDirection.magnitude >= 0.1f)
        {
            targetSpeed = playerWalkSpeed;

            if (inputMenager.isSprinting)
            {
                targetSpeed = playerSprintSpeed;
            }
        }

        Vector3 velocity = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, targetSpeed, walkingSmoothness);

        playerRigidbody.velocity = velocity * currentPlayerSpeed;

    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += groundRaycastOffset;

        isOnGround = Physics.SphereCast(rayCastOrigin, groundraycastLength, Vector3.down, out hit, groundLayer);

        if (!isOnGround)
        {
            timeInAir += Time.deltaTime;
            playerRigidbody.AddForce(Vector3.down * fallingSpeed * timeInAir * timeInAir);

        }
        else if (timeInAir > 0)
        {
            timeInAir = 0;
            justLand = true;
        }
        else
        {
            justLand = false;
        }
    }
}
