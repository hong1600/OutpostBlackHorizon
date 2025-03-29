using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public event Action onReloading;
    public int curBulletCount { get; private set; }
    public int maxBulletCount { get; private set; }
    public int haveBulletCount { get; private set; }
    public int curGrenadeCount { get; private set; }
    public bool isReloading { get; private set; } = false;  


    private void Awake()
    {
        curBulletCount = 30;
        maxBulletCount = 30;
        haveBulletCount = 150;
        curGrenadeCount = 10;

        Shared.gunManager = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullet();
        }
    }

    private void ReloadBullet()
    {
        if (!isReloading && (curBulletCount < maxBulletCount || curGrenadeCount == 0))
        {
            StartCoroutine(StartReloading());
        }
    }

    public void UseBullet()
    {
        curBulletCount -= 1;

        if (curBulletCount <= 0 && !isReloading)
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
        isReloading = true;

        AudioManager.instance.PlaySfx(ESfx.GUNRELOAD, transform.position);

        yield return new WaitForSeconds(2.5f);

        if (curGrenadeCount == 0) curGrenadeCount = 1;

        haveBulletCount -= (maxBulletCount - curBulletCount);
        curBulletCount = maxBulletCount;

        onReloading?.Invoke();

        isReloading = false;
    }
}
