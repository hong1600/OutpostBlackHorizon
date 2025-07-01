using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBaseSync<T> : MonoBehaviour where T : Enum
{
    protected SceneResourceBase<T> resource;

    public virtual GameObject Create(T _type, Vector3 _pos, Quaternion _rot, Transform _parent, Action<GameObject> _onComplete)
    {
        GameObject prefab = resource.GetPrefab(_type);

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, _pos, _rot, _parent);

            if (PhotonNetwork.IsMasterClient) 
            {
                Init(obj, _type, true);
            }
            else
            {
                Init(obj, _type, false);
            }

            _onComplete?.Invoke(obj);
            return obj;
        }
        else
        {
            _onComplete?.Invoke(null);
            return null;
        }
    }

    protected abstract void Init(GameObject _obj, T _type, bool _isMaster);
}
