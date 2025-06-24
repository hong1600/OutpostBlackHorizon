using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSync : PlayerCombat
{
    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    protected override void Update()
    {
        if (!pv.IsMine) return;

        if (Input.GetMouseButton(0) && cameraFpsShake.enabled == true && !isAttack && !gunManager.isReloading)
        {
            StartCoroutine(StartFireRifle(true));

            pv.RPC(nameof(RpcFireRifle), RpcTarget.Others);
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading && gunManager.curGrenadeCount > 0)
        {
            StartCoroutine(StartFireGrenade(true));

            pv.RPC(nameof(RpcFireGrenade), RpcTarget.Others);
        }
    }

    [PunRPC]
    private void RpcFireRifle()
    {
        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        StartCoroutine(StartOffFlash());
    }

    [PunRPC]
    private void RpcFireGrenade()
    {
        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        StartCoroutine(StartOffFlash());
    }

    [PunRPC]
    IEnumerator StartOffFlash()
    {
        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
    }
}
