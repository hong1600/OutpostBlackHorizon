using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolMng : MonoBehaviour
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] EffectPool effectPool;
    [SerializeField] HpBarPool hpBarPool;
    public IEnemyPool iEnemyPool;
    public IEffectPool iEffectPool;
    public IHpBarPool iHpBarPool;

    [SerializeField] Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();

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

    public void Init(string _type, GameObject _prefab, Transform _parent)
    {
        if (!poolDic.ContainsKey(_type))
        {
            poolDic[_type] = new Queue<GameObject>();
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

            if (pool.Count > 0)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    GameObject obj = pool.Dequeue();

                    if (!obj.activeInHierarchy)
                    {
                        obj.SetActive(true);
                        return obj;
                    }
                }
            }

            GameObject newObj = Instantiate(prefabDic[_type], _parent);
            newObj.name = prefabDic[_type].name;
            newObj.SetActive(true);
            return newObj;
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
