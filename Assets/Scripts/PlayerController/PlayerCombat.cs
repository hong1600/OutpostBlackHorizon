using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    BulletPool bulletPool;

    public event Action onUseBullet;

    [SerializeField] GunManager gunManager;
    [SerializeField] GunMovement gunMovement;
    [SerializeField] internal bool isAttack = false;
    [SerializeField] Transform fireTrs;
    [SerializeField] GameObject muzzleFlash;

    [SerializeField] float rifleSpeed;
    [SerializeField] float grenadeSpeed;

    public float cumulativeDmg { get; set; } = 0f;

    private void Start()
    {
        bulletPool = Shared.objectPoolManager.BulletPool;
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
        GameObject obj = bulletPool.FindBullet(EBullet.BULLET);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.InitBullet(null, 30, rifleSpeed, EBullet.BULLET, fireTrs);

        Shared.cameraManager.getCameraFpsShake.Shake();
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
        GameObject obj = bulletPool.FindBullet(EBullet.GRENADE);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.InitBullet(null, 100, grenadeSpeed, EBullet.GRENADE, fireTrs);
        Shared.cameraManager.getCameraFpsShake.Shake();
        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
        isAttack = false;
    }
}
