using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform mainCameraTransform;
    private Vector3  lastCameraTransform;
    private Vector3 deltaCameraTransform;

    public Camera mainCamera;
    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        transform.position = mainCameraTransform.position+Vector3.forward;
        lastCameraTransform = mainCameraTransform.position;
    }

    private void LateUpdate()
    {
        deltaCameraTransform = mainCameraTransform.position - lastCameraTransform;
        transform.position += deltaCameraTransform;
        lastCameraTransform = mainCamera.transform.position;
    }
}
