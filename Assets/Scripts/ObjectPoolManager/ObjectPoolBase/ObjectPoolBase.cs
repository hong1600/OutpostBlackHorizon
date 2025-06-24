using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase<T> : MonoBehaviour
{
    [SerializeField] protected List<GameObject> objectList = new List<GameObject>();
    [SerializeField] protected List<Transform> parentList = new List<Transform>();

    public Dictionary<T, Queue<GameObject>> poolDic = new Dictionary<T, Queue<GameObject>>();
    public Dictionary<T, GameObject> prefabDic = new Dictionary<T, GameObject>();
    public Dictionary<T, Transform> parentDic = new Dictionary<T, Transform>();

    public virtual void Init()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            T type = (T)(object)i;

            poolDic[type] = new Queue<GameObject>();
            prefabDic[type] = objectList[i];
            parentDic[type] = parentList[i];
        }
    }

    public GameObject FindObject(T _type, Vector3 _pos, Quaternion _rot)
    {
        if (poolDic.TryGetValue(_type, out Queue<GameObject> pool))
        {
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
                        return obj;
                    }
                }
            }

            GameObject newObj = Instantiate(prefabDic[_type], parentDic[_type]);
            newObj.name = prefabDic[_type].name;
            newObj.transform.position = _pos;
            newObj.transform.rotation = _rot;
            newObj.SetActive(true);
            return newObj;
        }
        return null;
    }

    public void ReturnPool(T _type, GameObject _obj)
    {
        if (poolDic.TryGetValue(_type, out Queue<GameObject> pool))
        {
            _obj.SetActive(false);
            pool.Enqueue(_obj);
        }
    }
}
