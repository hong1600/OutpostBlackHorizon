using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : Singleton<GunManager>
{
    public event Action onUpdateBullet;

    public int curBulletCount { get; private set; }
    public int maxBulletCount { get; private set; }
    public int haveBulletCount { get; private set; }
    public int curGrenadeCount { get; private set; }
    public bool isReloading { get; private set; } = false;

    [SerializeField] GunMovement gunMovemnet;

    protected override void Awake()
    {
        base.Awake();

        curBulletCount = 30;
        maxBulletCount = 30;
        haveBulletCount = 300;
        curGrenadeCount = 1;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullet();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            haveBulletCount = 210;
        }

    }

    private void ReloadBullet()
    {
        if (!isReloading && (curBulletCount < maxBulletCount || curGrenadeCount == 0))
        {
            if (haveBulletCount != 0)
            {
                StartCoroutine(StartReloading());
            }
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

    public void FillBullet(int _amount)
    {
        haveBulletCount += _amount;
        onUpdateBullet?.Invoke();
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

        if(haveBulletCount != 0)
        {
            int needBullet = maxBulletCount - curBulletCount;

            if (haveBulletCount >= needBullet)
            {
                haveBulletCount -= needBullet;
                curBulletCount = maxBulletCount;
            }
            else
            {
                curBulletCount += haveBulletCount;
                haveBulletCount = 0;
            }
        }

        onUpdateBullet?.Invoke();

        isReloading = false;
    }

    public GunMovement GunMovement { get { return gunMovemnet; } }
}
