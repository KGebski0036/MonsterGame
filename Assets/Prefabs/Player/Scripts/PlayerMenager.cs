using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenager : MonoBehaviour
{
    InputMenager        inputMenager;
    PlayerLocomotion    playerLocomotion;
    AnimatorMenager     animatorMenager;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorMenager = GetComponent<AnimatorMenager>();
    }

    void Update()
    {
        inputMenager.HandleAllInputs();
        animatorMenager.UpdateAnimetedValue();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleLocomotion();
    }
}
