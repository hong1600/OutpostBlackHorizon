using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GunMovement gunMovement;
    [SerializeField] internal bool isAttack = false;
    [SerializeField] Transform fireTrs;
    [SerializeField] GameObject muzzleFlash;

    [SerializeField] float rifleSpeed;
    [SerializeField] float grenadeSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAttack) 
        {
            StartCoroutine(StartAttackRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack) 
        {
            StartCoroutine(StartAttackGrenade());
        }
    }

    IEnumerator StartAttackRifle()
    {
        isAttack = true;
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();

        GameObject obj = Shared.objectPoolMng.iBulletPool.FindBullet(EBullet.BULLET);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.InitBullet(null, 30, rifleSpeed, EBullet.BULLET, fireTrs);

        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    IEnumerator StartAttackGrenade()
    {
        isAttack = true;
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();

        GameObject obj = Shared.objectPoolMng.iBulletPool.FindBullet(EBullet.BULLET);
        obj.transform.position = fireTrs.transform.position;
        obj.transform.rotation = fireTrs.rotation * Quaternion.Euler(0, 180, 0);
        Bullet bullet = obj.GetComponent<Bullet>();
        //bullet.InitBullet(null, 30, grenadeSpeed, EBullet.GRENADE);

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
        isAttack = false;
    }
}
