using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenager : MonoBehaviour
{   
    private InputMenager        inputMenager;
    private AnimationMenager    animationMenager;
    private LocomotionMenager   locomotionMenager;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        animationMenager = GetComponent<AnimationMenager>();
        locomotionMenager = GetComponent<LocomotionMenager>();
    }
    void Update()
    {
        inputMenager.HandleAllInputs();
        animationMenager.UpdateAnimatorValue();
        locomotionMenager.HandleAllLocomotion();
    }
}
