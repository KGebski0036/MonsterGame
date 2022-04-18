using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenager : MonoBehaviour
{
    [SerializeField] float smoothOfAnimationTransition;

    private Animator            animator;
    private LocomotionMenager   locomotion;

    private int                 horizontalHash;
    private int                 verticalHash;
    private int                 isInteractingHash;
    private int                 fallingHash;
    private int                 landingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        locomotion = GetComponent<LocomotionMenager>();

        horizontalHash = Animator.StringToHash("HorizontalSpeed");
        verticalHash = Animator.StringToHash("VerticalSpeed");
        isInteractingHash = Animator.StringToHash("isInteracting");
        fallingHash = Animator.StringToHash("Falling");
        landingHash = Animator.StringToHash("Landing");
    }
    public void UpdateAnimatorValue()
    {
        UpdateMovmentAnimatorValue();
        UpdateFallingAndLandingAnimatorValue();
    }

    public void PlayTargetAnimation(int animationHash, bool isInteractive)
    {
        animator.SetBool(isInteractingHash, isInteractive);
        animator.CrossFade(animationHash, smoothOfAnimationTransition);
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
        if (locomotion.justLand)
        {
            PlayTargetAnimation(landingHash, true);
        }
    }
}
