using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenager : MonoBehaviour
{
    InputMenager        inputMenager;
    PlayerLocomotion    playerLocomotion;
    AnimatorMenager     animatorMenager;
    CameraMenager       cameraMenager;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorMenager = GetComponent<AnimatorMenager>();
        cameraMenager = FindObjectOfType<CameraMenager>();
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

    private void LateUpdate()
    {
        cameraMenager.HandleAllMovment();
    }
}
