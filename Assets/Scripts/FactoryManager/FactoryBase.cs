using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class FactoryBase<T> : MonoBehaviour
{
    public virtual void Create(T _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        StartCoroutine(StartLoad(_type, _pos, _rot, _parent, _onComplete));
    }

    IEnumerator StartLoad(T _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        string address = ConvertKeyToAddress(_type);

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
        yield return handle;

        if(handle.Status == AsyncOperationStatus.Succeeded) 
        {
            GameObject prefab = handle.Result;
            GameObject obj = Instantiate(prefab, _pos, _rot, _parent);

            Init(obj, _type);
            _onComplete?.Invoke(obj);
        }
        else
        {
            _onComplete?.Invoke(null);
        }
    }

    protected abstract string ConvertKeyToAddress(T _type);
    protected abstract void Init(GameObject _obj, T _type); 
}
