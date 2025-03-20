using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] EffectPool effectPool;
    [SerializeField] HpBarPool hpBarPool;
    [SerializeField] BulletPool bulletPool;

    public EnemyPool EnemyPool { get; private set; }
    public EffectPool EffectPool { get; private set; }
    public HpBarPool HpBarPool { get; private set; }
    public BulletPool BulletPool { get; private set; }
    
    [SerializeField] Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Shared.objectPoolManager == null)
        {
            Shared.objectPoolManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        EnemyPool = enemyPool;
        EffectPool = effectPool;
        HpBarPool = hpBarPool;
        BulletPool = bulletPool;
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

    public GameObject GetObject(string _type, Vector3 _pos, Quaternion _rot, Transform _parent)
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
                        obj.transform.position = _pos;
                        obj.transform.rotation = _rot;
                        obj.SetActive(true);
                        Debug.Log("ÃÑ¾Ë»ý¼º");
                        return obj;
                    }
                }
            }

            GameObject newObj = Instantiate(prefabDic[_type], _parent);
            newObj.name = prefabDic[_type].name;
            newObj.transform.position = _pos;
            newObj.transform.rotation = _rot;
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
