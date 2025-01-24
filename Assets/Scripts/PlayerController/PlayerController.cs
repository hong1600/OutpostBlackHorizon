using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerAnim
{
    IDLE = 0,
    RUN = 1,
    JUMP = 2,
    LAND = 3,
    ATTACK = 4,
    DIE = 5,
}

public class PlayerController : MonoBehaviour
{
    Animator anim;
    CapsuleCollider cap;
    Rigidbody rigid;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] float runSpeed;
    [SerializeField] bool isGround;
    [SerializeField] bool isJump;
    [SerializeField] bool isAttack;
    [SerializeField] bool isRun;

    float moveX;
    float moveZ;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        moveSpeed = 5;
        isJump = false;
        jumpForce = 5;
        isGround = false;
        isAttack = false;
        rotationSpeed = 1;
        attackRange = 1f;
        attackDamage = 10f;
        runSpeed = 10f;
    }

    private void Update()
    {
        LockCursor();

        if (Shared.playerMng.isDie) return;

        Move();
        Turn();
        Attack();
        CheckGround();
        Jump();
        Run();
    }

    private void Move()
    {
        if (isAttack == false && isJump == false)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(moveX, 0, moveZ).normalized;

            if (dir.magnitude > 0 && !isRun)
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
            else if(dir.magnitude > 0 && isRun)
            {
                transform.Translate(dir * runSpeed * Time.deltaTime);
            }

            anim.SetFloat("Horizontal", moveX);
            anim.SetFloat("Vertical", moveZ);
        }
    }

    private void Run()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = true;
            ChangeAnim(EPlayerAnim.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            ChangeAnim(EPlayerAnim.IDLE);
        }
    }

    private void Turn()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttack && !isJump)
        {
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        ChangeAnim(EPlayerAnim.ATTACK);

        RaycastHit hit;

        if (Physics.Raycast(cap.bounds.center, transform.forward, out hit, attackRange, LayerMask.GetMask("Enemy")))
        {
            hit.collider.gameObject.GetComponent<ITakeDmg>().TakeDmg(attackDamage);
        }

        yield return new WaitForSeconds(1f);

        isAttack = false;
        ChangeAnim(EPlayerAnim.IDLE);
    }

    private void Jump()
    {
         if (Input.GetKeyDown(KeyCode.Space) && isGround && !isAttack)
         {
            rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
         }
    }

    private void ChangeAnim(EPlayerAnim eAnim)
    {
        switch (eAnim)
        {
            case EPlayerAnim.IDLE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.IDLE);
                break;
            case EPlayerAnim.RUN:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.RUN);
                break;
            case EPlayerAnim.JUMP:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.JUMP);
                break;
            case EPlayerAnim.LAND:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.LAND);
                break;
            case EPlayerAnim.ATTACK:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.ATTACK);
                break;
            case EPlayerAnim.DIE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.DIE);
                break;
        }
    }

    private void CheckGround()
    {
        if (Physics.Raycast(cap.bounds.center, Vector3.down,
            cap.bounds.extents.y + 0.1f , LayerMask.GetMask("Ground")))
        {
            if (!isGround)
            {
                ChangeAnim(EPlayerAnim.IDLE);
            }
            isGround = true;
            isJump = false;
        }
        else
        {
            isGround = false;
            isJump = true;
            ChangeAnim(EPlayerAnim.JUMP);
        }
    }
    private void LockCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
