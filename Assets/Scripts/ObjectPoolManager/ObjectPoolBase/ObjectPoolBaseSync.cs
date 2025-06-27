using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBaseSync<T> : MonoBehaviour
{
    [SerializeField] protected List<GameObject> objectList = new List<GameObject>();
    [SerializeField] protected List<Transform> parentList = new List<Transform>();

    public Dictionary<T, Queue<GameObject>> myPoolDic = new Dictionary<T, Queue<GameObject>>();
    public Dictionary<T, Queue<GameObject>> remotepoolDic = new Dictionary<T, Queue<GameObject>>();
    public Dictionary<T, GameObject> prefabDic = new Dictionary<T, GameObject>();
    public Dictionary<T, Transform> parentDic = new Dictionary<T, Transform>();

    public virtual void Init()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            T type = (T)(object)i;

            myPoolDic[type] = new Queue<GameObject>();
            remotepoolDic[type] = new Queue<GameObject>();
            prefabDic[type] = objectList[i];
            parentDic[type] = parentList[i];
        }
    }

    public GameObject FindObject(T _type, Vector3 _pos, Quaternion _rot, string _path)
    {
        if (myPoolDic.TryGetValue(_type, out Queue<GameObject> pool))
        {
            if (pool.Count > 0)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    GameObject obj = pool.Dequeue();

                    PhotonView pv = obj.GetComponent<PhotonView>();

                    if (!obj.activeInHierarchy && pv != null && pv.IsMine)
                    {
                        obj.transform.position = _pos;
                        obj.transform.rotation = _rot;
                        obj.SetActive(true);
                        return obj;
                    }

                    pool.Enqueue(obj);
                }
            }

            GameObject newObj = PhotonNetwork.Instantiate(_path + prefabDic[_type].name, _pos, _rot);
            newObj.name = prefabDic[_type].name;
            newObj.SetActive(true);
            return newObj;
        }
        return null;
    }

    public void ReturnPool(T _type, GameObject _obj)
    {
        if (myPoolDic.TryGetValue(_type, out Queue<GameObject> pool))
        {
            _obj.SetActive(false);
            pool.Enqueue(_obj);
        }
    }
}
