using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CameraMove : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Image customMouse;

    [SerializeField] float zoomSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;

    [SerializeField] float edgeDistance;
    [SerializeField] float cameraMoveSpeed;

    private void Start()
    {
        zoomSpeed = 10f;
        minZoom = 5f;
        maxZoom = 50f;
        edgeDistance = 50f;
        cameraMoveSpeed = 10f;
    }

    private void Update()
    {
        MoveHandle();
        ZoomHandle();
    }

    private void MoveHandle()
    {
        Vector3 mousePos = Shared.inputMng.iCustomMouse.GetMousePos();

        if (mousePos.x <= edgeDistance)
        {
            transform.Translate(Vector3.left * cameraMoveSpeed * Time.deltaTime, Space.World);
        }
        else if (mousePos.x >= Screen.width - edgeDistance)
        {
            transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime, Space.World);
        }

        if (mousePos.y <= edgeDistance)
        {
            transform.Translate(Vector3.back * cameraMoveSpeed * Time.deltaTime, Space.World);
        }
        else if (mousePos.y >= Screen.height - edgeDistance)
        {
            transform.Translate(Vector3.forward * cameraMoveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void ZoomHandle()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0) 
        {
            float newSize = cam.orthographic ? 
                cam.orthographicSize - scroll * zoomSpeed : cam.fieldOfView - scroll * zoomSpeed;

            newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

            if (cam.orthographic)
            {
                cam.orthographicSize = newSize;
            }
            else
            {
                cam.fieldOfView = newSize;
            }
        }
    }
}
