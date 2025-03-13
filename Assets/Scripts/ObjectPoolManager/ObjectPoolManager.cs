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

    [SerializeField] Transform bulletTrs;

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
                        Bullet bullet = obj.GetComponent<Bullet>();
                        if (bullet != null)
                        {
                            obj.SetActive(false);
                            obj.transform.position = bulletTrs.transform.position;
                            obj.transform.rotation = bulletTrs.transform.rotation * Quaternion.Euler(90, 0, 0);
                        }

                        obj.SetActive(true);
                        return obj;
                    }
                }
            }
            GameObject newObj = Instantiate(prefabDic[_type], _parent);

            Bullet newBullet = newObj.GetComponent<Bullet>();
            if (newBullet != null)
            {
                newObj.SetActive(false);
                newObj.transform.position = bulletTrs.transform.position;
                newObj.transform.rotation = bulletTrs.transform.rotation * Quaternion.Euler(90, 0, 0);
            }

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
