using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMenager : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] float      playerWalkSpeed;
    [SerializeField] float      turnSmoothTime;


    private CharacterController characterController;
    private InputMenager        inputMenager;
    private Vector3             moveDirection = Vector3.zero;
    private float               turnSmoothVelocity;
    private float               targetAngle;

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
        moveDirection.x = inputMenager.movmentInput.x;
        moveDirection.z = inputMenager.movmentInput.y;
        moveDirection.Normalize();

        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 velocity = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(velocity * playerWalkSpeed * Time.deltaTime);
        }
    }
}
