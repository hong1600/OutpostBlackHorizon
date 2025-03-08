using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;

    Camera mainCam;
    Rigidbody rigid;
    CapsuleCollider cap;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    Vector3 moveDir;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] int jumpCount = 0;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float fallGravity = 0.5f;
    [SerializeField] float maxFallSpeed = 50f;

    [SerializeField] float slopeLimit = 45f;

    [SerializeField] internal bool isGround = false;
    [SerializeField] internal bool isRun = false;
    [SerializeField] bool isJump = false;

    private void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        CheckInput();
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
        SetGravity();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRun = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRun = false;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpCount++;
            Jump();
        }
    }

    private void CheckGround()
    {
        if (Physics.SphereCast(cap.bounds.center, cap.radius, Vector3.down,
            out RaycastHit hit, cap.bounds.extents.y + 0.2f , LayerMask.GetMask("Ground"))
            && rigid.velocity.y <= 0.5f)
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
        }
        else
        {
            isGround = false;
        }
    }

    private void CheckSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(cap.bounds.center, Vector3.down, out hit,
            cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground")))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if(slopeAngle > slopeLimit) 
            {
                rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            }
        }
    }

    private void SetGravity()
    {
        if (!isGround) 
        {
            fallGravity -= gravity * Time.fixedDeltaTime;
            rigid.velocity += Vector3.up * gravity * Time.fixedDeltaTime;

            if (rigid.velocity.y < 0)
            {
                rigid.velocity += Vector3.up * gravity * (fallGravity - 1) * Time.fixedDeltaTime;
            }

            if (rigid.velocity.y < -maxFallSpeed)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, -maxFallSpeed, rigid.velocity.z);
            }
        }
    }

    private void Move()
    {
        if (playerStat.isDie || isJump) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDir.magnitude > 0)
        {
            Quaternion cameraRot = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
            moveDir = cameraRot * inputDir;

            float speed = isRun ? runSpeed : walkSpeed;

            rigid.velocity = new Vector3(moveDir.x * speed, rigid.velocity.y, moveDir.z * speed);
        }
        else
        {
            moveDir = Vector3.zero;
        }

        if (Shared.playerManager.anim != null)
        {
            Shared.playerManager.anim.SetFloat("Horizontal", moveX);
            Shared.playerManager.anim.SetFloat("Vertical", moveZ);
        }
    }

    private void Jump()
    {
        if (jumpCount == 0)
        {
            fallGravity = 0.5f;
            isJump = true;

            float angle = Vector3.Angle(moveDir, Vector3.up);

            Vector3 jumpDir = (moveDir + Vector3.up).normalized;

            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
        }
        else if (jumpCount == 1)
        {
            fallGravity = 0.5f;
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
