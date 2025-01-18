using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMng : MonoBehaviour
{
    public EnemyPool enemyPool;
    public IEnemyPool iEnemyPool;
    public EffectPool effectPool;
    public IEffectPool iEffectPool;
    public HpBarPool hpBarPool;
    public IHpBarPool iHpBarPool;

    Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Shared.objectPoolMng == null)
        {
            Shared.objectPoolMng = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iEnemyPool = enemyPool;
        iEffectPool = effectPool;
        iHpBarPool = hpBarPool;
    }

    public void Init(string _type, GameObject _prefab, int _initSize, Transform _parent)
    {
        if (!poolDic.ContainsKey(_type))
        {
            poolDic[_type] = new Queue<GameObject>();
        }

        Queue<GameObject> pool = poolDic[_type];

        for(int i = 0; i < _initSize; i++) 
        {
            GameObject obj = Instantiate(_prefab, _parent);
            obj.SetActive(false);
            obj.name = _prefab.name;
            pool.Enqueue(obj);
        }

        if (!prefabDic.ContainsKey(_type))
        {
            prefabDic[_type] = _prefab;
        }
    }

    public GameObject GetObject(string _type, Transform _parent)
    {
        if (poolDic.ContainsKey(_type))
        {
            Queue<GameObject> pool = poolDic[_type];

            if(pool.Count > 0) 
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(prefabDic[_type], _parent);
                newObj.name = prefabDic[_type].name;
                newObj.SetActive(true);
                return newObj;
            }
        }
        return null;
    }

    public void ReturnObject(string _type, GameObject _obj)
    {
        if(poolDic.ContainsKey(_type)) 
        {
            _obj.SetActive(false);
            poolDic[_type].Enqueue(_obj);
        }
    }
}
