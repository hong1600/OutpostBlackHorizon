using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    PlayerAI playerAI;

    Camera mainCam;
    Rigidbody rigid;
    CapsuleCollider cap;
    Animator anim;

    event Action onTakeDmg;

    [SerializeField] float maxHp = 100;
    [SerializeField] float curHp = 100;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] internal bool isAttack = false;
    [SerializeField] internal bool isGround = false;
    [SerializeField] internal bool isRun = false;
    [SerializeField] internal bool isDie = false;

    float moveX;
    float moveZ;
    [SerializeField] Transform fireTrs;

    public void InitPlayer()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        playerAI = new PlayerAI();
        playerAI.Init(this);
    }

    private void Update()
    {
        CheckInput();
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (isDie) return;
        Move();
    }

    private void CheckInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", moveX);
        anim.SetFloat("Vertical", moveZ);

        if (Input.GetKeyDown(KeyCode.LeftShift)) isRun = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRun = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();

        if (Input.GetMouseButtonDown(0) && !isAttack) Attack();
    }

    private void CheckGround()
    {
        isGround = Physics.Raycast(cap.bounds.center, Vector3.down,
            cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
    }

    private void Move()
    {
        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDir.magnitude > 0)
        {
            Vector3 cameraForward = mainCam.transform.forward;
            Vector3 cameraRight = mainCam.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDir = cameraForward * inputDir.z + cameraRight * inputDir.x;

            float speed = isRun ? runSpeed : walkSpeed;

            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }
    }

    private void Jump()
    {
        rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
    }

    protected virtual void Attack()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        isAttack = true;

        GameObject obj = Shared.objectPoolMng.iBulletPool.FindBullet(EBullet.BULLET);
        obj.transform.position = fireTrs.transform.position;
        obj.transform.rotation = fireTrs.rotation * Quaternion.Euler(0, 180, 0);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.InitBullet(null, 10, 0.1f);

        yield return new WaitForSeconds(0.1f);

        isAttack = false;
    }

    public void TakeDmg(float _dmg)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            onTakeDmg?.Invoke();
        }
        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine(StartDie());
        }
    }

    IEnumerator StartDie()
    {
        yield return new WaitForSeconds(1);

        Reset();
    }

    private void Reset()
    {
        isDie = false;
        playerAI.aiState = EPlayer.CREATE;
    }


    internal void ChangeAnim(EPlayer _ePlayer)
    {
        _ePlayer = playerAI.aiState;

        switch (_ePlayer)
        {
            case EPlayer.WALK:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.WALK);
                break;
            case EPlayer.RUN:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.RUN);
                break;
            case EPlayer.JUMP:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.JUMP);
                break;
            case EPlayer.LAND:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.LAND);
                break;
            case EPlayer.ATTACK:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.ATTACK);
                break;
            case EPlayer.DIE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.DIE);
                break;
        }
    }
}
