using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class FactoryBase<T> : MonoBehaviour where T : System.Enum
{
    protected SceneResourceBase<T> sceneResource;

    private void Start()
    {
        sceneResource = SceneResourceBase<T>.instance;
    }

    public virtual void Create(T _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        GameObject prefab = sceneResource.GetPrefab(_type);

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, _pos, _rot, _parent);
            Init(obj, _type);
            _onComplete?.Invoke(obj);
        }
        else
        {
            _onComplete?.Invoke(null);
        }
    }

    protected abstract void Init(GameObject _obj, T _type); 
}
