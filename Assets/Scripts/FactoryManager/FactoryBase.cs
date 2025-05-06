using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class FactoryBase<T> : MonoBehaviour
{
    public virtual void Create(T _type, Vector3 _pos, Quaternion _rot, Action<GameObject> _onComplete)
    {
        StartCoroutine(StartLoad(_type, _onComplete));
    }

    IEnumerator StartLoad(T _type, Action<GameObject> _onComplete)
    {
        string address = ConvertKeyToAddress(_type);
        var handle = Addressables.LoadAssetAsync<GameObject>(address);
        yield return handle;

        if(handle.Status == AsyncOperationStatus.Succeeded) 
        {
            GameObject prefab = handle.Result;
            Init(prefab, _type);
            _onComplete?.Invoke(prefab);

            ObjectPoolManager.instance.ReturnObject(_type.ToString(), prefab);
        }
        else
        {
            _onComplete?.Invoke(null);
        }
    }

    protected abstract string ConvertKeyToAddress(T _type);
    protected abstract void Init(GameObject _obj, T _type); 
}
