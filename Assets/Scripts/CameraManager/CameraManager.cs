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

    public CameraFpsShake CameraFpsShake { get; private set; }
    public CameraFpsZoom CameraFpsZoom { get; private set; }
    public CameraTopMove CameraTopMove { get; private set; }
    public CameraTopZoom CameraTopZoom { get; private set; }
    public CameraTopToFps CameraTopToFps { get; private set; }


    private void Awake()
    {
        Shared.cameraManager = this;

        CameraFpsShake = cameraFpsShake;
        CameraFpsZoom = cameraFpsZoom;
        CameraTopMove = cameraTopMove;
        CameraTopZoom = cameraTopZoom;
        CameraTopToFps = cameraTopToFps;
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
