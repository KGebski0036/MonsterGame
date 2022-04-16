using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMenager : MonoBehaviour
{
    Animator        animator;
    InputMenager    inputMenager;
    int             horizontalHash;
    int             verticalHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputMenager = GetComponent<InputMenager>();
        horizontalHash = Animator.StringToHash("HorizontalSpeed");
        verticalHash = Animator.StringToHash("VerticalSpeed");
    }
    public void UpdateAnimetedValue()
    {
        animator.SetFloat(
            horizontalHash,
            Mathf.Abs(inputMenager.movmentInput.y),
            0.1f,
            Time.deltaTime
            );
        animator.SetFloat(
            verticalHash,
            Mathf.Abs(inputMenager.movmentInput.x),
            0.1f,
            Time.deltaTime
            );
    }
}
