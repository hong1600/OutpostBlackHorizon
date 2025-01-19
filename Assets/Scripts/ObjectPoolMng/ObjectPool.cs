using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour
{
    [SerializeField] protected Dictionary<T, (GameObject obj, Transform parent)> objectDic =
    new Dictionary<T, (GameObject obj, Transform parent)>();

    [SerializeField] protected List<GameObject> objectList = new List<GameObject>();
    [SerializeField] protected List<Transform> objectParentList = new List<Transform>();

    protected virtual void Init(List<GameObject> _objList, List<Transform> _parentList)
    {
        for (int i = 0; i < _objList.Count; i++)
        {
            T objectType = (T)(object)i;

            objectDic.Add(objectType, (_objList[i], _parentList[i]));
            Shared.objectPoolMng.Init(_objList[i].name, _objList[i], _parentList[i]);
        }
    }

    protected virtual GameObject FindObject(T _objectType)
    {
        if (objectDic.ContainsKey(_objectType))
        {
            (GameObject obj, Transform parent) objectData = objectDic[_objectType];

            return Shared.objectPoolMng.GetObject(objectData.obj.name, objectData.parent);
        }

        return null;
    }
}
