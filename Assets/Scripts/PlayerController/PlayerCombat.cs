using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GunMovement gunMovement;
    [SerializeField] internal bool isAttack = false;
    [SerializeField] Transform fireTrs;
    [SerializeField] GameObject muzzleFlash;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttack) Attack();
    }

    protected virtual void Attack()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        muzzleFlash.SetActive(true);
        gunMovement.RecoilGun();

        GameObject obj = Shared.objectPoolMng.iBulletPool.FindBullet(EBullet.BULLET);
        obj.transform.position = fireTrs.transform.position;
        obj.transform.rotation = fireTrs.rotation * Quaternion.Euler(0, 180, 0);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.InitBullet(null, 30, 0.1f);

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);
        isAttack = false;
    }
}
