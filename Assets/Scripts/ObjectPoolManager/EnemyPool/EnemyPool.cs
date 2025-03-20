using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<EEnemy>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindEnemy(EEnemy _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnEnemy(EEnemy _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}
