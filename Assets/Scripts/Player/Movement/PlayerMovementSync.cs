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

    protected override void Start()
    {
        base.Start();

        if(pv.IsMine) 
        {
            pv.RPC(nameof(RPCMove), RpcTarget.Others, transform.position, transform.rotation, isRun, 0f, 0f);
        }
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
        base.Move();

        if (pv.IsMine)
        {
            pv.RPC(nameof(RPCMove), RpcTarget.Others, transform.position, transform.rotation, isRun, moveX, moveZ);
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
