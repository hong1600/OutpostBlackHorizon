using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : FactoryBase<EEnemy>
{
    TableEnemy tableEnemy;
    EnemyPool enemyPool;

    private void Start()
    {
        resource = ResourceManager.instance.GameSceneResource.EnemyResource;
        tableEnemy = DataManager.instance.TableEnemy;
        enemyPool = ObjectPoolManager.instance.EnemyPool;
    }

    public override GameObject Create
        (EEnemy _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        GameObject obj = enemyPool.FindEnemy(_type, _pos, _rot);
        Transform parent = enemyPool.parentDic[_type];

        if (obj != null)
        {
            Init(obj, _type);
            _onComplete?.Invoke(obj);
        }
        else
        {
            return base.Create(_type, _pos, _rot, parent, _onComplete);
        }

        return null;
    }

    protected override void Init(GameObject _obj, EEnemy _eEnemy)
    {
        EnemyBase enemy = _obj.GetComponent<EnemyBase>();

        if(enemy == null) 
        {
            enemy = _obj.GetComponentInChildren<EnemyBase>();
        }

        if (enemy != null)
        {
            TableEnemy.Info info = tableEnemy.Get(_eEnemy);

            if (info != null)
            {
                enemy.Init(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, _eEnemy);
            }
        }
    }
}
