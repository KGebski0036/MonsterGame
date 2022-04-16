using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenager : MonoBehaviour
{
    InputMenager        inputMenager;
    PlayerLocomotion    playerLocomotion;

    private void Awake()
    {
        inputMenager = GetComponent<InputMenager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void Update()
    {
        inputMenager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleLocomotion();
    }
}
