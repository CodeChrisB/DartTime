using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  // horizontal rotation speed
    public float horizontalSpeed = 1f;
    // vertical rotation speed
    public float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam;
    private PauseMenu pm;
    private bool zoom;
    void Start()
    {
        pm = (PauseMenu)GameObject.Find("GlobalScript").GetComponent(typeof(PauseMenu));
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
        if (pm.isPaused)
            return;

        if (Input.GetMouseButtonDown(1))
            ToggleZoom();

        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;
 
        yRotation += mouseX * (zoom ? 0.25f:1);
        xRotation -= mouseY * (zoom ? 0.25f : 1);
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        if (yRotation < -150)
            yRotation = -150;
        else if (yRotation > -30)
            yRotation = -30;


        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }

    private void ToggleZoom()
    {
        zoom = !zoom;
        cam.fieldOfView = zoom ? 20:60;
    }
}
