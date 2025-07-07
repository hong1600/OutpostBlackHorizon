using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactorySync : FactoryBaseSync<EEnemy>
{
    TableEnemy tableEnemy;
    IEnemyPool enemyPool;

    int id = 0;

    private void Start()
    {
        resource = ResourceManager.instance.GameSceneResource.EnemyResource;
        tableEnemy = DataManager.instance.TableEnemy;
        enemyPool = Shared.Instance.poolManager.EnemyPool;
    }

    public override GameObject Create
        (EEnemy _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        GameObject obj = enemyPool.FindEnemy(_type, _pos, _rot);
        Transform parent = enemyPool.ParentDic(_type);

        if (obj != null)
        {
            if(PhotonNetwork.IsMasterClient) 
            {
                Init(obj, _type, true);
            }
            else
            {
                Init(obj, _type, false);
            }

            _onComplete?.Invoke(obj);
            return obj;
        }
        else
        {
            _onComplete?.Invoke(null);
            return base.Create(_type, _pos, _rot, parent, _onComplete);
        }
    }

    protected override void Init(GameObject _obj, EEnemy _eEnemy, bool _isMaster)
    {
        id++;

        EnemyBase enemyBase = _obj.GetComponent<EnemyBase>();
        EnemySync enemySync = _obj.GetComponent<EnemySync>();

        Debug.Log(enemySync);

        if (enemyBase == null)
        {
            enemyBase = _obj.GetComponentInChildren<EnemyBase>();
        }
        if (enemySync == null)
        {
            enemySync = _obj.GetComponentInChildren<EnemySync>();
        }

        if (_isMaster)
        {
            if (enemyBase != null)
            {
                TableEnemy.Info info = tableEnemy.Get(_eEnemy);

                if (info != null)
                {
                    enemyBase.enabled = true;
                    enemyBase.Init(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, _eEnemy, id);
                }
            }
        }
        else
        {
            enemySync.enabled = true;
            Debug.Log(enemySync);
            enemySync.Init(id);
        }
    }
}
