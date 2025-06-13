using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementBase : MonoBehaviour
{
    protected InputManager inputManager;
    protected PlayerManager playerManager;
    protected PlayerStatus playerStatus;
    protected Camera mainCam;
    protected Rigidbody rigid;
    protected CapsuleCollider cap;

    public bool isMove;
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float runSpeed = 10f;
    [SerializeField] protected float walkInterval = 0.5f;
    [SerializeField] protected float runInterval = 0.4f;
    protected float footstepTimer = 0f;
    protected Vector3 moveDir;

    [SerializeField] protected float energyInterval = 0.1f;
    [SerializeField] protected float energyRecovery = 30;
    protected float energyTimer;
    protected bool isCanRun = true;

    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected int jumpCount = 0;

    [SerializeField] protected float gravity = -9.81f;
    [SerializeField] protected float fallGravity = 0.5f;
    [SerializeField] protected float maxFallSpeed = 50f;

    [SerializeField] protected float slopeLimit = 45f;

    [SerializeField] protected internal bool isGround = false;
    [SerializeField] protected internal bool isRun = false;
    [SerializeField] protected bool isJump = false;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    protected virtual void Start()
    {
        inputManager = InputManager.instance;
        playerManager = PlayerManager.instance;
        playerStatus = playerManager.playerStatus;
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

    protected abstract void Move();
    protected abstract void CheckGround();
    protected abstract void Run();
    protected abstract void SetGravity();
    protected abstract void Jump();
}
