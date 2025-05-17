using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPoolBase<EEnemy>
{
    public event Action onEnemyCount;

    EnemyManager enemyManager;
    EnemyFactory enemyFactory;

    Vector3 spawnPos;
    Quaternion spawnRot;

    private void Start()
    {
        Init();
        enemyManager = EnemyManager.instance;
        enemyFactory = FactoryManager.instance.EnemyFactory;
    }

    public GameObject FindEnemy(EEnemy _type, Vector3 _pos, Quaternion _rot)
    {
        string type = _type.ToString();

        if (poolManager.poolDic.ContainsKey(type))
        {
            Queue<GameObject> pool = poolManager.poolDic[type];

            if (pool.Count > 0)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    GameObject obj = pool.Dequeue();

                    if (!obj.activeInHierarchy)
                    {
                        obj.transform.position = _pos;
                        obj.transform.rotation = _rot;
                        obj.SetActive(true);
                        return obj;
                    }
                    else
                    {
                        pool.Enqueue(obj);
                    }
                }
            }
        }
        return null;
    }

    public void ReturnEnemy(EEnemy _type, GameObject _obj)
    {
        enemyManager.OnEnemyDie();
        ReturnPool(_type, _obj);
        onEnemyCount?.Invoke();
    }
}
