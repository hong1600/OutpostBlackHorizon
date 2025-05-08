using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SceneResourceBase<T> : Singleton<SceneResourceBase<T>> where T : System.Enum
{
    static Dictionary<T, GameObject> loadedPrefabDic = new Dictionary<T, GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    protected void LoadSceneResources(T[] _adresses)
    {
        for (int i = 0; i < _adresses.Length; i++)
        {
            T type = _adresses[i];
            string address = ConvertEnumToAddress(_adresses[i]);

            if (!loadedPrefabDic.ContainsKey(_adresses[i]))
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
                RegisterCallback(type, handle);
            }
        }
    }

    private void RegisterCallback(T _type, AsyncOperationHandle<GameObject> _handle)
    {
        _handle.Completed += delegate (AsyncOperationHandle<GameObject> operation)
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                loadedPrefabDic[_type] = operation.Result;
            }
        };
    }

    public void UnloadSceneResources()
    {
        foreach (KeyValuePair <T, GameObject> kvp in loadedPrefabDic)
        {
            Addressables.Release(kvp.Value);
        }
        loadedPrefabDic.Clear();
    }

    public GameObject GetPrefab(T _adress)
    {
        if (loadedPrefabDic.ContainsKey(_adress))
        {
            return loadedPrefabDic[_adress];
        }

        return null;
    }

    public string ConvertEnumToAddress(T _type)
    {
        string enumString = _type.ToString().ToLower();
        string format = char.ToUpper(enumString[0]) + enumString.Substring(1);
        return format;
    }

    protected abstract void LoadResources();
}
