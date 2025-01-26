using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] GameObject player;
    [SerializeField] Transform cameraTrs;
    [SerializeField] Transform top;
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject customMouse;
    [SerializeField] GameObject aimMouse;
    [SerializeField] GameObject functionUI;

    NormalPlayer normalPlayer;

    Vector3 velocity = Vector3.zero;

    bool isFps = false;
    bool isArrive = true;

    private void Awake()
    {
        mainCam = Camera.main;

        normalPlayer = player.GetComponent<NormalPlayer>();
        normalPlayer.enabled = false;
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
            customMouse.SetActive(true);
            aimMouse.SetActive(false);
            functionUI.SetActive(true);

            normalPlayer.enabled = false;

            isFps = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !isFps && isArrive)
        {
            mainCam.transform.SetParent(player.transform);
            rifle.SetActive(true);
            customMouse.SetActive(false);
            aimMouse.SetActive(true);
            functionUI.SetActive(false);

            normalPlayer.enabled = true;

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
            MoveCamera(cameraTrs);
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
