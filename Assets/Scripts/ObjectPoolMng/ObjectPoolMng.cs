using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMng : MonoBehaviour
{
    public EnemyPool enemyPool;
    public IEnemypool iEnemyPool;
    public HpBarPool hpBarPool;
    public IHpBarPool iHpBarPool;
    public EffectPool effectPool;
    public IEffectPool iEffectPool;

    Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

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
        iHpBarPool = hpBarPool;
        iEffectPool = effectPool;
    }

    public void Init(string _key, GameObject _prefab, int _initSize, Transform _parent)
    {
        if (!poolDic.ContainsKey(_key))
        {
            poolDic[_key] = new Queue<GameObject>();
        }

        Queue<GameObject> pool = poolDic[_key];

        for(int i = 0; i < _initSize; i++) 
        {
            GameObject obj = Instantiate(_prefab, _parent);
            obj.SetActive(false);
            obj.name = _key;
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(string _key, Transform _parent)
    {
        if (poolDic.ContainsKey(_key))
        {
            Queue<GameObject> pool = poolDic[_key];

            if(pool.Count > 0) 
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(poolDic[_key].Peek(), _parent);
                newObj.SetActive(true);
                return newObj;
            }
        }
        return null;
    }

    public void ReturnObject(string _key, GameObject _obj)
    {
        if(poolDic.ContainsKey(_key)) 
        {
            _obj.SetActive(false);
            poolDic[_key].Enqueue(_obj);
        }
    }
}
