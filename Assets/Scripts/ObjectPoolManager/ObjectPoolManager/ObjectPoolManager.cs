using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>, IObjectPoolManager
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] EffectPool effectPool;
    [SerializeField] HpBarPool hpBarPool;
    [SerializeField] BulletPool bulletPool;

    IBulletPool iBulletPool;
    IEnemyPool iEnemyPool;

    protected override void Awake()
    {
        base.Awake();

        enemyPool.Init();
        effectPool.Init();
        hpBarPool.Init();
        bulletPool.Init();

        EffectPool = effectPool;
        HpBarPool = hpBarPool;
        iBulletPool = bulletPool;
        iEnemyPool = enemyPool;
    }

    public EffectPool EffectPool { get; private set; }
    public HpBarPool HpBarPool { get; private set; }
    public IBulletPool BulletPool { get { return bulletPool; } }
    public IEnemyPool EnemyPool { get { return enemyPool; } }

}
