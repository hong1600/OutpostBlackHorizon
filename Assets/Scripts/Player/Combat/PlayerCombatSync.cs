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

    IEnumerator StartOffFlash()
    {
        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
    }

    protected override IEnumerator StartFireRifle(bool _isMine)
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        if (_isMine)
        {
            gunManager.UseBullet();
            gunMovement.RecoilGun();

            GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
            PlayerBulletSync bullet = obj.GetComponent<PlayerBulletSync>();
            bullet.Init(null, bulletDmg, bulletSpd, EBulletType.PLAYER);
            
            PhotonView pv = bullet.GetComponent<PhotonView>();
            pv.RPC(nameof(bullet.RPCInit), RpcTarget.Others, bulletDmg, bulletSpd, (int)EBulletType.PLAYER);

            cameraFpsShake.Shake();

            InvokeUseBullet();
        }

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    protected override IEnumerator StartFireGrenade(bool _isMine)
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        if (_isMine)
        {
            gunManager.UseGrenade();
            gunMovement.RecoilGun();

            GameObject obj = bulletPool.FindBullet(EBullet.PLAYERGRENADE, fireTrs.position, fireTrs.rotation);
            PlayerGrenadeSync bullet = obj.GetComponent<PlayerGrenadeSync>();
            bullet.Init(null, 100, grenadeSpd);

            cameraFpsShake.Shake();

            InvokeUseBullet();
        }

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);

        isAttack = false;
    }
}
