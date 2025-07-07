using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    [SerializeField] EnemyFactory enemyFactory;
    [SerializeField] EnemyFactorySync enemyFactorySync;

    protected override void Awake()
    {
        base.Awake();
    }

    public EnemyFactory EnemyFactory { get { return enemyFactory; } }
    public EnemyFactorySync EnemyFactorySync { get { return enemyFactorySync; } }
}
