using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurretMove : MonoBehaviour
{
    Camera mainCam;

    ViewState viewState;
    CameraTopToFps topToFps;

    public GameObject turretObj;
    public GameObject shootObj;
    [SerializeField] float mouseSpeed = 2f;
    float verticalRotation = 0f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        viewState = GameManager.instance.ViewState;
        topToFps = CameraManager.instance.CameraTopToFps;
    }

    private void LateUpdate()
    {
        LookMouse();
    }

    private void LookMouse()
    {
        if (viewState.CurViewState == EViewState.TURRET && topToFps.isArrive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            turretObj.transform.Rotate(Vector3.up * mouseX * mouseSpeed);

            verticalRotation -= mouseY * mouseSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 40f);

            shootObj.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
