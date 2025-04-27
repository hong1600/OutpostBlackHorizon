using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsZoom : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] GameObject rifle;
    [SerializeField] float normalFOV = 60;
    [SerializeField] float zoomFOV = 40f;
    [SerializeField] float zoomSpeed = 10f;
    [SerializeField] bool isZoom = false;
    [SerializeField] bool isZooming = false;
    GameObject scope;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        scope = GameUI.instance.scope;
        InputManager.instance.onRightClickDown += ZoomCamera;
    }

    private void ZoomCamera()
    {
        if(!isZooming) 
        {
            StartCoroutine(StartZoom());
        }
        
    }

    IEnumerator StartZoom()
    {
        isZooming = true;

        if (isZoom)
        {
            rifle.SetActive(true);
            scope.SetActive(false);
        }
        else
        {
            rifle.SetActive(false);
            scope.SetActive(true);
        }

        float targetFOV = isZoom ? normalFOV : zoomFOV;
        isZoom = !isZoom;

        while (Mathf.Abs(mainCam.fieldOfView - targetFOV) > 0.01f)
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        mainCam.fieldOfView = targetFOV;

        isZooming = false;
    }
}
