using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolSync : ObjectPoolBaseSync<EEnemy>, IEnemyPool
{
    public event Action onEnemyCount;

    EnemyManager enemyManager;

    public GameObject FindEnemy(EEnemy _type, Vector3 _pos, Quaternion _rot)
    {
        if (myPoolDic.TryGetValue(_type, out Queue<GameObject> pool))
        {
            if (pool.Count > 0)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    GameObject obj = pool.Dequeue();

                    PhotonView pv = obj.GetComponent<PhotonView>();

                    if (!obj.activeInHierarchy && pv != null && pv.IsMine)
                    {
                        obj.transform.position = _pos;
                        obj.transform.rotation = _rot;
                        obj.SetActive(true);
                        return obj;
                    }

                    pool.Enqueue(obj);
                }
            }
        }
        return null;
    }

    public void ReturnEnemy(EEnemy _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
        enemyManager.OnEnemyDie();
        onEnemyCount?.Invoke();
    }

    public Transform ParentDic(EEnemy _type)
    {
        return parentDic[_type];
    }

    public void AddEvent(Action _handler)
    {
        onEnemyCount += _handler;
    }
}
