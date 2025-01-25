using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] Transform player;
    [SerializeField] Transform top;
    [SerializeField] GameObject rifle;

    Vector3 velocity = Vector3.zero;

    bool isFps = false;
    bool isArrive = true;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        mainCam.transform.position = top.position;
        mainCam.transform.rotation = top.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isFps && isArrive)
        {
            mainCam.transform.SetParent(null);
            rifle.SetActive(false);

            isFps = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !isFps && isArrive)
        {
            mainCam.transform.SetParent(player.transform);
            rifle.SetActive(true);

            isFps = true;
        }

        if (!isFps)
        {
            isArrive = false;
            MoveCamera(top);
        }
        else
        {
            isArrive = false;
            MoveCamera(player);
        }
    }

    private void MoveCamera(Transform _trs)
    {
        Vector3 targetPosition = _trs.position;
        mainCam.transform.position = Vector3.SmoothDamp
            (mainCam.transform.position, targetPosition, ref velocity, 0.5f);

        Quaternion targetRot = _trs.rotation;
        mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation, targetRot, Time.deltaTime * 5f);

        if (Vector3.Distance(mainCam.transform.position, _trs.position) < 0.1f)
        {
            isArrive = true;
        }
    }
}
