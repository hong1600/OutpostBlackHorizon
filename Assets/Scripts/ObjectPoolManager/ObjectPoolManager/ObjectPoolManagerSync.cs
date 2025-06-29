using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManagerSync : Singleton<ObjectPoolManagerSync>, IObjectPoolManager
{
    [SerializeField] EffectPool effectPool;
    [SerializeField] HpBarPool hpBarPool;
    [SerializeField] EnemyPoolSync enemyPoolSync;
    [SerializeField] BulletPoolSync bulletPoolSync;

    IBulletPool bulletPool;
    IEnemyPool enemyPool;

    protected override void Awake()
    {
        base.Awake();

        bulletPoolSync.Init();

        bulletPool = bulletPoolSync;
        enemyPool = enemyPoolSync;
    }

    public IBulletPool BulletPool { get { return bulletPool; } }
    public IEnemyPool EnemyPool { get { return enemyPool; } }
}
