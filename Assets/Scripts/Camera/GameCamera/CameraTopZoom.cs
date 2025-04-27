using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CameraTopZoom : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] float zoomSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        zoomSpeed = 10f;
        minZoom = 5f;
        maxZoom = 50f;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0) 
        {
            float newSize = mainCam.orthographic ?
                mainCam.orthographicSize - scroll * zoomSpeed : mainCam.fieldOfView - scroll * zoomSpeed;

            newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

            if (mainCam.orthographic)
            {
                mainCam.orthographicSize = newSize;
            }
            else
            {
                mainCam.fieldOfView = newSize;
            }
        }
    }
}
