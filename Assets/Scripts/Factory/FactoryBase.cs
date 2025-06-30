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
        Debug.Log("��巹���� ���� ����");

        GameObject prefab = resource.GetPrefab(_type);

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, _pos, _rot, _parent);
            Init(obj, _type);
            _onComplete?.Invoke(obj);
            Debug.Log($"��巹����{obj}��ȯ");
            return obj;
        }
        else
        {
            Debug.Log("��� null��ȯ");

            _onComplete?.Invoke(null);
            return null;
        }
    }

    protected abstract void Init(GameObject _obj, T _type); 
}
