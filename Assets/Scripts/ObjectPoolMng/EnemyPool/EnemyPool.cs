using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemypool
{
    GameObject FindEnemy(string _key);
}

public class EnemyPool : MonoBehaviour, IEnemypool
{
    Dictionary<string, GameObject> enemyDic = new Dictionary<string, GameObject>();

    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] Transform parent;

    private void Start()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyDic.Add(enemyList[i].name, enemyList[i]);
            Shared.objectPoolMng.Init(enemyList[i].name, enemyList[i], 30, parent);
        }
    }

    public GameObject FindEnemy(string _key)
    {
        if (enemyDic.ContainsKey(_key))
        {
            return Shared.objectPoolMng.GetObject(_key, parent);
        }

        return null;
    }
}
