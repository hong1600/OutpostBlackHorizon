using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerMovementBase
{
    protected override void CheckGround()
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

    protected override void SetGravity()
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

    protected override void Move()
    {
        if (playerStatus.isDie || isJump) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

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
    }

    protected override void Run()
    {
        if(Input.GetKey(KeyCode.LeftShift) && playerStatus.curEnergy > 0 && isCanRun)
        {
            isRun = true;

            if(Time.time > energyTimer) 
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

            if(playerStatus.curEnergy >= energyRecovery) 
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

    protected override void Jump()
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
