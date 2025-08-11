using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] protected GunManager gunManager;

    protected IBulletPool bulletPool;
    protected CameraFpsShake cameraFpsShake;
    CameraFpsZoom cameraFpsZoom;
    protected GunMovement gunMovement;

    public event Action onUseBullet;

    [SerializeField] internal bool isAttack = false;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected Transform fireTrs;

    [SerializeField] protected float bulletDmg;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float grenadeSpd;

    private void Start()
    {
        bulletPool = Shared.Instance.poolManager.BulletPool;
        cameraFpsShake = CameraManager.instance.CameraFpsShake;
        cameraFpsZoom = CameraManager.instance.CameraFpsZoom;
        gunMovement = gunManager.GunMovement;

        InputManager.instance.onInput1 += SwapWeapon;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(0) && cameraFpsShake.enabled == true && !isAttack && !gunManager.isReloading) 
        {
            StartCoroutine(StartFireRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading && gunManager.curGrenadeCount > 0) 
        {
            StartCoroutine(StartFireGrenade());
        }
    }

    protected virtual IEnumerator StartFireRifle()
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        gunManager.UseBullet();

        if (!cameraFpsZoom.isZoom)
        {
            gunMovement.RecoilGun();
        }

        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.Init(null, bulletDmg, bulletSpd, EBulletType.PLAYER);

        cameraFpsShake.Shake();

        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    protected virtual IEnumerator StartFireGrenade()
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        gunManager.UseGrenade();
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

    protected void InvokeUseBullet()
    {
        onUseBullet?.Invoke();
    }

    private void SwapWeapon(int _weaponIndex)
    {
        EPlayerWeapon weapon = (EPlayerWeapon)_weaponIndex;

        switch (weapon) 
        {
            case EPlayerWeapon.RIFLE:

                break;
            case EPlayerWeapon.PISTOL:

                break;
        }
    }
}
