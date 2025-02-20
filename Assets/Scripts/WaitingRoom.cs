using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{
    Camera mainCam;
    Rigidbody rigid;
    CapsuleCollider cap;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    bool isRun;

    private void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Move();
        CursorLock();
        CheckInput();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if(inputDir.magnitude > 0) 
        {
            Quaternion cameraRot = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
            Vector3 moveDir = cameraRot * inputDir;

            float speed = isRun ? runSpeed : walkSpeed;

            rigid.velocity = new Vector3(moveDir.x * speed, rigid.velocity.y, moveDir.z * speed);
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        }
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

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.LeftShift)) isRun = true;
        if(Input.GetKeyUp(KeyCode.LeftShift)) isRun = false;
    }
}
