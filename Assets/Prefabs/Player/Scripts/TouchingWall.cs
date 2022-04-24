using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchingWall : MonoBehaviour
{
    public event EventHandler detectWall;

    private void OnCollisionStay(Collision collision)
    {
        detectWall?.Invoke(this, EventArgs.Empty);
    }

}
