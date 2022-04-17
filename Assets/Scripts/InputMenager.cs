using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour
{
    public Vector2  movmentInput;
    public bool     isSprinting;

    public void HandleAllInputs()
    {
        HandleMovmentInput();
    }
    private void HandleMovmentInput()
    {
        movmentInput.y = Input.GetAxisRaw("Vertical");
        movmentInput.x = Input.GetAxisRaw("Horizontal");

        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }
}
