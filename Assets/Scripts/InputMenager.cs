using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour
{
    public Vector2 movmentInput;

    public void HandleAllInputs()
    {
        HandleMovmentInput();
    }
    private void HandleMovmentInput()
    {
        movmentInput.y = Input.GetAxis("Vertical");
        movmentInput.x = Input.GetAxis("Horizontal");
    }
}