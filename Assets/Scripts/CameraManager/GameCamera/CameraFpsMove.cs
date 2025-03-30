using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsMove : MonoBehaviour
{
    Camera mainCam;

    ViewState viewState;
    CameraTopToFps topToFps;

    [SerializeField] GameObject playerObj;
    [SerializeField] float mouseSpeed = 2f;
    float verticalRotation = 0f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        viewState = Shared.gameManager.ViewState;
        topToFps = Shared.cameraManager.CameraTopToFps;
    }

    private void LateUpdate()
    {
        LookMouse();
    }

    private void LookMouse()
    {
        if (viewState.CurViewState == EViewState.FPS && topToFps.isArrive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            playerObj.transform.Rotate(Vector3.up * mouseX * mouseSpeed);

            verticalRotation -= mouseY * mouseSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
            mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
