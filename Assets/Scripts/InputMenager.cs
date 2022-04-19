using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour
{
    public Vector2  movmentInput;
    public bool     isSprinting;
    public bool     jumpingInput;

    private LocomotionMenager   locomotion;
    private AnimationMenager    animationMenager;

    private void Awake()
    {
        locomotion = GetComponent<LocomotionMenager>();
        animationMenager = GetComponent<AnimationMenager>();
    }
    public void HandleAllInputs()
    {
        HandleMovmentInput();
        HandleJumpInput();
    }
    private void HandleMovmentInput()
    {
        if (locomotion.isOnGround) //&& animationMenager.IsInteracting())
        {
            movmentInput.y = Input.GetAxisRaw("Vertical");
            movmentInput.x = Input.GetAxisRaw("Horizontal");
        }
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            locomotion.HandleJumping();
        }
    }
}
