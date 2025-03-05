using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CameraFpsShake cameraFpsShake;
    [SerializeField] CameraFpsZoom cameraFpsZoom;
    [SerializeField] CameraTopMove cameraTopMove;
    [SerializeField] CameraTopZoom cameraTopZoom;
    [SerializeField] CameraTopToFps cameraTopToFps;

    public CameraFpsShake getCameraFpsShake { get; private set; }
    public CameraFpsZoom getCameraFpsZoom { get; private set; }
    public CameraTopMove getCameraTopMove { get; private set; }
    public CameraTopZoom getCameraTopZoom { get; private set; }
    public CameraTopToFps getCameraTopToFps { get; private set; }


    private void Awake()
    {
        Shared.cameraManager = this;

        getCameraFpsShake = cameraFpsShake;
        getCameraFpsZoom = cameraFpsZoom;
        getCameraTopMove = cameraTopMove;
        getCameraTopZoom = cameraTopZoom;
        getCameraTopToFps = cameraTopToFps;
    }

    private void Update()
    {
        CursorLock();
    }

    private void CursorLock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
