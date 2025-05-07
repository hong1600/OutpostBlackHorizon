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
        tableEnemy = DataManager.instance.TableEnemy;
        enemyPool = ObjectPoolManager.instance.EnemyPool;
    }

    public override void Create
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
            base.Create(_type, _pos, _rot, parent, _onComplete);
        }
    }

    protected override void Init(GameObject _obj, EEnemy _eEnemy)
    {
        Enemy enemy = _obj.GetComponent<Enemy>();

        if (enemy != null)
        {
            TableEnemy.Info info = tableEnemy.Get(_eEnemy);

            if (info != null)
            {
                enemy.Init(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, _eEnemy);
            }
        }
    }

    public string GetEnemyKey(EEnemy _eEnemy)
    {
        switch (_eEnemy)
        {
            case EEnemy.ROBOT1: return "Robot1";
            case EEnemy.ROBOT2: return "Robot2";
            case EEnemy.ROBOT3: return "Robot3";
            case EEnemy.ROBOT4: return "Robot4";
            case EEnemy.ROBOT5: return "Robot5";
            case EEnemy.ROBOT6: return "Robot6";
            default: return null;
        }
    }

    protected override string ConvertKeyToAddress(EEnemy _key)
    {
        return GetEnemyKey(_key);
    }
}
