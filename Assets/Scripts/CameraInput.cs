using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    public Vector2 mouseInput;

    public void HandleAllInputs()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        mouseInput.y = Input.GetAxis("Mouse Y");
        mouseInput.x = Input.GetAxis("Mouse X");
    }
}
