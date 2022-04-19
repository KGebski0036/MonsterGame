using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenager : MonoBehaviour
{
    [SerializeField] float smoothOfAnimationTransition;
    [SerializeField] float maxSoftLanding;
    [SerializeField] float maxNormalLanding;

    private Animator            animator;
    private LocomotionMenager   locomotion;

    private int                 horizontalHash;
    private int                 verticalHash;
    private int                 isInteractingHash;
    private int                 fallingHash;
    private int                 normalLandingHash;
    private int                 softLandingHash;
    private int                 hardLandingHash;
    private int                 isJumpingHash;
    private int                 jumpingHash;
    private int                 isGrounded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        locomotion = GetComponent<LocomotionMenager>();

        horizontalHash = Animator.StringToHash("HorizontalSpeed");
        verticalHash = Animator.StringToHash("VerticalSpeed");
        isInteractingHash = Animator.StringToHash("isInteracting");
        fallingHash = Animator.StringToHash("Falling");
        normalLandingHash = Animator.StringToHash("NormalLanding");
        softLandingHash = Animator.StringToHash("SoftLanding");
        hardLandingHash = Animator.StringToHash("HardLanding");
        isJumpingHash = Animator.StringToHash("isJumping");
        jumpingHash = Animator.StringToHash("Jumping");
        isGrounded = Animator.StringToHash("isGrounded");
    }
    public void UpdateAnimatorValue()
    {
        UpdateMovmentAnimatorValue();
        UpdateFallingAndLandingAnimatorValue();
        UpadateJumpingAnimatorValues();
    }

    public void PlayTargetAnimation(int animationHash, bool isInteractive)
    {
        animator.SetBool(isInteractingHash, isInteractive);
        animator.CrossFade(animationHash, smoothOfAnimationTransition);
    }

    public void PlayLandAnimation(float strengthOfLand)
    {

        if (IsSoftLanding(strengthOfLand))
        {
            PlayTargetAnimation(softLandingHash, false);
        }
        else if (IsNormalLanding(strengthOfLand))
        {
            PlayTargetAnimation(normalLandingHash, true);
        }
        else
        {
            PlayTargetAnimation(hardLandingHash, true);
        }

    }

    private void UpdateMovmentAnimatorValue()
    {
        float speed = (locomotion.currentPlayerSpeed / locomotion.playerSprintSpeed) * 1.5f;
        animator.SetFloat(horizontalHash, speed, smoothOfAnimationTransition, Time.deltaTime);
        animator.SetFloat(verticalHash, speed, smoothOfAnimationTransition, Time.deltaTime);
    }

    private void UpdateFallingAndLandingAnimatorValue()
    {
        if (!locomotion.isOnGround && !animator.GetBool(isInteractingHash))
        {
            PlayTargetAnimation(fallingHash, true);
        }
    }

    private bool IsSoftLanding(float strengthOfLand)
    {
        return strengthOfLand < maxSoftLanding;
    }
    private bool IsNormalLanding(float strengthOfLand)
    {
        return strengthOfLand < maxNormalLanding;
    }
    public bool IsInteracting()
    {
        return animator.GetBool(isInteractingHash);
    }
    private void UpadateJumpingAnimatorValues()
    {
        if(locomotion.justStartJump)
        {
            locomotion.justStartJump = false;
            animator.SetBool(isJumpingHash, true);
            PlayTargetAnimation(jumpingHash, false);
        }
        animator.SetBool(isGrounded, locomotion.isOnGround);
    }
}
