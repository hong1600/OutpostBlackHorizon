using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour
{
    protected ObjectPoolManager poolManager;

    [SerializeField] protected List<GameObject> objectList = new List<GameObject>();
    [SerializeField] protected List<Transform> parentList = new List<Transform>();

    protected Dictionary<T, Transform> parentDic = new Dictionary<T, Transform>();

    protected virtual void Init()
    {
        poolManager = ObjectPoolManager.instance;

        for (int i = 0; i < objectList.Count; i++)
        {
            T type = (T)(object)i;
            string typeName = type.ToString();

            poolManager.Init(typeName, objectList[i], parentList[i]);
            parentDic[type] = parentList[i];
        }
    }

    protected virtual GameObject FindObject(T _type, Vector3 _pos, Quaternion _rot)
    {
        if (parentDic.TryGetValue(_type, out Transform parent))
        {
            return poolManager.GetObject(_type.ToString(), _pos, _rot, parent);
        }

        return null;
    }

    protected virtual void ReturnPool(T _type, GameObject _obj)
    {
        poolManager.ReturnObject(_type.ToString(), _obj);
    }
}
