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

    public float currentPlayerSpeed = 0;

    private CharacterController characterController;
    private InputMenager        inputMenager;
    private Vector3             moveDirection = Vector3.zero;
    private float               turnSmoothVelocity;
    private float               targetAngle;
    private float               targetSpeed;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        characterController = GetComponent<CharacterController>();
    }

    public void HandleAllLocomotion()
    {
        HandleRotation();
        HanleMovment();
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

    private void HanleMovment()
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

        characterController.Move(velocity * currentPlayerSpeed * Time.deltaTime);
    }
}
