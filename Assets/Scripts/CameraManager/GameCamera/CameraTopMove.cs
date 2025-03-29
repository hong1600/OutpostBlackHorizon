using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTopMove : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] Image customMouse;

    [SerializeField] float edgeDistance;
    [SerializeField] float cameraMoveSpeed;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        edgeDistance = 50f;
        cameraMoveSpeed = 10f;
    }

    private void Move()
    {
        Vector3 mousePos = InputManager.instance.cursor.GetMousePos();

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
}
