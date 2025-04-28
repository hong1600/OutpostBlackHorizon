using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<EEnemy>
{
    public event Action onEnemyCount;

    EnemyManager enemyManager;

    private void Start()
    {
        Init();
        enemyManager = EnemyManager.instance;
    }

    public GameObject FindEnemy(EEnemy _type, Vector3 _pos, Quaternion _rot)
    {
        enemyManager.OnEnemySpawn();
        onEnemyCount?.Invoke();
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnEnemy(EEnemy _type, GameObject _obj)
    {
        enemyManager.OnEnemyDie();
        ReturnPool(_type, _obj);
        onEnemyCount?.Invoke();
    }
}
