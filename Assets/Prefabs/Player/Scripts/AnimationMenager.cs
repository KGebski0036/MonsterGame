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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        locomotion = GetComponent<LocomotionMenager>();

        horizontalHash = Animator.StringToHash("HorizontalSpeed");
        verticalHash = Animator.StringToHash("VerticalSpeed");

    }
    public void UpdateAnimatorValue()
    {
        UpdateMovmentAnimatorValue();
    }

    private void UpdateMovmentAnimatorValue()
    {
        float speed = (locomotion.currentPlayerSpeed / locomotion.playerSprintSpeed) * 1.5f;
        animator.SetFloat(horizontalHash, speed, smoothOfAnimationTransition, Time.deltaTime);
        animator.SetFloat(verticalHash, speed, smoothOfAnimationTransition, Time.deltaTime);
    }
}
