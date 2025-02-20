using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class PlayerMovement : MonoBehaviour
{
    Camera mainCam;
    Rigidbody rigid;
    [SerializeField] PlayerMng playerMng;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] internal bool isGround = false;
    [SerializeField] internal bool isRun = false;
    [SerializeField] internal bool isDie = false;

    private void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckInput();
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRun = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRun = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();
    }

    private void CheckGround()
    {
        isGround = Physics.Raycast(playerMng.cap.bounds.center, Vector3.down,
            playerMng.cap.bounds.extents.y + 0.3f, LayerMask.GetMask("Ground"));
    }

    private void Move()
    {
        if (isDie) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDir.magnitude > 0)
        {
            Quaternion cameraRot = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
            Vector3 moveDir = cameraRot * inputDir;

            float speed = isRun ? runSpeed : walkSpeed;

            rigid.velocity = new Vector3(moveDir.x * speed, rigid.velocity.y, moveDir.z * speed);
        }

        playerMng.anim.SetFloat("Horizontal", moveX);
        playerMng.anim.SetFloat("Vertical", moveZ);
    }

    private void Jump()
    {
        rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
    }

    internal IEnumerator StartDie()
    {
        yield return new WaitForSeconds(1);

        Reset();
    }

    private void Reset()
    {
        isDie = false;
        playerMng.playerAI.aiState = EPlayer.CREATE;
    }

    internal void ChangeAnim(EPlayer _ePlayer)
    {
        _ePlayer = playerMng.playerAI.aiState;

        switch (_ePlayer)
        {
            case EPlayer.WALK:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.WALK);
                break;
            case EPlayer.RUN:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.RUN);
                break;
            case EPlayer.JUMP:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.JUMP);
                break;
            case EPlayer.LAND:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.LAND);
                break;
            case EPlayer.ATTACK:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.ATTACK);
                break;
            case EPlayer.DIE:
                playerMng.anim.SetInteger("PlayerAnim", (int)EPlayerAnim.DIE);
                break;
        }
    }
}
