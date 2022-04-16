using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] float      movmentSpeed;
    [SerializeField] float      rottationSpeed;


    InputMenager inputMenager;

    Vector3     moveDirection;
    Vector3     lookingDirection;
    Rigidbody   playerRigidbody;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void HandleLocomotion()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = playerCamera.transform.forward * inputMenager.movmentInput.y;
        moveDirection += playerCamera.transform.right * inputMenager.movmentInput.x;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movmentSpeed;

        playerRigidbody.velocity = moveDirection;
    }

    private void HandleRotation()
    {
        lookingDirection = playerCamera.transform.forward * inputMenager.movmentInput.y;
        lookingDirection += playerCamera.transform.right * inputMenager.movmentInput.x;
        lookingDirection.Normalize();
        lookingDirection.y = 0;
        lookingDirection *= rottationSpeed;

        if (lookingDirection == Vector3.zero)
            lookingDirection = transform.forward;

        Quaternion lookingRotation = Quaternion.LookRotation(lookingDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, lookingRotation, rottationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
