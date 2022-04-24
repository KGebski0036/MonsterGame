using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetMovment : MonoBehaviour
{
    GameObject opositeOfCamera;
    void Awake()
    {
        opositeOfCamera = GameObject.Find("OpositeOfCamera");

    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, opositeOfCamera.transform.position, 0.05f);
    }
}
