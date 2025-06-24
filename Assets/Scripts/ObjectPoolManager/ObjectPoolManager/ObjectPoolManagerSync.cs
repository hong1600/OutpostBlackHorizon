using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManagerSync : Singleton<ObjectPoolManagerSync>, IObjectPoolManager
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] EffectPool effectPool;
    [SerializeField] HpBarPool hpBarPool;
    [SerializeField] BulletPoolSync bulletPoolSync;

    IBulletPool bulletPool;

    protected override void Awake()
    {
        base.Awake();

        bulletPoolSync.Init();

        bulletPool = bulletPoolSync;
    }

    public IBulletPool BulletPool { get { return bulletPool; } }
}
