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

    protected override void Awake()
    {
        base.Awake();

        enemyPool.Init();
        effectPool.Init();
        hpBarPool.Init();
        bulletPool.Init();

        EnemyPool = enemyPool;
        EffectPool = effectPool;
        HpBarPool = hpBarPool;
        iBulletPool = bulletPool;
    }

    public EnemyPool EnemyPool { get; private set; }
    public EffectPool EffectPool { get; private set; }
    public HpBarPool HpBarPool { get; private set; }
    public IBulletPool BulletPool { get { return bulletPool; } }
}
