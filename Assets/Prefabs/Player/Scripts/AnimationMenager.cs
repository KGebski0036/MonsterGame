using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenager : MonoBehaviour
{
    [SerializeField] float smoothOfAnimationTransition;

    private Animator        animator;
    private InputMenager    input;
    private int             horizontalHash;
    private int             verticalHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<InputMenager>();
        horizontalHash = Animator.StringToHash("HorizontalSpeed");
        verticalHash = Animator.StringToHash("VerticalSpeed");
    }
    public void UpdateAnimatorValue()
    {
        UpdateMovmentAnimatorValue();
    }

    private void UpdateMovmentAnimatorValue()
    {
        animator.SetFloat(horizontalHash, Mathf.Abs(input.movmentInput.y), smoothOfAnimationTransition, Time.deltaTime);
        animator.SetFloat(verticalHash, Mathf.Abs(input.movmentInput.x), smoothOfAnimationTransition, Time.deltaTime);
    }
}
