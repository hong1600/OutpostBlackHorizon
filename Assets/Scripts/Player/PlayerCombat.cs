using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    BulletPool bulletPool;
    CameraFpsShake cameraFpsShake;
    GunManager gunManager;
    GunMovement gunMovement;
    AirStrike airStrike;
    GuideMissile guideMissile;

    public event Action onUseBullet;

    [SerializeField] internal bool isAttack = false;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] Transform fireTrs;

    [SerializeField] float bulletDmg;
    [SerializeField] float bulletSpd;
    [SerializeField] float grenadeSpd;

    private void Start()
    {
        bulletPool = ObjectPoolManager.instance.BulletPool;
        cameraFpsShake = CameraManager.instance.CameraFpsShake;
        gunManager = GunManager.instance;
        gunMovement = GunManager.instance.GunMovement;
        airStrike = PlayerManager.instance.airStrike;
        guideMissile = PlayerManager.instance.guideMissile;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cameraFpsShake.enabled == true && !isAttack && !gunManager.isReloading) 
        {
            StartCoroutine(StartAttackRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading && gunManager.curGrenadeCount > 0) 
        {
            StartCoroutine(StartAttackGrenade());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            airStrike.PlayAirStrike();
        }

        if(Input.GetKeyDown(KeyCode.X)) 
        {
            guideMissile.FireMissile();
        }
    }

    IEnumerator StartAttackRifle()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);
        gunManager.UseBullet();
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();
        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.Init(null, bulletDmg, bulletSpd, EBulletType.PLAYER);

        cameraFpsShake.Shake();
        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    IEnumerator StartAttackGrenade()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);
        gunManager.UseGrenade();
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();
        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERGRENADE, fireTrs.position, fireTrs.rotation);
        PlayerGrenade bullet = obj.GetComponent<PlayerGrenade>();
        bullet.Init(null, 100, grenadeSpd);

        cameraFpsShake.Shake();
        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
        isAttack = false;
    }
}
