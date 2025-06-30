using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class FactoryBase<T> : MonoBehaviour where T : Enum
{
    protected SceneResourceBase<T> resource;

    public virtual GameObject Create(T _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        Debug.Log("어드레서블 생성 실행");

        GameObject prefab = resource.GetPrefab(_type);

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, _pos, _rot, _parent);
            Init(obj, _type);
            _onComplete?.Invoke(obj);
            Debug.Log($"어드레서블{obj}반환");
            return obj;
        }
        else
        {
            Debug.Log("모두 null반환");

            _onComplete?.Invoke(null);
            return null;
        }
    }

    protected abstract void Init(GameObject _obj, T _type); 
}
