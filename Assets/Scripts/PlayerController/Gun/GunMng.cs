using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMng : MonoBehaviour
{
    public float curBulletCount { get; private set; }
    public float maxBulletCount { get; private set; }
    public float haveBulletCount { get; private set; }
    public float curGrenadeCount { get; private set; }

    private void Awake()
    {
        curBulletCount = 30f;
        maxBulletCount = 30f;
        haveBulletCount = 150f;
        curGrenadeCount = 5f;
    }

    public void UseBullet()
    {
        curBulletCount -= 1;

        if (curBulletCount <= 0)
        {
            StartCoroutine(StartReloading());
        }
    }
    public void UseGrenade()
    {
        curGrenadeCount -= 1;
    }

    IEnumerator StartReloading()
    {

        yield return new WaitForSeconds(2);

        haveBulletCount -= (maxBulletCount - curBulletCount);
        curBulletCount = maxBulletCount;
    }

}
