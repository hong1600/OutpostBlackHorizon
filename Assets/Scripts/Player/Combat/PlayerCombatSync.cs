using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSync : PlayerCombat, IOnEventCallback
{
    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    protected override void Update()
    {
        if (!pv.IsMine) return;

        if (Input.GetMouseButton(0) && cameraFpsShake.enabled == true && !isAttack && !gunManager.isReloading)
        {
            StartCoroutine(StartFireRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading && gunManager.curGrenadeCount > 0)
        {
            StartCoroutine(StartFireGrenade());
        }
    }

    IEnumerator StartOffFlash()
    {
        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
    }

    protected override IEnumerator StartFireRifle()
    {
        if (pv.IsMine)
        {
            isAttack = true;

            AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);

            muzzleFlash.SetActive(true);

            gunManager.UseBullet();
            gunMovement.RecoilGun();

            GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
            PlayerBulletSync bullet = obj.GetComponent<PlayerBulletSync>();
            bullet.Init(null, bulletDmg, bulletSpd, EBulletType.PLAYER);

            SendBulletData(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation, bulletDmg, bulletSpd);

            cameraFpsShake.Shake();

            InvokeUseBullet();

            yield return new WaitForSeconds(0.1f);

            muzzleFlash.SetActive(false);

            isAttack = false;
        }
    }

    protected override IEnumerator StartFireGrenade()
    {
        if (pv.IsMine)
        {
            isAttack = true;

            AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);

            muzzleFlash.SetActive(true);

            gunManager.UseGrenade();
            gunMovement.RecoilGun();

            GameObject obj = bulletPool.FindBullet(EBullet.PLAYERGRENADE, fireTrs.position, fireTrs.rotation);
            PlayerGrenade bullet = obj.GetComponent<PlayerGrenade>();
            bullet.Init(null, 100, grenadeSpd);

            SendBulletData(EBullet.PLAYERGRENADE, fireTrs.position, fireTrs.rotation, bulletDmg, bulletSpd);

            cameraFpsShake.Shake();

            InvokeUseBullet();

            yield return new WaitForSeconds(0.1f);

            muzzleFlash.SetActive(false);

            isAttack = false;
        }
    }

    private void SendBulletData(EBullet _type, Vector3 _pos, Quaternion _rot, float _dmg, float _spd)
    {
        BulletSyncData data = new BulletSyncData()
        {
            pos = _pos,
            rot = _rot,
            dmg = _dmg,
            spd = _spd
        };

        RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true };

        if (_type == EBullet.PLAYERBULLET) 
        {
            PhotonNetwork.RaiseEvent(PhotonEventCode.SPAWN_BULLET_EVENT, data, options, sendOptions);
        }
        else if(_type  == EBullet.PLAYERGRENADE) 
        {
            PhotonNetwork.RaiseEvent(PhotonEventCode.SPAWN_GRENADE_EVENT, data, options, sendOptions);
        }
    }

    private void SyncFireRifle(Vector3 _pos, Quaternion _rot, float _dmg, float _spd)
    {
        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, _pos, null);

        muzzleFlash.SetActive(true);

        StartCoroutine(StartOffFlash());

        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, _pos, _rot);
        PlayerBulletSync bullet = obj.GetComponent<PlayerBulletSync>();
        bullet.Init(null, _dmg, _spd, EBulletType.PLAYER);
    }

    private void SyncFireGrenade(Vector3 _pos, Quaternion _rot, float _dmg, float _spd)
    {
        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, _pos, null);

        muzzleFlash.SetActive(true);

        StartCoroutine(StartOffFlash());

        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERGRENADE, _pos, _rot);
        PlayerGrenadeSync bullet = obj.GetComponent<PlayerGrenadeSync>();
        bullet.Init(null, _dmg, _spd);
    }

    public void OnEvent(EventData _photonEvent)
    {
        if(!pv.IsMine) 
        {
            if (_photonEvent.Code == PhotonEventCode.SPAWN_BULLET_EVENT)
            {
                BulletSyncData data = (BulletSyncData)_photonEvent.CustomData;

                SyncFireRifle(data.pos, data.rot, data.dmg, data.spd);
            }
            else if (_photonEvent.Code == PhotonEventCode.SPAWN_GRENADE_EVENT)
            {
                BulletSyncData data = (BulletSyncData)_photonEvent.CustomData;

                SyncFireGrenade(data.pos, data.rot, data.dmg, data.spd);
            }
        }
    }
}
