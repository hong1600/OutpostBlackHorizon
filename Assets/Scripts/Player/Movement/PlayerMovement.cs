using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected Camera mainCam;
    protected Rigidbody rigid;
    protected CapsuleCollider cap;

    protected InputManager inputManager;
    protected PlayerManager playerManager;
    protected PlayerStatus playerStatus;
    protected CameraFpsMove cameraFpsMove;
    protected CameraFpsZoom cameraFpsZoom;

    public bool isMove;
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float runSpeed = 10f;
    [SerializeField] protected float walkInterval = 0.5f;
    [SerializeField] protected float runInterval = 0.4f;
    protected float footstepTimer = 0f;
    protected Vector3 moveDir;
    protected internal bool isRun = false;
    protected float moveX;
    protected float moveZ;

    [SerializeField] protected float energyInterval = 0.1f;
    [SerializeField] protected float energyRecovery = 30;
    protected float energyTimer;
    protected bool isCanRun = true;
    bool prevRunState = false;

    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected int jumpCount = 0;
    protected bool isJump = false;

    [SerializeField] protected float gravity = -9.81f;
    [SerializeField] protected float fallGravity = 0.5f;
    [SerializeField] protected float maxFallSpeed = 50f;
    protected internal bool isGround = false;

    [SerializeField] protected float slopeLimit = 45f;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    protected virtual void Start()
    {
        inputManager = InputManager.instance;
        playerManager = GetComponent<PlayerManager>();
        playerStatus = playerManager.playerStatus;
        cameraFpsMove = CameraManager.instance.CameraFpsMove;
        cameraFpsZoom = CameraManager.instance.CameraFpsZoom;
    }

    protected virtual void Update()
    {
        CheckGround();

        if (inputManager.isInputLock) return;

        CheckInput();
        Run();
    }

    private void FixedUpdate()
    {
        SetGravity();

        if (inputManager.isInputLock) return;

        Move();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount++;
            Jump();
        }
    }

    protected virtual void Move()
    {
        if (playerStatus.isDie || isJump) return;

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDir.magnitude > 0)
        {
            isMove = true;

            Quaternion cameraRot = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
            moveDir = cameraRot * inputDir;

            float speed = isRun ? runSpeed : walkSpeed;
            rigid.velocity = new Vector3(moveDir.x * speed, rigid.velocity.y, moveDir.z * speed);

            if (isGround)
            {
                float footstepInterval = isRun ? runInterval : walkInterval;

                if (Time.time >= footstepTimer)
                {
                    footstepTimer = Time.time + footstepInterval;
                    AudioManager.instance.PlaySfx(ESfx.FOOTSTEP, transform.position, null);
                }
            }
        }
        else
        {
            moveDir = Vector3.zero;
            isMove = false;
        }

        if (playerManager.anim != null)
        {
            playerManager.anim.SetFloat("Horizontal", moveX);
            playerManager.anim.SetFloat("Vertical", moveZ);
        }

        if (isRun != prevRunState)
        {
            prevRunState = isRun;
            cameraFpsMove.SetRunFOV(isRun);
        }
    }


    private void CheckSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(cap.bounds.center, Vector3.down, out hit,
            cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground")))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle > slopeLimit)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            }
        }
    }

    private void CheckGround()
    {
        if (Physics.SphereCast(cap.bounds.center, cap.radius, Vector3.down,
            out RaycastHit hit, cap.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"))
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

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && playerStatus.curEnergy > 0 && isCanRun && !cameraFpsZoom.isZoom)
        {
            isRun = true;

            if (Time.time > energyTimer)
            {
                playerStatus.UseEnergy(1);
                energyTimer = Time.time + energyInterval;

                if (playerStatus.curEnergy <= 0)
                {
                    isCanRun = false;
                    isRun = false;
                }
            }
        }
        else
        {
            isRun = false;

            if (playerStatus.curEnergy >= energyRecovery)
            {
                isCanRun = true;
            }
        }

        if (Time.time > energyTimer && playerStatus.curEnergy < 100 && !isRun)
        {
            playerStatus.FillEnergy(1);
            energyTimer = Time.time + energyInterval;
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
