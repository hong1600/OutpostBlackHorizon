using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSync : PlayerMovement
{
    PhotonView pv;

    Vector3 otherPos;
    Quaternion otherRot;
    bool otherIsRun;

    protected override void Awake()
    {
        base.Awake();
        pv = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        if (pv.IsMine)
        {
            StartCoroutine(StartSendMoveSync());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void Update()
    {
        if(!pv.IsMine) 
        {
            transform.position = Vector3.Lerp(transform.position, otherPos, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, otherRot, Time.deltaTime * 10);
            return;
        }

        base.Update();
    }

    protected override void Move()
    {
        if (pv.IsMine)
        {
            base.Move();
        }
    }

    IEnumerator StartSendMoveSync()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            if (pv.IsMine)
            {
                pv.RPC(nameof(RPCMove), RpcTarget.Others, transform.position, transform.rotation, isRun, moveX, moveZ);
            }

            yield return wait;
        }
    }

    [PunRPC]
    private void RPCMove(Vector3 _pos, Quaternion _rot, bool _isRun, float _moveX, float _moveZ)
    {
        if (!pv.IsMine)
        {
            otherPos = _pos;
            otherRot = _rot;
            otherIsRun = _isRun;

            if (playerManager != null && playerManager.anim != null)
            {
                playerManager.anim.SetFloat("Horizontal", _moveX);
                playerManager.anim.SetFloat("Vertical", _moveZ);
            }
        }
    }
}
