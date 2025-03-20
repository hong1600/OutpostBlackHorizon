using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    BulletPool bulletPool;
    CameraFpsShake cameraFpsShake;

    public event Action onUseBullet;

    [SerializeField] GunManager gunManager;
    [SerializeField] GunMovement gunMovement;
    [SerializeField] internal bool isAttack = false;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] Transform fireTrs;

    [SerializeField] float bulletDmg;
    [SerializeField] float bulletSpd;
    [SerializeField] float grenadeSpd;

    public float cumulativeDmg { get; set; } = 0f;

    private void Start()
    {
        bulletPool = Shared.objectPoolManager.BulletPool;
        cameraFpsShake = Shared.cameraManager.CameraFpsShake;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAttack && !gunManager.isReloading) 
        {
            StartCoroutine(StartAttackRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading) 
        {
            StartCoroutine(StartAttackGrenade());
        }
    }

    IEnumerator StartAttackRifle()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position);
        gunManager.UseBullet();
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();
        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.Init(null, bulletDmg, bulletSpd);

        cameraFpsShake.Shake();
        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    IEnumerator StartAttackGrenade()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position);
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
