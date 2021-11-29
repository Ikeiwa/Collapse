using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraCopy : MonoBehaviour
{
    private Camera cam;
    public Camera target;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.localPosition = target.transform.localPosition;
        transform.localRotation = target.transform.localRotation;
        cam.fieldOfView = target.fieldOfView;
    }
}
