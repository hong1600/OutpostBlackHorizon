using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyPool
{
    GameObject FindEnemy(EEnemy _eEnemy);
}

public class EnemyPool : ObjectPool<EEnemy>, IEnemyPool
{
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(enemyList, parentList);
    }

    public GameObject FindEnemy(EEnemy _eEnemy)
    {
        return base.FindObject(_eEnemy);
    }
}
